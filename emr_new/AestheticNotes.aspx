<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="AestheticNotes.aspx.cs" Inherits="AestheticNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="AesVisits">
		<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td colspan="2">
					<b>Aesthetic Contact Entires (all)</b> [<a href="patient_aestheticNotes_all.aspx?patientid=<%= PatientID %>">view
						all details</a>]
				</td>
				<td>
					<div align="right">
						[<a href="contact_record_add.aspx?patientid=<%= PatientID %>">Add</a>]</div>
				</td>
			</tr>
			<tr class="regText">
				<td width="90">
					<b>Date</b>
				</td>
				<td width="171">
					<strong>Type</strong>
				</td>
				<td width="351">
					<b>Notes</b>
				</td>
			</tr>
			<tr valign="top" class="regText">
				<td colspan="3">
					<hr size="1" />
				</td>
			</tr>
			<asp:Repeater ID="rptNotes" runat="server">
				<ItemTemplate>
					<tr valign="top" class="regText">
						<td width="90">
							<a href="contact_record_close.aspx?ContactID=<%# Eval("ContactID")%>">
								<%# ((DateTime)Eval("ContactDateEntered")).ToShortDateString() %></a>
						</td>
						<td width="200">
							<%# Eval("AptTypeDesc")%>
						</td>
						<td>
							<%# Eval("MessageBody")%>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</div>
</asp:Content>
