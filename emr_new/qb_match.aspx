<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="qb_match.aspx.cs" Inherits="qb_match" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:Panel ID="pnlMatch" runat="server" CssClass="modalPopup" Width="600px">
		<div class="popup_Container">
			<div class="popup_Titlebar" id="Div1">
				<div class="TitlebarLeft" id="Div2" runat="server">
					Match a Quickbooks Customer
				</div>
			</div>
			<div class="popup_Body">
				<p>
					This patient has not been matched to a customer in QuickBooks.<br />
					To Match a customer, select an exisitng QuickBooks customer from the drop down.<br />
				</p>
				<asp:Label ID="lblAddress" runat="server" /><br />
				<asp:DropDownList ID="ddlQBCustomers" runat="server" />
			</div>
			<div class="popup_Buttons">
				<asp:Button ID="btnMatch" runat="server" Text="Match" CssClass="button" OnClick="btnMatch_Click" />
			</div>
		</div>
	</asp:Panel>
	<asp:Panel ID="pnlMatched" runat="server" CssClass="modalPopup" Width="600px">
		<div class="popup_Container">
			<div class="popup_Titlebar" id="Div3">
				<div class="TitlebarLeft" id="Div6" runat="server">
					Match a Quickbooks Customer
				</div>
			</div>
			<div class="popup_Body">
				<p>
					This patient is matched to a customer in QuickBooks.</p>
			</div>
			<div class="popup_Buttons">
			</div>
		</div>
	</asp:Panel>
</asp:Content>
