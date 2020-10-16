<%@ Page Title="" Language="C#" MasterPageFile="~/LabSSRS/sub.master" AutoEventWireup="true" CodeFile="SummaryReport.aspx.cs" Inherits="SummaryReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<div id="PatientName" style="z-index: 7; width: 569px; visibility: visible;">
		<p class="PageTitle">
			<strong>Summary Lab Report</strong></p>

		<br />
	</div>
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport 
			DisplayName="Open Invoices" />
	</rsweb:ReportViewer>
</asp:Content>

