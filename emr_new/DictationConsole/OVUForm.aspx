<%@ Page Title="" Language="C#" MasterPageFile="sub.master" AutoEventWireup="true"
	CodeFile="OVUForm.aspx.cs" Inherits="DictationConsole_OVUForm" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt"
		interactivedeviceinfos="(Collection)" processingmode="Remote" waitmessagefont-names="Verdana"
		waitmessagefont-size="14pt" asyncrendering="true" sizetoreportcontent="true"
		backcolor="Transparent" interactivitypostbackmode="AlwaysAsynchronous" pagecountmode="Actual">
		<ServerReport ReportPath="/EMR Reports/OVUForm New" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="Office Visit Update" />
	</rsweb:reportviewer>
</asp:Content>
