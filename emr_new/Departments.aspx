<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="Departments.aspx.cs" Inherits="Departments" %>

<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
	TagPrefix="NineRays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
	TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<table>
				<tr>
					<td>
						<NineRays:FlyTreeView ID="Dept" runat="server" 
							PostBackOnDropAccept="true" DragDropMode="Copy">
							<Nodes>
								<NineRays:FlyTreeNode Text="Departments" ImageUrl="$vista_folder" ContextMenuID="mnuDept"
									Expanded="true" />
							</Nodes>
						</NineRays:FlyTreeView>
					</td>
				</tr>
			</table>
			<NineRays:FlyContextMenu ID="mnuDept" runat="server" OnCommand="mnuDept_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Create Department" CommandName="Create" AutoPostBack="true" />
                    
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuStaff" runat="server" OnCommand="mnuStaff_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Remove from Dpeartment" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>

            <NineRays:FlyContextMenu ID="mnuDepatment" runat="server" OnCommand="mnuStaff_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Remove from Dpeartment" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>

            
			<asp:Button ID="dummy1" runat="server" Style="display: none" />
			<cc1:ModalPopupExtender ID="modDept" runat="server" BackgroundCssClass="ModalPopupBG"
				CancelControlID="btnCancelDept" TargetControlID="dummy1" PopupControlID="pnlDept" />
			<asp:Panel ID="pnlDept" runat="server" CssClass="modalPopup" Width="250px">
				<div class="popup_Container">
					<div class="popup_Titlebar">
						<div class="TitlebarLeft">
							Add Department
						</div>
					</div>
					<div class="popup_Body">
						<asp:Label ID="lblDeptName" runat="server" Text="Name:" /><asp:TextBox ID="txtDeptName"
							runat="server" />
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnOkDept" Text="Done" CssClass="button" runat="server" OnClick="btnOkDept_Click" />
						<asp:Button ID="btnCancelDept" Text="Cancel" CssClass="button" runat="server" />
					</div>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
