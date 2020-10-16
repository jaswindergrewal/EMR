<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="Admin_LabReminders.aspx.cs" Inherits="Admin_LabReminders" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<script type="text/javascript">
		var applyFilterTimeout = null;
		function applyFilter() {
			if (applyFilterTimeout) {
				window.clearTimeout(applyFilterTimeout);
			}

			applyFilterTimeout = window.setTimeout(doFiltering, 1000);
		}
		function doFiltering() {
			grdSymptomLab.filter();
		}
		var applyFilterTimeoutDiag = null;
		function applyFilterDiag() {
			if (applyFilterTimeoutDiag) {
				window.clearTimeout(applyFilterTimeoutDiag);
			}

			applyFilterTimeout = window.setTimeout(doFilteringDiag, 1000);
		}
		function doFilteringDiag() {
			grdDiagnosisLab.filter();
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:UpdateProgress ID="prog1" runat="server" AssociatedUpdatePanelID="upd1" DisplayAfter="0">
		<ProgressTemplate>
			<h1>
				Please wait . . .</h1>
		</ProgressTemplate>
	</asp:UpdateProgress>
	<asp:UpdatePanel ID="upd1" runat="server">
		<ContentTemplate>
			<cc1:TabContainer ID="tabScrips" runat="server" Width="800px" CssClass="lmc_tab"
				ActiveTabIndex="0">
				<cc1:TabPanel HeaderText="Symtptoms" runat="server" ID="pnlSymptom" BackColor="#EFE1C9">
					<ContentTemplate>
						<p class="regText">
							Symptom
							<asp:DropDownList ID="ddlSymptom" runat="server"  DataTextField="SymptomName"
								DataValueField="SymptomID" ClientIDMode="Static" OnSelectedIndexChanged="ddlSymptom_SelectedIndexChanged"
								AutoPostBack="True" />
							<br />
							<br />
							<asp:Button ID="btnRecord" runat="server" CssClass="button" 
								Text="Record checked labs" OnClick="btnRecord_Click" />
							<br />
							<obout:Grid ID="grdSymptomLab" runat="server" OnRebind="grdSymptomLab_Rebind" AutoGenerateColumns="False"
								AllowAddingRecords="False" PageSize="10" FolderStyle="grid_styles/Style_7" AllowFiltering="True" 
								KeepSelectedRecords="False" OnPreRender="Grid_PreRender">
								<Columns>
									<obout:CheckBoxSelectColumn AllowSorting="False" ShowHeaderCheckBox="False"
										Width="75" Index="0" />
									<obout:Column DataField="Assigned" HeaderText="Currently Assigned"
										Width="120" AllowFilter="False" Index="1" />
									<obout:Column HeaderText="Lab - enter text in box for filter" 
										DataField="GroupName" AllowSorting="False" Width="500"
										ReadOnly="True" ShowFilterCriterias="False" FilterTemplateId="NameFilterHist" Index="2">
										<TemplateSettings FilterTemplateId="NameFilterHist" />
										<FilterOptions>
											<obout:FilterOption IsDefault="True" Type="Contains" />
										</FilterOptions>
									</obout:Column>
									<obout:Column DataField="GroupID" Visible="False" HeaderText="GroupID" 
										Index="3" />
								</Columns>
								<FilteringSettings InitialState="Visible" FilterPosition="Top" />
								<Templates>
									<obout:GridTemplate runat="server" ID="NameFilterHist" ControlID="NameHist" ControlPropertyName="">
										<Template>
											<obout:OboutTextBox runat="server" ID="NameHist" Width="150px" autocomplete="off">
								<ClientSideEvents OnKeyUp="applyFilter" />
											</obout:OboutTextBox>
										</Template>
									</obout:GridTemplate>
								</Templates>
							</obout:Grid>
							
							<p>
							</p>
						</p>
					</ContentTemplate>
				</cc1:TabPanel>
				<cc1:TabPanel HeaderText="Diagnosis" runat="server" ID="pnlDiagnosis" BackColor="#EFE1C9">
					<ContentTemplate>
						<p class="regText">
							Symptom
							<asp:DropDownList ID="ddlDiagnosis" runat="server"  DataTextField="Diag_Title"
								DataValueField="Diagnosis_ID" ClientIDMode="Static" OnSelectedIndexChanged="ddlDiagnosis_SelectedIndexChanged"
								AutoPostBack="true" />
							<br />
							<br />
							<asp:Button ID="btnRecordDiag" runat="server" CssClass="button" Text="Record checked labs"
								Visible="true" OnClick="btnRecordDiag_Click" />
							<br />
							<obout:Grid ID="grdDiagnosisLab" runat="server" OnRebind="grdDiagnosisLab_Rebind"
								AutoGenerateColumns="false" AllowAddingRecords="false" AllowPaging="true" PageSize="10"
								FolderStyle="grid_styles/Style_7" AllowSorting="true" AllowFiltering="true" CallbackMode="true"
								KeepSelectedRecords="false"  OnPreRender="Grid_PreRender">
								<Columns>
									<obout:CheckBoxSelectColumn AllowSorting="false" ShowHeaderCheckBox="false" HeaderText=""
										Width="75" />
									<obout:Column AllowSorting="true" DataField="Assigned" HeaderText="Currently Assigned"
										Width="120" AllowFilter="false" />
									<obout:Column HeaderText="Lab - enter text in box for filter" DataField="GroupName" AllowSorting="false" Width="500"
										ReadOnly="true" ShowFilterCriterias="false" FilterTemplateId="NameFilterDiag">
										<TemplateSettings FilterTemplateId="NameFilterDiag" />
										<FilterOptions>
											<obout:FilterOption IsDefault="True" Type="Contains" />
										</FilterOptions>
									</obout:Column>
									<obout:Column DataField="GroupID" Visible="false" />
								</Columns>
								<FilteringSettings InitialState="Visible" FilterPosition="Top" />
								<Templates>
									<obout:GridTemplate runat="server" ID="NameFilterDiag" ControlID="NameDiag" ControlPropertyName="">
										<Template>
											<obout:OboutTextBox runat="server" ID="NameDiag" Width="150px" autocomplete="off">
								<ClientSideEvents OnKeyUp="applyFilterDiag" />
											</obout:OboutTextBox>
										</Template>
									</obout:GridTemplate>
								</Templates>
							</obout:Grid>

						</p>
					</ContentTemplate>
				</cc1:TabPanel>
			</cc1:TabContainer>
			<script type="text/javascript">
				document.bgColor = "#EFE1C9";
			</script>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
