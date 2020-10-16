<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="PendingFollowUps.aspx.cs" Inherits="PendingFollowUps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="FlaggedFollow">
		<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td>
					<b>Follow Ups </b>
				</td>
				<td align="center">
					<asp:RadioButtonList ID="rdoPending" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
						<asp:ListItem Selected="True" Text="Pending Only" Value="0" />
						<asp:ListItem Selected="False" Text="All" Value="1" />
					</asp:RadioButtonList>
				</td>
				<td>
					<div align="right">
						[<a href="followup_confirmation_Aes.aspx?patientid=<%= PatientID %>">Add</a>]</div>
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
			<asp:Repeater ID="rptFollow" runat="server">
				<ItemTemplate>
					<tr valign="top" class="regText">
						<td>
							</a><a href="admin_contact_add_pendingfollowups.aspx?patientid=<%# Eval("patientid") %>&followup_id=<%# Eval("FollowUp_ID") %> &MasterPage=~/sub.master">[<%# Eval("Range_Start") %>]
								- [<%# Eval ("Range_End") %>]</a>
						</td>
						<td>
							<%# Eval("FollowUp_Type_Desc") %>
						</td>
						<td>
							<%# Eval("FollowUp_Body") %>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</div>
</asp:Content>
