<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="CombinePatients.aspx.cs" Inherits="CombinePatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p>
		<asp:Label ID="lblTitle" runat="server" CssClass="PageTitle" Text="Combine Patient Records" /></p>
	<asp:Label ID="lblSource" runat="server" CssClass="regText" Text="Source ID" />
	<asp:TextBox ID="txtSource" runat="server" /><br />
	<asp:Label ID="lblDest" runat="server" CssClass="regText" Text="Destination ID" />
	<asp:TextBox ID="txtDest" runat="server" /><br />
	<asp:Button ID="btnGo" runat="server" CssClass="button" Text="Go!" OnClick="btnGo_Click" />
</asp:Content>
