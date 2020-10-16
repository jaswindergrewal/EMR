<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="PresrcriptionList.aspx.cs" Inherits="PresrcriptionList" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.SuperForm" Assembly="obout_SuperForm" %>
<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Register TagPrefix="LMC" TagName="PatientInfo" Src="controls/PatientInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<%--<link href="resources/custom-styles/persist/style.css" rel="Stylesheet" type="text/css" />--%>
	<script type="text/javascript" src="Scripts/Scrips.js">
	</script>
	<style type="text/css">
		#grid-overlay
		{
			z-index: 100;
			position: absolute;
			top: 0px;
			left: 0px;
			bottom: 0px;
			right: 0px;
			background-color: #EEE;
			display: none;
			font-weight: bolder;
			opacity: 0.4;
			filter: alpha(opacity=40);
		}
		#overlay-supps
		{
			z-index: 100;
			position: absolute;
			top: 0px;
			left: 0px;
			bottom: 0px;
			right: 0px;
			background-color: #EEE;
			display: none;
			font-weight: bolder;
			opacity: 0.4;
			filter: alpha(opacity=40);
		}
	</style>
	<script type="text/javascript" language="javascript">
	    function printPharmSupp() {
	        var scripIds = "";
	        for (i = 0; i <= grdSupps.SelectedRecords.length - 1; i++) {
	            if (scripIds == "") {
	                scripIds += grdSupps.SelectedRecords[i].PresscriptionSuppID;
	            }
	            else {
	                scripIds += "," + grdSupps.SelectedRecords[i].PresscriptionSuppID;
	            }
	        }
	        if (scripIds == "") {
	            alert("You must check at least one check box for printing.");
	        }
	        else {
	            window.location = "PrescriptionSupp.aspx?PatientID=" + document.getElementById("txtPatientID").value + "&scripIds=" + scripIds + "&FullScreen=" + document.getElementById("txtFullScreen").value + "&Clinic=<%= Clinic %>&AptID=" + document.getElementById("txtAptID").value;
	        }
        }

        function printPharm() {
            var scripIds = "";
            for (i = 0; i <= grdScrips.SelectedRecords.length - 1; i++) {
                if (scripIds == "") {
                    scripIds += grdScrips.SelectedRecords[i].PrescriptionID;
                }
                else {
                    scripIds += "," + grdScrips.SelectedRecords[i].PrescriptionID;
                }
            }
            if (scripIds == "") {
                alert("You must check at least one check box for printing.");
            }
            else {
                window.location = "PrescriptionPharm.aspx?PatientID=" + document.getElementById("txtPatientID").value + "&scripIds=" + scripIds + "&FullScreen=" + document.getElementById("txtFullScreen").value + "&Clinic=<%= Clinic %>&AptID=" + document.getElementById("txtAptID").value;
            }
        }

        function printPrescriptionHistory() {

            window.location = "prescription_history.aspx?PatientID=" + document.getElementById("txtPatientID").value;

        }

        function supplementHistory() {
            window.location = "supplement_history.aspx?PatientID=" + document.getElementById("txtPatientID").value;
        }

     

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<table width="1050px">
				<tr>
					<td>
						<LMC:PatientInfo ID="PatInfo" runat="server" Width="1050px" />
					</td>
				</tr>
				<tr>
					<td>
						<cc1:TabContainer ID="tabScrips" runat="server" Width="1050px" CssClass="lmc_tab"
							ActiveTabIndex="0" OnClientActiveTabChanged="changeActiveTab">
							<cc1:TabPanel HeaderText="Drugs" runat="server" ID="pnlDrugs" BackColor="#EFE1C9">
								<ContentTemplate>
									<div class="persist" style="background-color: Transparent; padding-left: 0px;">
										<input type="hidden" id="txtPatientID" runat="server" clientidmode="Static" />
										<input type="hidden" id="txtStaffID" runat="server" clientidmode="Static" />
										<input type="hidden" id="txtAptID" runat="server" clientidmode="Static" />
										<input type="hidden" id="txtFullScreen" runat="server" clientidmode="Static" />
										<asp:CheckBox ID="chkLongevity" runat="server" Text="Include Longevity Prescriptions"
											CssClass="regText" Checked="True" AutoPostBack="True" OnCheckedChanged="CheckedChanged"
											Style="display: none;" />
										<asp:CheckBox ID="chkThirdParty" runat="server" Text="Include 3rd Party Prescriptions"
											CssClass="regText" AutoPostBack="True" OnCheckedChanged="CheckedChanged" Checked="true"
											Style="display: none;" />
										<obout:OboutButton runat="server" ID="btnPrintPharm" Text="Print for pharmacy" OnClientClick="printPharm();return false;"
											UseSubmitBehavior="False" FolderStyle="" />&nbsp;&nbsp;
										<obout:OboutButton runat="server" ID="btnPrintPatient" Text="Print all prescriptions"
											UseSubmitBehavior="False" OnClientClick="GetAutoship(); return false;" FolderStyle="" />&nbsp;&nbsp;
										<obout:OboutButton runat="server" ID="btnBack" Text="Back to Appointment" UseSubmitBehavior="False"
											OnClientClick="backToApt();" FolderStyle="" />
										<obout:OboutButton runat="server" ID="btnHistory" Text="Prescription History" UseSubmitBehavior="False"
											FolderStyle="" OnClientClick="printPrescriptionHistory();" />
										<div style="position: relative; width: 875px;">
											<obout:Grid ID="grdScrips" runat="server" ShowLoadingMessage="False" AllowPaging="False"
												PageSize="-1" AllowSorting="False" AutoGenerateColumns="False" AllowAddingRecords="False"
												AllowPageSizeSelection="False" Serialize="False" Width="200px" CellPadding="0"
												CellSpacing="0" FolderStyle="~/grid_styles/Style_5"  OnRebind="grdScrips_Rebind">
												<Columns>
													<obout:Column DataField="DrugID" Visible="false" />
													<obout:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" HeaderText="Print" Width="50" runat="server" Index="1"
														ShowHeaderCheckBox="false" />
													<obout:Column DataField="PrescriptionID" Visible="false" />
													<obout:Column DataField="Supplement_yn" Visible="False" HeaderText="Supplement_yn"
														Index="6" />
													<obout:Column HeaderText="Drug" DataField="DrugName" Width="185" Wrap="True" Index="7" />
													<obout:Column HeaderText="Sig" DataField="Drug_Dose" Width="175" Wrap="True" Index="8" />
													<obout:Column HeaderText="Disp" DataField="Drug_Dispenses" Width="45" Index="9" />
													<obout:Column HeaderText="Refills" DataField="Drug_NumbRefills" Width="60" Index="10" />
													<obout:Column HeaderText="Start Date" DataField="Drug_DatePrescibed" Width="80" Wrap="True"
														DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}"
														Index="11" />
													<obout:Column HeaderText="Stop Date" DataField="Drug_EndDate" Width="80" Wrap="True"
														DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}"
														Index="12" />
													<obout:Column HeaderText="Prescribed by" DataField="Writer" Width="100" Wrap="True"
														Index="13" />
													<obout:Column HeaderText="RePrescription" DataField="RePre_yn" Width="100" Index="14" />
													<obout:Column HeaderText="Notes" DataField="Notes" Width="150" Wrap="True" Index="15" />
													<obout:Column DataField="ThirdParty" Visible="false" />
												</Columns>
												<TemplateSettings RowEditTemplateId="tplRowEdit" />
												<ScrollingSettings EnableVirtualScrolling="True" />
												<%--<CssSettings CSSFolderImages="resources/custom-styles/persist" />--%>
												<PagingSettings ShowRecordsCount="False" />
												<ClientSideEvents OnClientDblClick="onDoubleClickCurrent" OnBeforeClientEdit="grdScrips_ClientEdit"
													ExposeSender="True" />
												<Templates>
													<obout:GridTemplate runat="server" ID="TemplateWithCheckbox">
														<Template>
															<asp:CheckBox runat="server" ID="ChkID" ToolTip="<%# Container.Value %>" />
														</Template>
													</obout:GridTemplate>
													<obout:GridTemplate runat="server" ID="tplRowEdit" ControlID="" ControlPropertyName="">
														<Template>
															<table class="rowEditTable" cellpadding="6" cellspacing="6">
																<tr>
																	<td valign="top" style="width: 100px;" align="right">
																		Sig
																	</td>
																	<td>
																		<asp:TextBox ID="txtSigRefill" runat="server" Columns="150" CssClass="regText" ClientIDMode="Static" />
																	</td>
																</tr>
																<tr>
																	<td align="right">
																		# Dispensed
																	</td>
																	<td>
																		<asp:TextBox ID="txtDispRefill" runat="server" CssClass="regText" ClientIDMode="Static" />
																	</td>
																</tr>
																<tr>
																	<td align="right">
																		# of Refills
																	</td>
																	<td>
																		<asp:TextBox ID="txtRefillsRefill" runat="server" CssClass="regText" ClientIDMode="Static"
																			Text="0" />
																	</td>
																</tr>
																<tr>
																	<td align="right">
																		Date Prescribed
																	</td>
																	<td>
																		<asp:TextBox ID="txtStartDateRefill" runat="server" CssClass="regText" ClientIDMode="Static"  />
																		<obout:Calendar ID="calStartDateRefill" runat="server" DatePickerMode="true" TextBoxId="txtStartDateRefill"
																			DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																			AutoPostBack="false" />
																	</td>
																</tr>
																<tr>
																	<td align="right">
																		Date End
																	</td>
																	<td>
																		<asp:TextBox ID="txtDateEndRefill" runat="server" CssClass="regText" ClientIDMode="Static" />
																		<obout:Calendar ID="calDateEndRefill" runat="server" DatePickerMode="true" TextBoxId="txtDateEndRefill"
																			DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																			AutoPostBack="false" />
																	</td>
																</tr>
																<tr>
																	<td align="right" valign="top">
																		Notes
																	</td>
																	<td>
																		<asp:TextBox ID="txtNotesRefill" runat="server" TextMode="MultiLine" Rows="3" Columns="20"
																			CssClass="regText" ClientIDMode="Static" />
																	</td>
																</tr>
																<tr>
																	<td colspan="2" align="center">
																		<obout:OboutButton ID="btnRefill" runat="server" Text="Refill" Width="85" OnClientClick="grdSrips_ClientClick(); return false;"
																			UseSubmitBehavior="false" ClientIDMode="Static" />
																		<obout:OboutButton ID="btnClosePrescrip" runat="server" Text="Close" Width="80" OnClientClick="btnClosePrescrip_ClientClick(); return false;"
																			UseSubmitBehavior="false" />
																		<obout:OboutButton ID="btnDeletePrescrip" runat="server" Text="Delete" Width="80"
																			OnClientClick="btnDeletePrescrip_ClientClick();return false;" UseSubmitBehavior="false" />
																	</td>
																</tr>
															</table>
														</Template>
													</obout:GridTemplate>
												</Templates>
											</obout:Grid>
											<div id="grid-overlay">
												Please wait . . .</div>
										</div>
									</div>
									<br />
									<cc1:CollapsiblePanelExtender ID="collNew" runat="server" TargetControlID="pnlNew"
										ExpandControlID="pnlNewTitle" CollapseControlID="pnlNewTitle" TextLabelID="Label2"
										CollapsedText="Click to add new prescriptions" ExpandedText="Click to add new prescriptions"
										Collapsed="True" SuppressPostBack="True" ImageControlID="Image2" ExpandedImage="~/images/collapse.jpg"
										CollapsedImage="~/images/expand.jpg" Enabled="True">
									</cc1:CollapsiblePanelExtender>
									<asp:Panel ID="pnlNewTitle" runat="server" CssClass="border" BackColor="#D6B781">
										<asp:Image ID="Image2" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0px">
										</asp:Image>
										<asp:Label ID="Label2" runat="server" Text="New Prescription" CssClass="regText"></asp:Label>
									</asp:Panel>
									<asp:Panel ID="pnlNew" runat="server">
										<br />
										<table>
											<tr>
												<td>
													<span id="WindowPositionHelper"></span>
													<div class="persist" style="background-color: Transparent; padding-left: 0px;">
														<obout:Grid ID="grdNew" runat="server" AutoGenerateColumns="False" CellPadding="6"
															CellSpacing="6" AllowRecordSelection="False" AllowColumnResizing="False" AllowAddingRecords="False"
															AllowFiltering="True" AllowPageSizeSelection="true" AllowPaging="true" PageSize="10"
															FolderStyle="~/grid_styles/Style_7" OnRebind="grdNew_Rebind" Width="570px">
															<Columns>
																<obout:Column ID="colDrug" HeaderText="Drug" DataField="DrugName" Width="570" Wrap="True"
																	ShowFilterCriterias="False" FilterTemplateId="NameFilter" Index="0">
																	<TemplateSettings FilterTemplateId="NameFilter" />
																	<FilterOptions>
																		<obout:FilterOption IsDefault="True" Type="Contains" />
																	</FilterOptions>
																</obout:Column>
																<obout:Column DataField="DrugID" Visible="False" HeaderText="DrugID" Index="1" />
															</Columns>
															<FilteringSettings InitialState="Visible" FilterPosition="Top" />
															<PagingSettings ShowRecordsCount="False" />
															<ClientSideEvents OnBeforeClientEdit="grdNew_ClientEdit" OnClientDblClick="onDoubleClick"
																ExposeSender="True" />
															<Templates>
																<obout:GridTemplate runat="server" ID="NameFilter" ControlID="Name" ControlPropertyName="">
																	<Template>
																		<obout:OboutTextBox runat="server" ID="Name" Width="300px" autocomplete="off">
													<ClientSideEvents OnKeyUp="applyFilter" />
																		</obout:OboutTextBox>
																	</Template>
																</obout:GridTemplate>
															</Templates>
														</obout:Grid>
														<owd:Window ID="NewDrug" runat="server" IsModal="True" ShowCloseButton="True" Status=""
															RelativeElementID="grdNew" Top="0" Left="100" Height="470" Width="851" VisibleOnLoad="False"
															StyleFolder="wdstyles/grandgray" Title="Add Prescription" DebugMode="True" EnableClientSideControl="False"
															IconPath="" InitialMode="NORMAL" IsDraggable="True" IsResizable="True" MinHeight="0"
															MinWidth="0" OnClientClose="" OnClientDrag="" OnClientInit="" OnClientOpen=""
															OnClientPreClose="" OnClientPreOpen="" OnClientResize="" Opacity="100" Overflow="HIDDEN"
															PageOpacity="25" ShowMaximizeButton="False" ShowStatusBar="True">
															<div class="super-form">
																<obout:SuperForm ID="SuperFormNewDrug" runat="server" AutoGenerateRows="False" DataKeyNames="OrderID"
																	DefaultMode="Insert" Width="825px" AllowDataKeysInsert="False" GridLines="None"
																	HorizontalAlign="Center">
																	<Fields>
																		<obout:TemplateField>
																			<EditItemTemplate>
																				<input type="hidden" id="txtDrugID" runat="server" clientidmode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField>
																			<EditItemTemplate>
																				<asp:CheckBox ID="chkThirdParty" runat="server" Text="3rd Party Prescription? " ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField>
																			<EditItemTemplate>
																				Drug
																				<input type="text" id="txtDrugNameLocal" value='' style="border-style: none;" size="50"
																					class="regText" clientidmode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Sig
																				<asp:TextBox ID="txtSig" runat="server" Columns="150" CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				# Dispensed
																				<asp:TextBox ID="txtDisp" runat="server" CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				# of Refills
																				<asp:TextBox ID="txtRefill" runat="server" CssClass="regText" ClientIDMode="Static"
																					Text="0" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Date Prescribed
																				<asp:TextBox ID="txtStartDate" runat="server" CssClass="regText" ClientIDMode="Static" />
																				<obout:Calendar ID="calStartDate" runat="server" DatePickerMode="true" TextBoxId="txtStartDate"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				End Date
																				<asp:TextBox ID="txtEndDate" runat="server" CssClass="regText" ClientIDMode="Static" />
																				<obout:Calendar ID="calEndDate" runat="server" DatePickerMode="true" TextBoxId="txtEndDate"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Notes<br />
																				<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="3" Columns="40"
																					CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet4">
																			<EditItemTemplate>
																				<obout:OboutButton ID="OboutButton1" runat="server" Text="Save" OnClientClick="saveChanges(); return true;"
																					Width="75" UseSubmitBehavior="false" />
																				<obout:OboutButton ID="OboutButton2" runat="server" Text="Cancel" OnClientClick="cancelChanges(); return false;"
																					Width="75" UseSubmitBehavior="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																	</Fields>
																	<CommandRowStyle HorizontalAlign="Center" />
																	<FieldHeaderStyle HorizontalAlign="Right" Width="100px" />
																	<PagerStyle HorizontalAlign="Center" />
																</obout:SuperForm>
															</div>
														</owd:Window>
													</div>
												</td>
												<td valign="top">
													<input type="button" class="button" value='Add Drug' onclick="AddDrugWin(); return false;" />
													<owd:Window ID="AddDrugWindow" runat="server" IsModal="True" ShowCloseButton="True"
														Status="" RelativeElementID="grdNew" Top="0" Left="100" Height="550" Width="851"
														VisibleOnLoad="False" StyleFolder="wdstyles/grandgray" Title="Add Prescription"
														DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL"
														IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose=""
														OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen=""
														OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" ShowMaximizeButton="False"
														ShowStatusBar="True">
														<div >
															<obout:SuperForm ID="SuperForm1" runat="server" AutoGenerateRows="False" DataKeyNames="OrderID"
																DefaultMode="Insert" Width="825px" Height="100%" AllowDataKeysInsert="False"
																GridLines="None" HorizontalAlign="Center">
																<Fields>
																	<obout:TemplateField>
																		<EditItemTemplate>
																			Drug Name
																			<asp:TextBox ID="txtNewDrugName" runat="server" Columns="50" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField>
																		<EditItemTemplate>
																			<asp:CheckBox ID="chkThirdPartyAdd" runat="server" Text="3rd Party Prescription? "
																				ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Sig
																			<asp:TextBox ID="txtSigAdd" runat="server" Columns="100" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			# Dispensed
																			<asp:TextBox ID="txtDispAdd" runat="server" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			# of Refills
																			<asp:TextBox ID="txtRefillAdd" runat="server" ClientIDMode="Static" Text="0" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Date Prescribed
																			<asp:TextBox ID="txtStartDateAdd" runat="server" ClientIDMode="Static" />
																		<%--	<cc1:CalendarExtender ID="calStartAdd" runat="server" TargetControlID="txtStartDateAdd" />--%>
                                                                            <obout:Calendar ID="calStartDate" runat="server" DatePickerMode="true" TextBoxId="txtStartDateAdd"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			End Date
																			<asp:TextBox ID="txtEndDateAdd" runat="server" ClientIDMode="Static" />
																			<%--<cc1:CalendarExtender ID="calEndAdd" runat="server" TargetControlID="txtEndDateAdd" />--%>
                                                                            <obout:Calendar ID="calEndDate" runat="server" DatePickerMode="true" TextBoxId="txtEndDateAdd"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Notes<br />
																			<asp:TextBox ID="txtNotesAdd" runat="server" TextMode="MultiLine" Rows="3" Columns="40"
																				ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet4">
																		<EditItemTemplate>
																			<obout:OboutButton ID="btnOkAddAdrug" runat="server" Text="Save" OnClientClick="saveChangesAdd(); return true;"
																				Width="75" UseSubmitBehavior="false" />
																			<obout:OboutButton ID="btnCancelAddAdrug" runat="server" Text="Cancel" OnClientClick="cancelChangesAdd(); return false;"
																				Width="75" UseSubmitBehavior="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
                                                                    <obout:TemplateField>
																		
																	</obout:TemplateField>
                                                                     <obout:TemplateField>
																		
																	</obout:TemplateField>
                                                                     <obout:TemplateField>
																		
																	</obout:TemplateField>
																</Fields>
																<CommandRowStyle HorizontalAlign="Center" />
																<FieldHeaderStyle HorizontalAlign="Right" Width="100px" />
																<PagerStyle HorizontalAlign="Center" />
                                                                
															</obout:SuperForm>
                                                            
														</div>
													</owd:Window>
												</td>
											</tr>
										</table>
									</asp:Panel>
									<br />
								</ContentTemplate>
							</cc1:TabPanel>
							<cc1:TabPanel HeaderText="Closed Prescriptions" runat="server" ID="CloseScrips" BackColor="#EFE1C9">
								<ContentTemplate>
									<asp:Panel ID="pnlHist" runat="server">
										<div class="persist" style="background-color: Transparent; padding-left: 0px;">
											<obout:Grid ID="grdHist" runat="server" ShowLoadingMessage="False" AutoGenerateColumns="False"
												AllowAddingRecords="False" FolderStyle="~/grid_styles/Style_7" AllowFiltering="True"
												AllowRecordSelection="False" AllowColumnResizing="False" AllowPageSizeSelection="true"
												OnRebind="grdHist_Rebind">
												<Columns>
													<obout:Column ID="Column1" HeaderText="Drug" DataField="DrugName" Width="185" Wrap="True"
														ShowFilterCriterias="False" FilterTemplateId="NameFilterHist" Index="0">
														<TemplateSettings FilterTemplateId="NameFilterHist" />
														<FilterOptions>
															<obout:FilterOption IsDefault="True" Type="Contains" />
														</FilterOptions>
													</obout:Column>
													<obout:Column ID="Column2" HeaderText="Sig" DataField="Drug_Dose" Width="225" Wrap="True"
														Index="1" AllowFilter="false" />
													<obout:Column ID="Column3" HeaderText="Disp" DataField="Drug_Dispenses" Width="45"
														Index="2" AllowFilter="false" />
													<obout:Column ID="Column4" HeaderText="Refills" DataField="Drug_NumbRefills" Width="60"
														Index="3" AllowFilter="false" />
													<obout:Column ID="Column5" HeaderText="Start Date" DataField="Drug_DatePrescibed"
														Width="75" Wrap="True" DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}"
														Index="4" AllowFilter="false" />
													<obout:Column ID="Column6" HeaderText="Close Date" DataField="Drug_EndDate" Width="75"
														Wrap="True" DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}"
														Index="5" AllowFilter="false" />
													<obout:Column ID="Column7" HeaderText="Prescribed by" DataField="Writer" Width="100"
														Wrap="True" Index="6" AllowFilter="false" />
													<obout:Column ID="Column8" HeaderText="RePrescription" DataField="RePre_yn" Width="100"
														Index="7" AllowFilter="false" />
													<obout:Column ID="Column9" HeaderText="Notes" DataField="Notes" Width="150" Wrap="True"
														Index="8" AllowFilter="false" />
												</Columns>
												<FilteringSettings InitialState="Visible" FilterPosition="Top" />
												<PagingSettings ShowRecordsCount="False" />
												<Templates>
													<obout:GridTemplate runat="server" ID="NameFilterHist" ControlID="NameHist" ControlPropertyName="">
														<Template>
															<obout:OboutTextBox runat="server" ID="NameHist" Width="150px" autocomplete="off">
													<ClientSideEvents OnKeyUp="applyFilterHist" />
															</obout:OboutTextBox>
														</Template>
													</obout:GridTemplate>
												</Templates>
											</obout:Grid>
										</div>
									</asp:Panel>
								</ContentTemplate>
							</cc1:TabPanel>
							<cc1:TabPanel HeaderText="Supplements" runat="server" ID="pnlSuppd" BackColor="#EFE1C9">
								<ContentTemplate>
									<span id="WindowPositionHelperSupp"></span>
									<input type="hidden" id="txtScripData" />
									<obout:OboutButton runat="server" ID="btnPrintSupp" Text="Print for pharmacy" OnClientClick="printPharmSupp();return false;"
										UseSubmitBehavior="false" />&nbsp;&nbsp;
									<obout:OboutButton runat="server" ID="btnPrintPatientSupp" Text="Print all prescriptions"
										UseSubmitBehavior="False" OnClientClick="GetAutoship(); return false;" FolderStyle="" />
									<obout:OboutButton runat="server" ID="btnBackSupp" Text="Back to Appointment" UseSubmitBehavior="false"
										OnClientClick="backToApt();" Visible="true" />
									<obout:OboutButton runat="server" ID="btnHistorySupp" Text="Supplement History" UseSubmitBehavior="False"
										FolderStyle="" OnClientClick="supplementHistory();" />
									<div class="persist" style="background-color: Transparent; padding-left: 0px;">
										<obout:Grid ID="grdSupps" runat="server" ShowLoadingMessage="false" AllowPaging="false"
											PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="false"
											AllowPageSizeSelection="false" Serialize="false" Width="110px" CellPadding="0" FolderStyle="~/grid_styles/Style_7"
											CallbackMode="true" CellSpacing="0" 
											OnRebind="grdSupps_Rebind">
											<Columns>
												<obout:Column DataField="ProductID" Visible="false" />
												<obout:CheckBoxSelectColumn ID="CheckBoxSelectColumn2" ReadOnly="true" HeaderText="Print" Width="50" runat="server"
													Index="1" ShowHeaderCheckBox="false">
												</obout:CheckBoxSelectColumn>
												<obout:Column DataField="PresscriptionSuppID" Visible="false" />
												<obout:Column HeaderText="Supplement" DataField="SuppName" Width="185px" Wrap="true" />
												<obout:Column HeaderText="Sig" DataField="SuppDose" Width="175px" Wrap="true" />
												<obout:Column HeaderText="Disp" DataField="SuppDispenses" Width="45px" />
												<obout:Column HeaderText="Refills" DataField="SuppNumbRefills" Width="60px" />
												<obout:Column HeaderText="Start Date" DataField="SuppDatePrescibed" Width="80px"
													Wrap="true" DataFormatString="{0:MM/dd/yyyy}" />
												<obout:Column HeaderText="Stop Date" DataField="SuppEndDate" Width="80px" Wrap="true"
													DataFormatString="{0:MM/dd/yyyy}" />
												<obout:Column HeaderText="Prescribed by" DataField="Writer" Width="100px" Wrap="true" />
												<obout:Column HeaderText="RePrescription" DataField="RePre_yn" Width="100px" />
												<obout:Column HeaderText="Notes" DataField="Notes" Width="150px" Wrap="true" />
											</Columns>
											<TemplateSettings RowEditTemplateId="tplRowEditSupp" />
											<ScrollingSettings EnableVirtualScrolling="true" />
											<%--<CssSettings CSSFolderImages="resources/custom-styles/persist" />--%>
											<PagingSettings ShowRecordsCount="false" />
											<ClientSideEvents OnClientDblClick="onDoubleClickCurrentSupp" OnBeforeClientEdit="grdSupps_ClientEdit"
												ExposeSender="true" />
											<Templates>
												<obout:GridTemplate runat="server" ID="TemplateWithCheckboxSupp">
													<Template>
														<asp:CheckBox runat="server" ID="ChkID" ToolTip="<%# Container.Value %>" />
													</Template>
												</obout:GridTemplate>
												<obout:GridTemplate runat="server" ID="tplRowEditSupp">
													<Template>
														<table class="rowEditTable" cellpadding="6" cellspacing="6">
															<tr>
																<td valign="top" style="width: 100px;" align="right">
																	Sig
																</td>
																<td>
																	<asp:TextBox ID="txtSigRefillSupp" runat="server" Columns="100" CssClass="regText"
																		ClientIDMode="Static" />
																</td>
															</tr>
															<tr>
																<td align="right">
																	# Dispensed
																</td>
																<td>
																	<asp:TextBox ID="txtDispRefillSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																</td>
															</tr>
															<tr>
																<td align="right">
																	# of Refills
																</td>
																<td>
																	<asp:TextBox ID="txtRefillsRefillSupp" runat="server" CssClass="regText" ClientIDMode="Static"
																		Columns="150" />
																</td>
															</tr>
															<tr>
																<td align="right">
																	Date Prescribed
																</td>
																<td>
																	<asp:TextBox ID="txtStartDateRefillSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																	<obout:Calendar ID="calStartDateRefillSupp" runat="server" DatePickerMode="true"
																		TextBoxId="txtStartDateRefillSupp" DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																		AutoPostBack="false" />
																</td>
															</tr>
															<tr>
																<td align="right">
																	Date End
																</td>
																<td>
																	<asp:TextBox ID="txtDateEndRefillSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																	<obout:Calendar ID="calDateEndRefillSupp" runat="server" DatePickerMode="true" TextBoxId="txtDateEndRefillSupp"
																		DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																		AutoPostBack="false" />
																</td>
															</tr>
															<tr>
																<td align="right" valign="top">
																	Notes
																</td>
																<td>
																	<asp:TextBox ID="txtNotesRefillSupp" runat="server" TextMode="MultiLine" Rows="3"
																		Columns="20" CssClass="regText" ClientIDMode="Static" />
																</td>
															</tr>
															<tr>
																<td colspan="2" align="center">
																	<obout:OboutButton ID="btnRefillSupp" runat="server" Text="Refill" Width="85" OnClientClick="grdSupps_ClientClick(); return false;"
																		UseSubmitBehavior="false" />
																	<obout:OboutButton ID="btnClosePrescripSupp" runat="server" Text="Close" Width="80"
																		OnClientClick="btnClosePrescripSupp_ClientClick(); return false;" UseSubmitBehavior="false" />
																	<obout:OboutButton ID="btnDeletePrescripSupp" runat="server" Text="Delete" Width="80"
																		OnClientClick="btnDeletePrescripSupp_ClientClick();return false;" UseSubmitBehavior="false" />
																</td>
															</tr>
														</table>
													</Template>
												</obout:GridTemplate>
											</Templates>
										</obout:Grid>
										<div id="overlay-supps">
											Please wait . . .</div>
									</div>
									<br />
									<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlNewSupp"
										ExpandControlID="pnlNewTitleSupp" CollapseControlID="pnlNewTitleSupp" TextLabelID="Label3"
										CollapsedText="Click to show New Supplement" ExpandedText="Click to hide New Supplements"
										Collapsed="true" SuppressPostBack="true" ImageControlID="Image3" ExpandedImage="~/images/collapse.jpg"
										CollapsedImage="~/images/expand.jpg">
									</cc1:CollapsiblePanelExtender>
									<asp:Panel ID="pnlNewTitleSupp" runat="server" CssClass="border" BackColor="#D6B781">
										<asp:Image ID="Image3" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0">
										</asp:Image>
										<asp:Label ID="Label3" runat="server" Text="New Supplement" CssClass="regText">
										</asp:Label>
									</asp:Panel>
									<asp:Panel ID="pnlNewSupp" runat="server">
										<br />
										<table>
											<tr>
												<td>
													<div class="persist" style="background-color: Transparent; padding-left: 0px;">
														<obout:Grid ID="grdNewSupp" runat="server" AutoGenerateColumns="false" AllowPaging="true"
															PageSize="10" CellPadding="6" CellSpacing="6" CallbackMode="true" Serialize="true"
															AllowRecordSelection="false" AllowGrouping="false" AllowColumnResizing="false"
															AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="true"
															FolderStyle="~/grid_styles/Style_7" OnRebind="grdNewSupp_Rebind">
															<Columns>
																<obout:Column ID="colSupp" runat="server" HeaderText="Supplement" DataField="ProductName"
																	Width="570" Wrap="true" ShowFilterCriterias="false">
																	<TemplateSettings FilterTemplateId="NameFilterSupp" />
																	<FilterOptions>
																		<obout:FilterOption IsDefault="true" Type="Contains" />
																	</FilterOptions>
																</obout:Column>
																<obout:Column DataField="ProductID" Visible="false" />
															</Columns>
															<FilteringSettings InitialState="Visible" FilterPosition="Top" />
															<ScrollingSettings EnableVirtualScrolling="false" />
															<PagingSettings ShowRecordsCount="false" />
															<ClientSideEvents OnBeforeClientEdit="grdNewSupp_ClientEdit" OnClientDblClick="onDoubleClickSupp"
																ExposeSender="true" />
															<Templates>
																<obout:GridTemplate runat="server" ID="NameFilterSupp" ControlID="Name">
																	<Template>
																		<obout:OboutTextBox runat="server" ID="Name" Width="300px" autocomplete="off">
																			<ClientSideEvents OnKeyUp="applyFilterSupp" />
																		</obout:OboutTextBox>
																	</Template>
																</obout:GridTemplate>
															</Templates>
														</obout:Grid>
														<owd:Window ID="newSupp" runat="server" IsModal="true" ShowCloseButton="true" Status=""
															RelativeElementID="grdSupps" Top="0" Left="100" Height="470" Width="851" VisibleOnLoad="false"
															StyleFolder="wdstyles/grandgray" Title="Add Prescription">
															<div class="super-form">
																<obout:SuperForm ID="SuperForm2" runat="server" AutoGenerateRows="false" AutoGenerateInsertButton="false"
																	AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" DataKeyNames="OrderID"
																	DefaultMode="Insert" Width="825">
																	<Fields>
																		<obout:TemplateField>
																			<EditItemTemplate>
																				<input type="hidden" id="txtDrugIDSupp" runat="server" clientidmode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField>
																			<EditItemTemplate>
																				Supplement
																				<input type="text" id="txtDrugNameLocalSupp" value='' style="border-style: none;"
																					size="50" clientidmode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Sig
																				<asp:TextBox ID="txtSigSupp" runat="server" Columns="100" CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				# Dispensed
																				<asp:TextBox ID="txtDispSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				# of Refills
																				<asp:TextBox ID="txtRefillSupp" runat="server" CssClass="regText" ClientIDMode="Static"
																					Text="0" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Date Prescribed
																				<asp:TextBox ID="txtStartDateSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																			 <%-- <cc1:CalendarExtender ID="calsuppStartDateAdd" runat="server" TargetControlID="txtStartDateSupp" />--%>
																	<obout:Calendar ID="calsuppStartDateAdd" runat="server" DatePickerMode="true" TextBoxId="txtStartDateSupp"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				End Date
																				<asp:TextBox ID="txtEndDateSupp" runat="server" CssClass="regText" ClientIDMode="Static" />
																				<%-- <cc1:CalendarExtender ID="calsuppEndDateAdd" runat="server" TargetControlID="txtEndDateSupp" />--%>
                                                                                <obout:Calendar ID="calsuppEndDateAdd" runat="server" DatePickerMode="true" TextBoxId="txtEndDateSupp"
																					DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																					AutoPostBack="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet1">
																			<EditItemTemplate>
																				Notes<br />
																				<asp:TextBox ID="txtNotesSupp" runat="server" TextMode="MultiLine" Rows="3" Columns="40"
																					CssClass="regText" ClientIDMode="Static" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																		<obout:TemplateField FieldSetID="FieldSet4">
																			<EditItemTemplate>
																				<obout:OboutButton ID="OboutButton1Supp" runat="server" Text="Save" OnClientClick="saveChangesSupp(); return true;"
																					Width="75" UseSubmitBehavior="false" />
																				<obout:OboutButton ID="OboutButton2Supp" runat="server" Text="Cancel" OnClientClick="cancelChangesSupp(); return false;"
																					Width="75" UseSubmitBehavior="false" />
																			</EditItemTemplate>
																		</obout:TemplateField>
																	</Fields>
																</obout:SuperForm>
															</div>
														</owd:Window>
													</div>
												</td>
												<td valign="top">
													<input type="button" class="button" value='Add Supplement' onclick="AddDrugWinSupp(); return false;" />
													<owd:Window ID="AddNewSupp" runat="server" IsModal="true" ShowCloseButton="true"
														Status="" RelativeElementID="WindowPositionHelperSupp" Top="0" Left="100" Height="470"
														Width="851" VisibleOnLoad="false" StyleFolder="wdstyles/grandgray" Title="Add Supplement">
														<div class="super-form">
															<obout:SuperForm ID="AddDrugWindowSupp" runat="server" AutoGenerateRows="false" AutoGenerateInsertButton="false"
																AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" DataKeyNames="OrderID"
																DefaultMode="Insert" Width="825" Height="100%">
																<Fields>
																	<obout:TemplateField>
																		<EditItemTemplate>
																			Supplement Name
																			<asp:TextBox ID="txtNewDrugNameSupp" runat="server" Columns="50" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Sig
																			<asp:TextBox ID="txtSigAddSupp" runat="server" Columns="100" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			# Dispensed
																			<asp:TextBox ID="txtDispAddSupp" runat="server" ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			# of Refills
																			<asp:TextBox ID="txtRefillAddSupp" runat="server" ClientIDMode="Static" Text="0" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Date Prescribed
																			<asp:TextBox ID="txtStartDateAddSupp" runat="server" ClientIDMode="Static" />
																			<obout:Calendar ID="calStartDateSupp1" runat="server" DatePickerMode="true" TextBoxId="txtStartDateAddSupp"
																				DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																				AutoPostBack="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			End Date
																			<asp:TextBox ID="txtEndDateAddSupp" runat="server" ClientIDMode="Static" />
																			<obout:Calendar ID="calEndDateSupp1" runat="server" DatePickerMode="true" TextBoxId="txtEndDateAddSupp"
																				DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
																				AutoPostBack="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet1">
																		<EditItemTemplate>
																			Notes<br />
																			<asp:TextBox ID="txtNotesAddSupp" runat="server" TextMode="MultiLine" Rows="3" Columns="40"
																				ClientIDMode="Static" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																	<obout:TemplateField FieldSetID="FieldSet4">
																		<EditItemTemplate>
																			<obout:OboutButton ID="btnOkAddAdrugSupp" runat="server" Text="Save" OnClientClick="saveChangesAddSupp(); return true;"
																				Width="75" UseSubmitBehavior="false" />
																			<obout:OboutButton ID="btnCancelAddAdrugSupp" runat="server" Text="Cancel" OnClientClick="cancelChangesAddSupp(); return false;"
																				Width="75" UseSubmitBehavior="false" />
																		</EditItemTemplate>
																	</obout:TemplateField>
																</Fields>
															</obout:SuperForm>
														</div>
													</owd:Window>
												</td>
											</tr>
										</table>
									</asp:Panel>
									<br />
								</ContentTemplate>
							</cc1:TabPanel>
							<cc1:TabPanel HeaderText="Closed Supplements" runat="server" ID="CloseSupps" BackColor="#EFE1C9">
								<ContentTemplate>
									<asp:Panel ID="pnlHistSupp" runat="server">
										<div class="persist" style="background-color: Transparent; padding-left: 0px;">
											<obout:Grid ID="grdHistSupp" runat="server" ShowLoadingMessage="false" AllowPaging="true"
												PageSize="10" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="false"
												AllowColumnReordering="true" AllowPageSizeSelection="true" Serialize="true" CallbackMode="true"
												FolderStyle="~/grid_styles/Style_7" AllowFiltering="true" OnRebind="grdHistSupp_Rebind">
												<Columns>
													<obout:Column HeaderText="Supplement" DataField="SuppName" Width="185px" Wrap="true" />
													<obout:Column HeaderText="Sig" DataField="SuppDose" Width="225px" Wrap="true" />
													<obout:Column HeaderText="Disp" DataField="SuppDispenses" Width="45px" />
													<obout:Column HeaderText="Refills" DataField="SuppNumbRefills" Width="60px" />
													<obout:Column HeaderText="Start Date" DataField="SuppDatePrescibed" Width="75px"
														Wrap="true" DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}" Index="11" />                                                    
													<obout:Column HeaderText="Stop Date" DataField="SuppEndDate" Width="75px" Wrap="true"
														DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}" Index="11" />
													<obout:Column HeaderText="Prescribed by" DataField="Writer" Width="100px" Wrap="true" />
													<obout:Column HeaderText="RePrescription" DataField="RePre_yn" Width="100px" />
													<obout:Column HeaderText="Notes" DataField="Notes" Width="150px" Wrap="true" />
												</Columns>
												<ScrollingSettings EnableVirtualScrolling="true" />
												<PagingSettings ShowRecordsCount="false" />
											</obout:Grid>
										</div>
									</asp:Panel>
								</ContentTemplate>
							</cc1:TabPanel>
						</cc1:TabContainer>
					</td>
				</tr>
			</table>
			<owd:Window ID="CheckAutoship" runat="server" IsModal="true" ShowCloseButton="true"
				Status="" VisibleOnLoad="false" StyleFolder="wdstyles/grandgray" Title="Select those items that will be auto shipped."
				Opacity="100" Height="400" Width="360">
				<div class="super-form">
					<div class="persist" style="background-color: Transparent; padding-left: 0px; overflow: auto;">
						<table width="350">
							<tr>
								<td align="center">
									<obout:Grid ID="grdAutoship" runat="server" ShowLoadingMessage="false" AllowPaging="false"
										PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="false"
										AllowColumnReordering="false" AllowPageSizeSelection="false" Serialize="true"
										CallbackMode="true" FolderStyle="~/grid_styles/Style_7" AllowFiltering="false"
										OnRebind="grdAutoship_Rebind" ShowFooter="false">
										<Columns>
											<obout:CheckBoxSelectColumn HeaderText="Select All" Width="100" ControlType="Standard" />
											<obout:Column DataField="ProductName" HeaderText="Supplement" Width="250" />
											<obout:Column DataField="PrescriptionSuppID" Visible="false" />
										</Columns>
										<LocalizationSettings NoRecordsText="No changes needed." />
									</obout:Grid>
								</td>
							</tr>
							<tr>
								<td align="center">
									<obout:OboutButton runat="server" ID="OboutButton3" Text="Continue" UseSubmitBehavior="False"
										OnClientClick="btnPrintPatient_Click(); return false;" FolderStyle="" />
								</td>
							</tr>
						</table>
					</div>
			</owd:Window>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
