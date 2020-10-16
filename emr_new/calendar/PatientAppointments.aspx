<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientAppointments.aspx.cs"
	Inherits="PatientAppointments" MasterPageFile="Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<div>
		<table border="0" cellpadding="4" cellspacing="0" class="border" width="98%" style="margin-left:10px">
			<caption>
				<h3 style="text-align:left">
					Upcoming Appointments</h3>
			</caption>
			<tr>
				<td align="left" width="10%">
					<font color="#666666"><b>Patient</b></font>
				</td>
				<td align="left">
					<asp:TextBox runat="server" ID="txtPatient" Text='' CausesValidation="true" TabIndex="1"
						AutoPostBack="true" OnTextChanged="txtPatient_TextChanged" Columns="40" />
					<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1"
						ServiceMethod="GetPatientList" ServicePath="~/PatientNamesServ.asmx" TargetControlID="txtPatient" />
				</td>
			</tr>
			<tr>
				<td colspan="2" width="100%">
					<asp:GridView ID="gridAppts" runat="server"  AutoGenerateColumns="false"
						EmptyDataText="No Appointments" Visible="false" AllowPaging="true" PageSize="20"  width="100%" OnPageIndexChanging="gridAppts_PageIndexChanging">
						<Columns>
							<asp:BoundField DataField="ApptStart" DataFormatString="{0:d}" HeaderText="Date" ItemStyle-Width="20%" />
							<asp:BoundField DataField="ApptStart" DataFormatString="{0:t}" HeaderText="Time" ItemStyle-Width="20%" />
							<asp:BoundField DataField="ProviderName" HeaderText="Provider" ItemStyle-Width="20%" />
							<asp:BoundField DataField="TypeName" HeaderText="Appointment Type" ItemStyle-Width="20%" />
							<asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="20%" />
						</Columns>
					</asp:GridView>
				</td>
			</tr>
			
		</table>
	</div>
</asp:Content>
