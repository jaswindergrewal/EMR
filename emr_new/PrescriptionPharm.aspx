<%@ Page Title="" Language="C#" MasterPageFile="~/LabSSRS/sub.master" AutoEventWireup="true"
	CodeFile="PrescriptionPharm.aspx.cs" Inherits="PrescriptionPharm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p>
		&nbsp;<obout:OboutButton ID="btnBak" Text="Back to Prescriptions" runat="server" OnClick="btnBak_Click" /> 
    <p>
        &nbsp;<obout:OboutButton ID="btnList" Text="Back to Pending List" runat="server" OnClick="btnList_Click" />
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual" ShowRefreshButton="false">
		<ServerReport ReportPath="/EMR Reports/PrescriptionPharm" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="Prescription" />
	</rsweb:ReportViewer>
</asp:Content>
