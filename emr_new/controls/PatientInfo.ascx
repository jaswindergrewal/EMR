<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientInfo.ascx.cs" Inherits="controls_PatientInfo" %>
<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
	<tr bgcolor="#D6B781">
          <% 
                if (objModelPatient != null)
                {%>

		<td class="regText" id="tdName" runat="server">
			<b>Patient Name:</b>
          
               
			<%= objModelPatient.FirstName %>&nbsp;<%=objModelPatient.MiddleInitial%>&nbsp;<%=objModelPatient.LastName%> [<%= @qbName %> ]
			&nbsp;&nbsp;&nbsp;<strong>Age:</strong>
			<%=Age%>&nbsp;&nbsp;&nbsp;<strong>Clinic:&nbsp;</strong><%=objModelPatient.Clinic%>
			&nbsp;&nbsp;<strong>Sex</strong>:
			<%=objModelPatient.Sex%>
			&nbsp; &nbsp;<strong>Nick Name</strong>: <span class="style2">
				<asp:Label ID="lblNickName" runat="server" />
				<strong>Renewal Month:</strong>
				<asp:Label ID="lblRenewalMonth" runat="server" />
				<strong>Autoship:</strong>
				<asp:Label ID="lblAutoship" runat="server" /></span><br /><br /> <strong>Allergies</strong>:
			<asp:Label ID="lblAllergies" runat="server" />

            <br /><br /> <%--<strong>Management Program</strong>:
			<asp:Label ID="lblManagementProgram" runat="server" />--%>
			
			<asp:Label ID="lblInactive" runat="server" Text="Patient is INACTIVE" Font-Bold="true"
				BackColor="Red" />
		</td>
		<td nowrap="nowrap" class="regText" align="right">
			[<a href="patient_details_printable.aspx?patientid=<%= PatientID %>" target="_blank">Printable
				View</a>]<br /><br />
            <asp:Button ID="btnNewTicket" runat="server" Text="Autoship Ticket" class="button"
				OnClick="btnNewTicket_Click" />&nbsp;&nbsp;<asp:Button ID="btnAllergies" runat="server" Text="Edit Allergies" class="button"
				OnClick="btnAllergies_Click" />
		</td>
        

                <% } 
                else 
                {
                %>
                
        <td class="regText" id="td1" runat="server">
			<b>Patient Name:</b>
          
               
			
			&nbsp;&nbsp;&nbsp;<strong>Age:</strong>
			&nbsp;&nbsp;&nbsp;<strong>Clinic:&nbsp;</strong>
			&nbsp;&nbsp;<strong>Sex</strong>:
			
			&nbsp; &nbsp;<strong>Nick Name</strong>: <span class="style2">
				<asp:Label ID="Label1" runat="server" />
				<strong>Renewal Month:</strong>
				<asp:Label ID="Label2" runat="server" />
				<strong>Autoship:</strong>
				<asp:Label ID="Label3" runat="server" /></span><br /><br /> <strong>Allergies</strong>:
			<asp:Label ID="Label4" runat="server" />
			<%--<asp:Button ID="Button1" runat="server" Text="Edit Allergies" class="button"
				OnClick="btnAllergies_Click" />--%>
			<asp:Label ID="Label5" runat="server" Text="Patient is INACTIVE" Font-Bold="true"
				BackColor="Red" />
		</td>
	
                <% } %>
	</tr>
</table>
<br />
