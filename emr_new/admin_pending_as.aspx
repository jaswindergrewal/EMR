<%@ Page Title="Pending Auto Ship Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_pending_as.aspx.cs" Inherits="admin_pending_as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p class="PageTitle">
		Pending Auto Ship Requests</p>
	<table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781">
			<td>
				<strong>Appointment Date </strong>
			</td>
			<td>
				<strong>Date Range for Follow Up </strong>
			</td>
			<td>
				<strong>Clinic</strong><br />

                 <asp:DropDownList ID="ddlClinic" runat="server" AutoPostBack="true" CssClass="FormField clinic" Width="150px" OnSelectedIndexChanged="ddlClinic_SelectedIndexChanged">
                   
                </asp:DropDownList>
			</td>
			<td>
				<strong>Follow Type</strong>
			</td>
			<td>
				<strong>Entered By </strong>
			</td>
			<td>
				<strong>Patient Name </strong>
			</td>
		</tr>
		<asp:Repeater ID="rptConsults" runat="server">
			<ItemTemplate>
				<tr>
					<td>
						<a href="admin_contact_add_pendingfollowups.aspx?followUp_id=<%# Eval("followup_id")%>&patientid=<%# Eval("patientid")%>">
							<%# Eval("dateentered")%></a>
					</td>
					<td>
						<a href="admin_contact_add_pendingfollowups.aspx?followUp_id=<%# Eval("followup_id")%>&patientid=<%# Eval("patientid")%>">
							[<%# Eval("Range_Start")%>] - [<%# Eval("Range_End")%>]</a>
					</td>
					<td>
						<%# Eval("clinic")%>
					</td>
					<td>
						<%# Eval("followup_type_desc")%>
					</td>
					<td>
						<%# Eval("EmployeeName")%>
					</td>
					<td>
						<%# Eval("Lastname")%>,&nbsp;<%# Eval("Firstname")%></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<p>
		<input name="Submit" type="button" class="button" onclick="MM_goToURL('parent','admin_main.aspx');return document.MM_returnValue"
			value="Back to Admin Page">
	</p>
	<p>
		&nbsp;
	</p>
</asp:Content>
