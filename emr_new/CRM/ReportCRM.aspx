<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CrmDashBoard.master" AutoEventWireup="true" CodeFile="ReportCRM.aspx.cs" Inherits="CRM_ReportCRM" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport ReportServerUrl="http://10.0.2.89/reportserver" ReportPath="/EMR Reports/OVUForm New"
			DisplayName="Open Invoices" />
	</rsweb:ReportViewer>
</asp:Content>

