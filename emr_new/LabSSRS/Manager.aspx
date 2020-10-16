<%@ Page Title="Manage Lab Report" Language="C#" MasterPageFile="~/LabSSRS/Site.master" AutoEventWireup="true"
	CodeFile="Manager.aspx.cs" Inherits="Manager" ValidateRequest="false" %>

<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
	TagPrefix="NineRays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
	TagPrefix="cc2" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="LabReportPanels"
		DynamicLayout="true">
		<ProgressTemplate>
			<img src="images/indicator.gif" alt="Loading" />
			<strong>Please Wait . . .</strong></ProgressTemplate>
	</asp:UpdateProgress>
	<flan:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server"
		ControlToOverlayID="Panels" CssClass="updateProgress" TargetControlID="UpdateProgress1" />
	<asp:UpdatePanel ID="LabReportPanels" runat="server">
		<ContentTemplate>
			<table>
				<tr>
					<td valign="top">
						<div style="width: 300px;">
							<NineRays:FlyTreeView ID="Tests" runat="server" DragDropAcceptNames="" Padding="3px"
								FadeEffect="True" BackColor="White" DragDropName="" DragDropMode="Move" BackgroundImage="images/export/beige_back.gif"
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
					</td>
					<td valign="top">
						<cc1:DragPanelExtender ID="extDragPanels" runat="server" DragHandleID="pnlHeader"
							TargetControlID="pnlPanels" />
						<asp:Panel ID="pnlPanels" runat="server" Width="550px">
							<asp:Panel ID="pnlHeader" runat="server" CssClass="PageTitle">
								You may drag this anywhere on the screen
							</asp:Panel>
							<asp:Panel runat="server" ID="pnlBody">
								<NineRays:FlyTreeView ID="Panels" runat="server" Padding="3px" FadeEffect="True"
									BackColor="White" DragDropName="" DragDropMode="Copy" BackgroundImage="images/export/beige_back.gif"
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
								<NineRays:FlyContextMenu ID="mnuTriggerGroup" runat="server" OnCommand="mnuTriggerGroupn_Command">
									<Items>
										<NineRays:FlyMenuItem Text="Remove Group from Trigger" CommandName="Remove" AutoPostBack="true" />
									</Items>
								</NineRays:FlyContextMenu>
							</asp:Panel>
						</asp:Panel>
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
									<asp:TextBox ID="txtPanelName" runat="server" Text="" />
									<asp:Label ID="lblPanelSort" runat="server" Text='Sort"' />
									<asp:TextBox ID="txtPanelSort" runat="server" Text="" /><br />
									<asp:Label ID="lblPnaelDescription" runat="server" Text="Panel Description" /><br />
									<obout:Editor PathPrefix="Editor_data/" ID="edPanelDescrip" runat="server" Appearance="custom"
										ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
										AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
										<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
										<TopToolbar Appearance="Lite" />
									</obout:Editor>
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
											<td align="left">
												<asp:Label ID="lblGroupTitle" runat="server" Text="Group Title" />
												<asp:TextBox ID="txtGroupTitle" runat="server" TabIndex="1" />
											</td>
											<td>
												<asp:Label ID="lblSortOrder" runat="server" Text="Sort Order" />
												<asp:TextBox ID="txtSortOrder" runat="server" TabIndex="2" />
											</td>
											<td>
												<asp:CheckBox ID="cboGraph" runat="server" Text="Show Graph" Checked='<%# Eval("ShowGraph") %>' />
											</td>
											<td>
												<asp:Label ID="lblChartBottom" runat="server" Text="Lowest Value" />
												<asp:TextBox ID="txtChartBottom" runat="server" TabIndex="2" />
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label ID="lblHigh" runat="server" Text="Male Longevity Range Upper" />
												<asp:TextBox ID="txtGroupHigh" runat="server" TabIndex="2" />
											</td>
											<td>
												<asp:Label ID="lblLow" runat="server" Text="Male Longevity Range Lower" />
												<asp:TextBox ID="txtGroupLow" runat="server" TabIndex="3" />
											</td>
											<td>
												<asp:Label ID="Label8" runat="server" Text="Female Longevity Range Upper" />
												<asp:TextBox ID="txtFemHigh" runat="server" TabIndex="4" />
											</td>
											<td>
												<asp:Label ID="Label9" runat="server" Text="Female Longevity Range Lower" />
												<asp:TextBox ID="txtFemLow" runat="server" TabIndex="5" />
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label ID="Label10" runat="server" Text="Male Normal Range Upper" />
												<asp:TextBox ID="txtNormHigh" runat="server" TabIndex="6" />
											</td>
											<td>
												<asp:Label ID="Label11" runat="server" Text="Male Normal Range Lower" />
												<asp:TextBox ID="txtNormLow" runat="server" TabIndex="7" />
											</td>
											<td>
												<asp:Label ID="Label12" runat="server" Text="Female Normal Range Upper" />
												<asp:TextBox ID="txtFemNormHigh" runat="server" TabIndex="8" />
											</td>
											<td>
												<asp:Label ID="Label13" runat="server" Text="Female Normal Range Lower" />
												<asp:TextBox ID="txtFemNormLow" runat="server" TabIndex="9" />
											</td>
										</tr>
										<tr>
											<td colspan="4">
												<asp:Label ID="lbl1" runat="server" Text="Description" />
												<obout:Editor PathPrefix="Editor_data/" ID="edDescrip" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label1" runat="server" Text="Male High Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="edHigh" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label2" runat="server" Text="Male Low Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="edLow" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label3" runat="server" Text="Male Normal Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="edNormal" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label14" runat="server" Text="Female High Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="FemHigh" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label15" runat="server" Text="Female Low Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="FemLow" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
												<br />
												<asp:Label ID="Label16" runat="server" Text="Female Normal Text" />
												<obout:Editor PathPrefix="Editor_data/" ID="FemNormal" runat="server" Appearance="custom"
													ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
													AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
													<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
													<TopToolbar Appearance="Lite" />
												</obout:Editor>
											</td>
										</tr>
									</table>
								</div>
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
									<obout:Editor PathPrefix="Editor_data/" ID="edTriggerDesc" runat="server" Appearance="custom"
										ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
										AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
										<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
										<TopToolbar Appearance="Lite" />
									</obout:Editor>
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
									<obout:Editor PathPrefix="Editor_data/" ID="edConditionDescrip" runat="server" Appearance="custom"
										ShowQuickFormat="false" DefaultFontFamily="Arial" DefaultFontSize="12" Submit="false"
										AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
										<BottomToolbar ShowHtmlTextCounter="false" ShowPlainTextCounter="true" />
										<TopToolbar Appearance="Lite" />
									</obout:Editor>
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
												<asp:ListItem Text=">=" />
												<asp:ListItem Text="<=" />
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
												<asp:ListItem Text=">=" />
												<asp:ListItem Text="<=" />
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
						</div>
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
