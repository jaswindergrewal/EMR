<%@ Page Title="" Language="C#" MasterPageFile="sub.master" AutoEventWireup="true" CodeFile="WeightChart.aspx.cs" Inherits="DictationConsole_WeightChart" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual"
		ShowBackButton="false" ShowCredentialPrompts="false" ShowDocumentMapButton="false"
		ShowExportControls="false" ShowFindControls="false" ShowPageNavigationControls="false"
		ShowParameterPrompts="false" ShowPrintButton="false" ShowPromptAreaButton="false"
		ShowRefreshButton="false" ShowToolBar="false">
		<ServerReport ReportPath="/EMR Reports/WeightGraph" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="" />
	</rsweb:ReportViewer>
</asp:Content>


