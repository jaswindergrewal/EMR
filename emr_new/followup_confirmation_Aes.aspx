<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="followup_confirmation_Aes.aspx.cs" Inherits="followup_confirmation_Aes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="700" height="120" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr>
			<td>
				Is this directly related to an appointment?<br>
				If so please click on the Appointments tab on the EMR Navigation Menu,<br>
				find the Appointment and enter it from the Appointment Console.
			</td>
			<td>
				<p>
					<input name="Button" type="button" class="button" onclick="MM_goToURL('self','patientInfo.aspx?patientid=<%=Request.QueryString["patientid"]%>    ');return document.MM_returnValue"
						value="Back to Patient Details">
				</p>
			</td>
		</tr>
		<tr>
			<td height="185">
				If this Follow Up Request is <font color="#FF0000"><strong>not</strong></font> directly
				related to an existing Appointment, click to continue.
			</td>
			<td>
				<p>
					<input name="records_btn" type="button" class="button" id="records_btn" onclick="MM_goToURL('self','MedicalRecords.aspx?patientid=<%= Request.QueryString["patientid"] %>');return document.MM_returnValue"
						value="Medical Records Follow Up">
				</p>
				<p>
					<input name="consult_btn" type="button" class="button" id="Button2" onclick="MM_goToURL('self','ScheduleConsult.aspx?patientid=<%= Request.QueryString["patientid"] %>');return document.MM_returnValue"
						value="Enter Consult Follow Up">
				</p>
				<p>
					<input name="general_foll_btn" type="button" class="button" id="general_foll_btn"
						onclick="MM_goToURL('self','FollowupNote.aspx?patientid=<%= Request.QueryString["patientid"] %>');return document.MM_returnValue"
						value="Enter General Follow Up">
				</p>
				<p>
					<input name="bd_btn" type="button" class="button" id="bd_btn" onclick="MM_goToURL('self','LabAdd.aspx?patientid=<%= Request.QueryString["patientid"] %>');return document.MM_returnValue"
						value="Enter BD Follow Up">
				</p>
				<p>
					<input name="bd_btn" type="button" class="button" id="Button1" onclick="MM_goToURL('self','apt_followupnote_aes_add_short.aspx?patientid=<%= Request.QueryString["patientid"] %>');return document.MM_returnValue"
						value="Schedule Aesthetic Follow Up">
				</p>
			</td>
		</tr>
	</table>
</asp:Content>
