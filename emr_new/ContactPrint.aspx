<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="ContactPrint.aspx.cs" Inherits="ContactPrint" %>

<%@ Register TagPrefix="LMC" TagName="PatientInfo" Src="controls/PatientInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div style="width:1000px;">
		<table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781">
				<td>
					<strong>Contact Details </strong>
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					<input name="Button" type="button" class="button" onclick="MM_goToURL('parent','Manage.aspx?Contact=True&PatientID=<%=Contact.PatientID%>');return document.MM_returnValue"
						value="Back to Contact Records"/>
				</td>
			</tr>
			<tr>
				<td width="194" nowrap="nowrap">
					<strong>Patient Name </strong>
				</td>
				<td>
					<asp:Label ID="lblPatientName" runat="server"  />
				</td>
				<td width="100">
					<strong>Date Entered</strong>
				</td>
				<td>
					<asp:Label ID="lblContactDateEntered" runat="server" />
				</td>
			</tr>
			<tr>
				<td width="194" nowrap="nowrap">
					<strong>Category</strong>
				</td>
				<td width="315">
					<asp:Label ID="lblAptTypeDesc" runat="server" />
				</td>
				<td width="100">
					<strong>Entered By</strong>
				</td>
				<td width="125">
					<asp:Label ID="lblEnteredBy" runat="server" />
				</td>
			</tr>
		</table>
		<br />
		<%= ContactMessage %>
	</div></asp:Content>
