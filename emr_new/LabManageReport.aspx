<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabManageReport.aspx.cs" MasterPageFile="~/LabSSRS/sub.master" Inherits="LabManageReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<div id="PatientName" style="z-index: 7; width: 569px; visibility: visible;">
		<p class="PageTitle">
			<strong>Lab Manage Data Report</strong></p>

		<br />
	</div>
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport ReportPath="/LMCReports/Summary" ReportServerUrl="http://50.23.221.50/ReportServer"
			DisplayName="Lab Manage Data" />
	</rsweb:ReportViewer>
</asp:Content>
