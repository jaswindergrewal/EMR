<%@ Page Title="" Language="C#" MasterPageFile="~/LabSSRS/sub.master" AutoEventWireup="true"
	CodeFile="OpenInvoices.aspx.cs" Inherits="OpenInvoices" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="PatientName" style="z-index: 7; width: 569px; visibility: visible;">
		<p class="PageTitle">
			<strong>Open Invoices</strong></p>

		<br />
	</div>
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport ReportPath="/EMR Reports/Open Invoices" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="Open Invoices" />
	</rsweb:ReportViewer>
</asp:Content>
