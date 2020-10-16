<%@ Register TagPrefix="dotnetcharting" Namespace="dotnetCHARTING" Assembly="dotnetCHARTING" %>
<%@ Page language="c#" Codefile="LabChart.aspx.cs" AutoEventWireup="true" Inherits="Quest.LabChart" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LabChart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="LabelPatientName" runat="server" />
			<br />
			<asp:label id="LabelErrorMessage" runat="server" ForeColor="Red" />
			<p/>
			<asp:dropdownlist id="DropDownListTestType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListTestType_SelectedIndexChanged" />
			<input type="button" value="Back to Patient Labs" name="ClickBack" onclick=(history.back())><p/>
			
			<asp:label id="LabelStartDate" runat="server">Start Date</asp:label>
			&nbsp;
			<asp:textbox id="TextBoxStartDate" runat="server" />
			&nbsp;&nbsp;&nbsp;
			<asp:label id="LabelEndDate" runat="server">End Date</asp:label>
			&nbsp;
			<asp:textbox id="TextBoxEndDate" runat="server" />
			&nbsp;&nbsp;&nbsp;
			<asp:Button id="ButtonUpdate" runat="server" Text="Update Chart" OnClick="ButtonUpdate_Click" />
			<p/>
			<dotnetcharting:chart id="Chart1" runat="server" />
			<dotnetcharting:chart id="Chart2" runat="server" />
			<dotnetcharting:chart id="Chart3" runat="server" />
			<dotnetcharting:chart id="Chart4" runat="server" />
			<dotnetcharting:chart id="Chart5" runat="server" />
			<dotnetcharting:chart id="Chart6" runat="server" />
			<dotnetcharting:chart id="Chart7" runat="server" />
			<dotnetcharting:chart id="Chart8" runat="server" />
			<dotnetcharting:chart id="Chart9" runat="server" />
			<dotnetcharting:chart id="Chart10" runat="server" />
			<dotnetcharting:chart id="Chart11" runat="server" />
			<dotnetcharting:chart id="Chart12" runat="server" />
			<dotnetcharting:chart id="Chart13" runat="server" />
			<dotnetcharting:chart id="Chart14" runat="server" />
			<dotnetcharting:chart id="Chart15" runat="server" />
			<dotnetcharting:chart id="Chart16" runat="server" />
			<dotnetcharting:chart id="Chart17" runat="server" />
			<dotnetcharting:chart id="Chart18" runat="server" />
			<dotnetcharting:chart id="Chart19" runat="server" />
			<dotnetcharting:chart id="Chart20" runat="server" />
		</form>
	</body>
</HTML>