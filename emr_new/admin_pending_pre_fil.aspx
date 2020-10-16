<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_pending_pre_fil.aspx.cs" Inherits="admin_pending_pre_fil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="900" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr>
			<td width="79" bgcolor="#D6B781">
				<strong>Patient Name </strong>
			</td>
			<td width="495" colspan="2">
				<%= PendingPrescriptions.LastName %>,
				<%= PendingPrescriptions.FirstName %>
			</td>
			<td width="88">
				<div align="right">
					<% if (PendingPrescriptions.viewable_yn == true) %>
					<input name="Button" type="button" class="button" onclick="MM_goToURL('parent','admin_prescription_delete.asp?pre_id=<%= Request.QueryString["pre_id"]%>');return document.MM_returnValue"
						value="Delete">
				</div>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>3rd Party</strong>
			</td>
			<td colspan="3">
				<%=PendingPrescriptions.ThirdParty_YN %>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Drug</strong>
			</td>
			<td colspan="3">
				<%=PendingPrescriptions.DrugName %>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Sig</strong>
			</td>
			<td colspan="3">
				<asp:TextBox ID="Sig" runat="server" CssClass="FormField" Columns="120" />
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Dispenses</strong>
			</td>
			<td>
				<asp:TextBox ID="Dispenses" runat="server" CssClass="FormField" />
			</td>
			<td colspan="2">
				<% if (PendingPrescriptions.viewable_yn == false)
	   { %>
				<strong><font color="#FF0000">THIS PRESCRIPTION HAS BEEN DELETED OR CLOSED </font>
				</strong>
				<% } %>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong># of Refills </strong>
			</td>
			<td colspan="3">
				<asp:TextBox ID="NumbRefills" runat="server" CssClass="FormField" />
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Date Entered </strong>
			</td>
			<td colspan="3">
				<asp:TextBox ID="DateEntered" runat="server" CssClass="FormField" onblur="WAValidateDT(document.form1.DateEntered,'- Invalid date or time',true,/.*/,'mm/dd/yyyy','','',false,/.*/,'','','',document.form1.DateEntered,0,true);WAAlertErrors('The following errors were found','Correct invalid entries to continue',true,false);return document.MM_returnValue"
					 />
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Notes</strong>
			</td>
			<td colspan="3">
				<asp:TextBox ID="PreNotes" runat="server" TextMode="MultiLine" Columns="50" Rows="3" CssClass="FormField"/>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<strong>Approved</strong>
			</td>
			<td colspan="3">
				<%= PendingPrescriptions.Approved_YN %><% if (PendingPrescriptions.Approved_YN == true) %>
				- <strong>Date</strong>:
				<%=PendingPrescriptions.Approved_Date %><%%>
			</td>
		</tr>
		<tr>
			<td bgcolor="#D6B781">
				<input name="Approved_YN" type="hidden" id="Approved_YN" value="1">
			</td>
			<td colspan="3">
				<input name="Back" type="button" class="button" id="Back" onclick="MM_goToURL('parent','admin_pending_prescriptions.aspx');return document.MM_returnValue"
					value="Back to List">
				<input name="Presc" type="button" class="button" id="Presc" onclick="MM_goToURL('parent','PresrcriptionList.aspx?patientid=<%=PendingPrescriptions.PatientID %>');return document.MM_returnValue"
					value="Patient Prescriptions">
				<input name="PatientDetails" type="button" class="button" id="PatientDetails" onclick="MM_goToURL('parent','Manage.aspx?patientid=<%=PendingPrescriptions.PatientID %>');return document.MM_returnValue"
					value="Patient Details">
				<input name="ApprovedDate" type="hidden" id="ApprovedDate" value="<%= DateTime.Now.ToShortDateString() %>">
				<% if (PendingPrescriptions.Approved_YN == false && PendingPrescriptions.AccessLevel == "dr") %>
				<asp:Button ID="btnApprove" runat="server" CssClass="button" Text="Approve / Submit"
					OnClick="btnApprove_Click" />
				<% if (PendingPrescriptions.Approved_YN == true)
	   { %>
				&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" OnClick="btnPrint_Click" />
				<% } %>
			</td>
		</tr>
	</table>
	
</asp:Content>
