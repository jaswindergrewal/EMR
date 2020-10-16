<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="problem_symp_edit.aspx.cs" Inherits="problem_symp_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="400" border="0" cellspacing="0" cellpadding="5">
		<tr><td>&nbsp;</td><td>Priority</td><td>Severity</td><td>Trend</td></tr>
		<tr class="regText">
			<td>
				<asp:Label ID="lblSymptom" runat="server" CssClass="regText" />
			</td>
			<td>
			<asp:DropDownList ID="ddlPri" runat="server">
				<asp:ListItem Text="1" Value="1"></asp:ListItem>
				<asp:ListItem Text="2" Value="2"></asp:ListItem>
				<asp:ListItem Text="3" Value="3"></asp:ListItem>
				<asp:ListItem Text="4" Value="4"></asp:ListItem>
				<asp:ListItem Text="5" Value="5"></asp:ListItem>
			</asp:DropDownList>
			</td>
			<td>
			<asp:DropDownList ID="ddlSev" runat="server">
				<asp:ListItem Text="1" Value="1"></asp:ListItem>
				<asp:ListItem Text="2" Value="2"></asp:ListItem>
				<asp:ListItem Text="3" Value="3"></asp:ListItem>
				<asp:ListItem Text="4" Value="4"></asp:ListItem>
				<asp:ListItem Text="5" Value="5"></asp:ListItem>
			</asp:DropDownList>
			</td>
			<td>
			<asp:DropDownList ID="ddlTrend" runat="server">
				<asp:ListItem Text="Better" Value="Better"></asp:ListItem>
				<asp:ListItem Text="Same" Value="Same"></asp:ListItem>
				<asp:ListItem Text="Worse" Value="Worse"></asp:ListItem>
			</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="button" />
			</td>
			<td>
				&nbsp;
			</td>
			<td>
				&nbsp;
			</td>
		</tr>
	</table>
</asp:Content>
