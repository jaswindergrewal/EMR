<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/Site.master" AutoEventWireup="true" CodeFile="AllLabReports.aspx.cs" Inherits="AllLabReports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
	<title>Generate All Lab Reports</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<div align="center">
		<h1>
			Generate Lab Reports</h1>
		<br />
		<br />
		<br />
		<div>
			<span>Enter a starting date </span>
			<br />
			<asp:TextBox ID="txtStartDate" runat="server" Width="150px" MaxLength="60" />
			<br />
			<span>Select the number of days </span>
			<br />
			<asp:TextBox ID="txtDays" runat="server" Width="150px" MaxLength="60" />
			<br />
			<span>Enter a save location (on server or UNC path)<br /><b>MUST END WITH A \</b></span>
			<br />
			<asp:TextBox ID="txtPath" runat="server" Width="150px" MaxLength="60" />
			<br />
			<asp:Button ID="BtnPdf" runat="server" Text="Click to Create Reports (this could take a while depending on the time frame selected."
				OnClick="BtnPdf_Click" />
			<asp:Panel ID="PnlRptVr" runat="server" Visible="false">
				<rsweb:ReportViewer ID="RptVr" runat="server">
					<ServerReport ReportPath="/LabReports/LabResults" ReportServerUrl="http://10.0.2.89/reportserver"
						DisplayName="Lab Report" />
				</rsweb:ReportViewer>
			</asp:Panel>
		</div>
	</div>
</asp:Content>

