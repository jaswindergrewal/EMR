<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="Patients_CriticalTasks.aspx.cs" Inherits="Patients_CriticalTasks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="LMC" TagName="PatientInfo" Src="controls/PatientInfo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

	<asp:UpdatePanel ID="upd" runat="server">
		<ContentTemplate>
			<asp:GridView ID="grdTasks" runat="server" GridLines="Horizontal" AutoGenerateColumns="false"
				RowStyle-HorizontalAlign="Center" CellPadding="4" CssClass="border" RowStyle-VerticalAlign="Middle"
				Width="672px">
				<Columns>
					<asp:BoundField DataField="TaskName" HeaderText="Task" ItemStyle-VerticalAlign="Middle" />
					<asp:TemplateField HeaderText="Requested">
						<ItemTemplate>
							<asp:CheckBox ID="cbRequested" runat="server" Checked='<%# Eval("Requested") %>'
								CssClass="regText" OnCheckedChanged="Requested_CheckedChanged" AutoPostBack="true" />
							<asp:Label ID="lblRequested" runat="server" Text='<%# Eval("RequestedDate") %>' CssClass="regText" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Received">
						<ItemTemplate>
							<asp:CheckBox ID="cbReceived" runat="server" Checked='<%# Eval("Received") %>' CssClass="regText"
								OnCheckedChanged="Received_CheckedChanged" AutoPostBack="true" />
							<asp:Label ID="lblReceived" runat="server" Text='<%# Eval("ReceivedDate") %>' CssClass="regText" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Reviewed">
						<ItemTemplate>
							<asp:CheckBox ID="cbReviewed" runat="server" Checked='<%# Eval("Reviewed") %>' CssClass="regText"
								OnCheckedChanged="Reviewed_CheckedChanged" AutoPostBack="true" />
							<asp:Label ID="lblReviewed" runat="server" Text='<%# Eval("ReviewedDate") %>' CssClass="regText" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Label ID="lblID" runat="server" Text='<%# Eval("TaskID") %>' Style="display: none" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:HyperLink ID="lnkDoc" runat="server" NavigateUrl='<%# Eval("Upload_Path") %>'
								Text='<%# Eval("Upload_Title") %>' Target="_blank" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modReceived" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="" TargetControlID="Dummy" PopupControlID="pnlReceived" Y="200" />
			<asp:Panel ID="pnlReceived" runat="server" CssClass="modalPopup" Width="600px">
				<div class="popup_Container">
					<div class="popup_Titlebar" id="Div4">
						<div class="TitlebarLeft" id="Div5" runat="server">
							Recieve the document
						</div>
					</div>
					<div class="popup_Body">
						<p>
							<asp:Label ID="lblTaskName" runat="server" /></p>
						Select the upload that was received
						<asp:GridView ID="grdDocs" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="grdDocs_SelectedIndexChanged"
							DataKeyNames="ID,PatientID" Caption="Documents for patient" CssClass="FormField"
							EmptyDataText="No documents" Width="100%">
							<Columns>
								<asp:TemplateField HeaderText="Document">
									<ItemTemplate>
										<asp:HyperLink ID="hlDoc" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Upload_Path") %>'
											Target="_blank" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:g}" />
								<asp:ButtonField ControlStyle-CssClass="button" Text="Select" ButtonType="Button"
									CommandName="Select" />
							</Columns>
						</asp:GridView>
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnCancelDoc" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancelDoc_Click" />
					</div>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

