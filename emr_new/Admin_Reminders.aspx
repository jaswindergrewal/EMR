<%@ Page Title="Reminders" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="Admin_Reminders.aspx.cs" Inherits="Admin_Reminders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<cc1:TabContainer ID="tabScrips" runat="server" Width="1050px" CssClass="lmc_tab"
		ActiveTabIndex="0" OnActiveTabChanged="ConsoleContainer_ActiveTabChanged" AutoPostBack="true">
		<cc1:TabPanel HeaderText="Supplements" runat="server" ID="pnlSymptom" BackColor="#EFE1C9">
			<ContentTemplate>
				<iframe id="ifrHist1" runat="server" width="100%" height="3000px" style="background-image: images/export/beige_back.gif;
					border-style: none; border-width: 0" frameborder="0" src="Admin_SupplementReminders.aspx" />
			</ContentTemplate>
		</cc1:TabPanel>
		<cc1:TabPanel HeaderText="Labs" runat="server" ID="TabPanel1" BackColor="#EFE1C9">
			<ContentTemplate>
				<asp:Panel ID="pnlLabs" runat="server" Visible="false">
					<iframe id="ifrLabs" runat="server" width="100%" height="3000px" style="background-image: images/export/beige_back.gif;
						border-style: none; border-width: 0" frameborder="0" />
				</asp:Panel>
			</ContentTemplate>
		</cc1:TabPanel>
	</cc1:TabContainer>
</asp:Content>
