<%@ Page Language="c#" CodeFile="LabTableOld.aspx.cs" AutoEventWireup="true" Inherits="Quest.LabTable" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>LabTable</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="FlowLayout">
    <form id="Form1" method="post" runat="server">
        <p>
            <b>Patient Name:<asp:Label ID="lblPatnames" Text="" runat="server"></asp:Label></b>
        </p>
        <asp:DataGrid ID="DataGridLabResults" runat="server"
            AutoGenerateColumns="true" Font-Names="Arial" Font-Size="9pt">
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <AlternatingItemStyle BackColor="#EFEFEF" />
            <Columns>
            </Columns>
        </asp:DataGrid>
    </form>
</body>
</html>
