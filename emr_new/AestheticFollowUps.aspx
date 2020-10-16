<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="AestheticFollowUps.aspx.cs" Inherits="AestheticFollowUps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="FlaggedAes">
			<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
				<tr bgcolor="#D6B781" class="regText">
					<td>
						<b>Pending Aesthetic Follow Ups </b>
					</td>
					<td>
						&nbsp;
					</td>
					<td>
						<div align="right">
							[<a href="apt_followupnote_aes_add_Short.aspx?patientid=<%= PatientID%>">Add</a>]</div>
					</td>
				</tr>
				<tr class="regText">
					<td>
						<b>Date</b>
					</td>
					<td>
						<strong>Category</strong>
					</td>
					<td>
						<b>Notes</b>
					</td>
				</tr>
				<asp:Repeater ID="rptFups" runat="server">
					<ItemTemplate>
						<tr valign="top" class="regText">
							<td>
								<a href="admin_contact_add_pendingfollowups.aspx?patientid=<%# PatientID %>&followup_id=<%# Eval("FollowUp_ID")%>">
									[<%# Eval("Range_Start")%>] - [<%# Eval("Range_End")%>]</a>
							</td>
							<td>
								<%# Eval("FollowUp_Type_Desc")%>
							</td>
							<td>
								<%# Eval("FollowUp_Body")%>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
	</div>
</asp:Content>
