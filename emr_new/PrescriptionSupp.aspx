<%@ Page Title="" Language="C#" MasterPageFile="~/LabSSRS/sub.master" AutoEventWireup="true" CodeFile="PrescriptionSupp.aspx.cs" Inherits="PrescriptionSupp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<script type="text/ecmascript" language="javascript">
		function toScrps() {
			history.back();
		}
	</script></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p>
		<obout:oboutbutton id="btnBak" text="Back to Prescriptions" runat="server" onclientclick="toScrps();return false;" /> <obout:OboutButton ID="btnList" Text="Back to Pending List" runat="server" OnClick="btnList_Click" />
	</p>
	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
		InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
		WaitMessageFont-Size="14pt" AsyncRendering="true" SizeToReportContent="true"
		BackColor="Transparent" InteractivityPostBackMode="AlwaysAsynchronous" PageCountMode="Actual">
		<ServerReport ReportPath="/EMR Reports/PrescriptionSupp" ReportServerUrl="http://10.0.2.89/reportserver"
			DisplayName="Supplements" />
	</rsweb:ReportViewer>
</asp:Content>

