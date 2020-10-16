<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="OldVisits.aspx.cs" Inherits="OldVisits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="HistricVisits">
		<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td colspan="2">
					<b>Historic Office Visits Notes </b>
				</td>
				<td>
					<div align="right">
					</div>
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
			<asp:Repeater ID="rptVisits" runat="server">
				<ItemTemplate>
					<tr valign="top" class="regText">
						<td width="90">
							<a href="Visit_historic.asp?VisitID=<%# Eval("VisitID")%>">
								<%# Eval("VisitDate")%></a>
						</td>
						<td width="200" nowrap="nowrap">
							<%# Eval("TypeofVisit")%>
						</td>
						<td>
							<%# Eval("Subjective")%>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
		<br />
		<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td colspan="3">
					<b>Historic Office Contact Notes </b>
				</td>
				<td width="351">
					<div align="right">
					</div>
				</td>
			</tr>
			<tr class="regText">
				<td width="90">
					<b>Call Date</b>
				</td>
				<td width="90">
					<strong>Visit Date</strong>
				</td>
				<td colspan="2">
					<strong>Reason</strong>
				</td>
			</tr>
			<tr valign="top" class="regText">
				<td colspan="4">
					<hr size="1" />
				</td>
			</tr>
			<asp:Repeater ID="rptNotes" runat="server">
				<ItemTemplate>
					<tr valign="top" class="regText">
						<td width="90">
							<%# Eval("CallDate")%>
						</td>
						<td width="90">
							<%# Eval("VisitDate")%>
						</td>
						<td colspan="2">
							<%# Eval("ReasonForCallBack")%>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</div>
</asp:Content>
