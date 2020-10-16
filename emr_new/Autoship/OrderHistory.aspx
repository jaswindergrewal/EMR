<%@ Page Title="Order History" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
	CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="PatientName" style="z-index: 7; width: 569px; visibility: visible;">
		<p class="PageTitle">
			<strong>Order History</strong></p>
		<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td width="81%" bgcolor="#D6B781">
					<b>Patient Name:</b>
					<asp:Label ID="lblHeader" runat="server" />
				</td>
				<td width="19%" bgcolor="#D6B781">
					<div align="right">
						<asp:Button ID="btnDetails" runat="server" CssClass="button" Text="Back to Patient Details" />
					</div>
				</td>
			</tr>
		</table>
		<br />
	</div>
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport ReportPath="/Autoship/Order History Hiddden" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="Lab Report" />
	</rsweb:ReportViewer>
</asp:Content>
