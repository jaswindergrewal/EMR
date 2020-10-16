<%@ Page Title="Manage Lab Report" Language="C#" MasterPageFile="~/LabSSRS/Site.master" AutoEventWireup="true"
	CodeFile="Manager1.aspx.cs" Inherits="Manager" ValidateRequest="false" %>

<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
	TagPrefix="NineRays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
	TagPrefix="cc2" %>
<%@ Register Namespace="MyControls" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<%--	<cc1:ToolkitScriptManager ID="ScriptManager" runat="Server" EnablePartialRendering="true"
		ScriptMode="Release" CombineScriptsHandlerUrl="~/CombineScriptsHandler.ashx" LoadScriptsBeforeUI="false" />--%>
	<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="LabReportPanels"
		DynamicLayout="true">
		<ProgressTemplate>
			<img src="~/images/indicator.gif" alt="Loading" />
			<strong>Please Wait . . .</strong></ProgressTemplate>
	</asp:UpdateProgress>
	<flan:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server"
		ControlToOverlayID="Panels" CssClass="updateProgress" TargetControlID="UpdateProgress1" />
	<asp:UpdatePanel ID="LabReportTests" runat="server">
		<ContentTemplate>
			<div style="width: 300px;">
				<NineRays:FlyTreeView ID="Tests" runat="server" DragDropAcceptNames="" Padding="3px"
					FadeEffect="True" BackColor="White" DragDropName="" DragDropMode="Move" BackgroundImage="~/images/export/beige_back.gif"
					ExpandLevel="2" CssClass="regText" ContextMenuID="mnuTest" DrawLines="true" OnPopulateNodes="Tests_PopulateNodes">
					<HoverStyle Font-Underline="True" />
					<Nodes>
						<NineRays:FlyTreeNode Text="Tests" Expanded="true" ImageUrl="$vista_folder" PopulateNodesOnDemand="true">
						</NineRays:FlyTreeNode>
					</Nodes>
				</NineRays:FlyTreeView>
				<NineRays:FlyContextMenu ID="mnuTest" runat="server" OnCommand="mnuTest_Command">
					<Items>
						<NineRays:FlyMenuItem Text="Hide" CommandName="Hide" AutoPostBack="true" />
					</Items>
				</NineRays:FlyContextMenu>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:UpdatePanel ID="LabReportPanels" runat="server">
		<ContentTemplate>
			<div>
			<NineRays:FlyTreeView ID="Panels" runat="server" Padding="3px" FadeEffect="True"
				BackColor="White" DragDropName="" DragDropMode="Copy" BackgroundImage="~/images/export/beige_back.gif"
				CssClass="regText" OnNodeInserted="Panels_NodeInserted" PostBackOnDropAccept="true"
				DrawLines="true" ContextMenuID="mnuPanel" Width="550px">
				<HoverStyle Font-Underline="True" />
				<Nodes>
					<NineRays:FlyTreeNode Text="Panels" Expanded="true" ImageUrl="$vista_folder">
					</NineRays:FlyTreeNode>
				</Nodes>
			</NineRays:FlyTreeView>
			<NineRays:FlyContextMenu ID="mnuPanel" runat="server" OnCommand="mnuPanel_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Create Panel" CommandName="Create" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuGroups" runat="server" OnCommand="mnuGroups_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Create Group" CommandName="Create" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuOneGroup" runat="server" OnCommand="mnuOneGroup_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Edit Group Info" CommandName="Edit" AutoPostBack="true" />
					<NineRays:FlyMenuItem Text="Remove Group" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuOnePanel" runat="server" OnCommand="mnuOnePanel_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Edit Panel Info" CommandName="Edit" AutoPostBack="true" />
					<NineRays:FlyMenuItem Text="Remove Panel" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuOneTest" runat="server" OnCommand="mnuOneTest_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Remove" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuOneTrigger" runat="server" OnCommand="mnuOneTrigger_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Edit Triger" CommandName="Edit" AutoPostBack="true" />
					<NineRays:FlyMenuItem Text="Remove Trigger" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuTriggers" runat="server" OnCommand="mnuTriggers_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Create Trigger" CommandName="CreateTriger" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuConditions" runat="server" OnCommand="mnuConditions_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Add Condition" CommandName="Add" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<NineRays:FlyContextMenu ID="mnuOneCondition" runat="server" OnCommand="mnuOneCondition_Command">
				<Items>
					<NineRays:FlyMenuItem Text="Edit Condition" CommandName="Edit" AutoPostBack="true" />
					<NineRays:FlyMenuItem Text="Add/Edit Details" CommandName="Details" AutoPostBack="true" />
					<NineRays:FlyMenuItem Text="Remove Condition" CommandName="Remove" AutoPostBack="true" />
				</Items>
			</NineRays:FlyContextMenu>
			<asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modPanelInfo" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="" TargetControlID="Dummy" PopupControlID="pnlPanelInfo" />
			<asp:Panel ID="pnlPanelInfo" runat="server" CssClass="modalPopup" Width="600px">
				<div class="popup_Container">
					<div class="popup_Titlebar" id="Div4">
						<div class="TitlebarLeft" id="Div5" runat="server">
							Panel Iinformation
						</div>
					</div>
					<div class="popup_Body">
						<asp:Label ID="lblPanelName" runat="server" Text="Panel Name" />
						<asp:TextBox ID="txtPanelName" runat="server" Text="" /><br />
						<asp:Label ID="lblPnaelDescription" runat="server" Text="Panel Description" /><br />
						<custom:CustomEditor ID="edPanelDescrip" runat="server" Height="350px" Width="100%" />
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnCancelPanelInfo" Text="Cancel" CssClass="button" runat="server" />
						<asp:Button ID="btnOkPanelInfo" Text="Ok" CssClass="button" runat="server" OnClick="btnOkPanelInfo_Click"
							AutoPostBack="true" />
					</div>
				</div>
			</asp:Panel>
			<asp:Button ID="Dummy1" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modGroups" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="" TargetControlID="Dummy1" PopupControlID="pnlGroupInfo" />
			<asp:Panel ID="pnlGroupInfo" runat="server" CssClass="modalPopup" Width="900px">
				<div class="popup_Container" style="border-style: groove; overflow: auto; width: 900px;
					height: 460px; border-color: #E8E8E8; border-width: thin">
					<div class="popup_Titlebar" id="Div1">
						<div class="TitlebarLeft" id="Div2" runat="server">
							Group Iinformation
						</div>
					</div>
					<div class="popup_Body">
						<table width="100%">
							<tr>
								<td>
									<asp:Label ID="lblGroupTitle" runat="server" Text="Group Title" />
									<asp:TextBox ID="txtGroupTitle" runat="server" />
								</td>
								<td>
									<asp:Label ID="lblHigh" runat="server" Text="Range Upper" />
									<asp:TextBox ID="txtGroupHigh" runat="server" />
								</td>
								<td>
									<asp:Label ID="lblLow" runat="server" Text="Range Lower" />
									<asp:TextBox ID="txtGroupLow" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:Label ID="lbl1" runat="server" Text="Description" />
									<custom:CustomEditor ID="edDescrip" runat="server" Height="250px" Width="700px" />
									<br />
									<asp:Label ID="Label1" runat="server" Text="High Text" />
									<custom:CustomEditor ID="edHigh" runat="server" Height="250px" Width="700px" />
									<br />
									<asp:Label ID="Label2" runat="server" Text="Low Text" />
									<custom:CustomEditor ID="edLow" runat="server" Height="250px" Width="700px" />
									<br />
									<asp:Label ID="Label3" runat="server" Text="Normal Text" />
									<custom:CustomEditor ID="edNormal" runat="server" Height="250px" Width="700px" />
								</td>
							</tr>
						</table>
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnCancelGroupInfo" Text="Cancel" CssClass="button" runat="server" />
						<asp:Button ID="btnOkGroupInfo" Text="Ok" CssClass="button" runat="server" OnClick="btnOkGroupInfo_Click"
							AutoPostBack="true" />
					</div>
				</div>
			</asp:Panel>
			<asp:Button ID="Dummy3" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modTrigger" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="btnTriggerCancel" TargetControlID="Dummy3" PopupControlID="pmlTrigger" />
			<asp:Panel ID="pmlTrigger" runat="server" CssClass="modalPopup" Width="600px">
				<div class="popup_Container">
					<div class="popup_Titlebar" id="Div7">
						<div class="TitlebarLeft" id="Div8" runat="server">
							Trigger Iinformation
						</div>
					</div>
					<div class="popup_Body">
						<asp:Label ID="Label4" runat="server" Text="Trigger Title" />
						<asp:TextBox ID="txtTriggerName" runat="server" Text="" /><br />
						<asp:Label ID="Label5" runat="server" Text="Trigger Description" /><br />
						<custom:CustomEditor ID="edTriggerDesc" runat="server" Height="350px" Width="100%" />
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnTriggerCancel" Text="Cancel" CssClass="button" runat="server" />
						<asp:Button ID="btnTriggerOk" Text="Ok" CssClass="button" runat="server" OnClick="btnTriggerOk_Click" />
					</div>
				</div>
			</asp:Panel>
			<asp:Button ID="Dummy4" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modCondition" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="btnConditionCancel" TargetControlID="Dummy4" PopupControlID="pnlCondition" />
			<asp:Panel ID="pnlCondition" runat="server" CssClass="modalPopup" Width="600px">
				<div class="popup_Container">
					<div class="popup_Titlebar" id="Div9">
						<div class="TitlebarLeft" id="Div10" runat="server">
							Conditions Information
						</div>
					</div>
					<div class="popup_Body">
						<asp:Label ID="Label6" runat="server" Text="Condition Name" />
						<asp:TextBox ID="txtConditionName" runat="server" Text="" /><br>
						<asp:Label ID="lblSex" runat="server" Text="Sex" />
						<asp:RadioButtonList ID="lstSex" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Text="Male" Value="M" />
							<asp:ListItem Text="Female" Value="F" />
							<asp:ListItem Text="Both" Value="A" Selected="True" />
						</asp:RadioButtonList>
						<asp:Label ID="Label7" runat="server" Text="Condition Description" /><br />
						
						<custom:CustomEditor  ID="edConditionDescrip" runat="server" Height="200px" Width="100%" />
					</div>
					<div class="popup_Buttons">
						<asp:Button ID="btnConditionCancel" Text="Cancel" CssClass="button" runat="server" />
						<asp:Button ID="btnConditionOk" Text="Ok" CssClass="button" runat="server" OnClick="btnConditionOk_Click" />
					</div>
				</div>
			</asp:Panel>
			<asp:Button ID="Dummy5" runat="server" Style="visibility: hidden;" />
			<cc1:ModalPopupExtender ID="modDetails" BackgroundCssClass="ModalPopupBG" runat="server"
				CancelControlID="btnDetailsDone" TargetControlID="Dummy5" PopupControlID="pnlDetails" />
			<asp:Panel ID="pnlDetails" runat="server" CssClass="modalPopup" Width="600px" Height="500px">
				<div class="popup_Container">
					<div class="popup_Titlebar" id="Div3">
						<div class="TitlebarLeft" id="Div6" runat="server">
							<asp:Label ID="lblDetails" runat="server" />
						</div>
					</div>
					<table>
						<tr>
							<td colspan="2">
								<div class="popup_Body">
									<asp:GridView ID="grdConditions" runat="server" HeaderStyle-BackColor="Blue" HeaderStyle-ForeColor="White"
										AlternatingRowStyle-BackColor="LightBlue" AutoGenerateColumns="true" RowStyle-HorizontalAlign="Center"
										ShowHeaderWhenEmpty="true" EmptyDataText="No criteria defined" ShowFooter="true"
										OnSelectedIndexChanged="grdConditions_SelectedIndexChanged" DataKeyNames="ID">
										<Columns>
											<asp:CommandField ShowSelectButton="true" SelectText="Remove" ButtonType="Button"
												ControlStyle-CssClass="button" />
										</Columns>
										<EmptyDataTemplate>
											No Details Defined.</EmptyDataTemplate>
									</asp:GridView>
							</td>
						</tr>
						<tr>
							<td align="right">
								Group
							</td>
							<td align="left">
								<asp:DropDownList ID="ddlInsertGroups" runat="server">
									<asp:ListItem Text="Glucose" />
									<asp:ListItem Text="Hempglobin" />
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="right">
								Operator
							</td>
							<td align="left">
								<asp:DropDownList ID="ddlOperator" runat="server">
									<asp:ListItem Text="=" />
									<asp:ListItem Text=">" />
									<asp:ListItem Text="<" />
									<asp:ListItem Text="<=" />
									<asp:ListItem Text=">=" />
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="right">
								Value
							</td>
							<td align="left">
								<asp:TextBox ID="txtValue" runat="server" Width="20" Text="" />
							</td>
						</tr>
						<tr>
							<td align="center" colspan="2">
								AND
							</td>
						</tr>
						<tr>
							<td align="right">
								Operator
							</td>
							<td align="left">
								<asp:DropDownList ID="ddlOptOperator" runat="server">
									<asp:ListItem Text="=" />
									<asp:ListItem Text=">" />
									<asp:ListItem Text="<" />
									<asp:ListItem Text="<=" />
									<asp:ListItem Text=">=" />								
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="right">
								Value
							</td>
							<td align="left">
								<asp:TextBox ID="txtOptValue" runat="server" Width="20" Text="" />
							</td>
						</tr>
						<tr>
							<td colspan="2">
								<asp:Button ID="btnInsertDetail" runat="server" OnClick="btnInsertDetail_Click" Text="Insert"
									CssClass="button" />
								<asp:Button ID="btnDetailsDone" Text="Done" CssClass="button" runat="server" />
							</td>
						</tr>
					</table>
				</div>
				<div class="popup_Buttons">
				</div>
			</asp:Panel>
</div>		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
