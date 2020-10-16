<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="RenewalException.aspx.cs" Inherits="RenewalException" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:Table ID="mainTable" runat="server" Width="500" CellPadding="6" CellSpacing="6"
		CssClass="regText">
		<asp:TableRow>
			<asp:TableCell HorizontalAlign="Right">
				Exception Text
			</asp:TableCell>
			<asp:TableCell>
				<asp:TextBox ID="txtException" runat="server"></asp:TextBox>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
				<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Content>
