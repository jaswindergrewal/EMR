<%@ Page Title="Manage External Labs and Panels" Language="C#" MasterPageFile="~/external/Site.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="External_Manage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<cc1:TabContainer ID="tabScrips" runat="server" Width="1050px" CssClass="lmc_tab"
		ActiveTabIndex="0" AutoPostBack="true">
		<cc1:TabPanel HeaderText="Manage Panels" runat="server" ID="pnlPanel" BackColor="#EFE1C9">
			<ContentTemplate>
				<iframe id="ifrHist1" runat="server" width="100%" height="3000px" style="background-image: images/export/beige_back.gif;
					border-style: none; border-width: 0" frameborder="0" src="Panels.aspx" />
			</ContentTemplate>
		</cc1:TabPanel>
		<cc1:TabPanel HeaderText="Manage Labs" runat="server" ID="TabPanel1" BackColor="#EFE1C9">
			<ContentTemplate>
				<asp:Panel ID="pnlLabs" runat="server" Visible="true">
					<iframe id="ifrLabs" runat="server" width="100%" height="3000px" style="background-image: images/export/beige_back.gif;
						border-style: none; border-width: 0" frameborder="0" src="Labs.aspx" />
				</asp:Panel>
			</ContentTemplate>
		</cc1:TabPanel>
	</cc1:TabContainer></asp:Content>

