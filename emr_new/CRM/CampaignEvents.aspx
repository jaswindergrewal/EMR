<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/Site.master" AutoEventWireup="true"
	CodeFile="CampaignEvents.aspx.cs" Inherits="CRM_CampaignEvents" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<script type="text/javascript">
		function ValidateCampaign(record) {

		}

		function ValidateEvent(record) {
			var txtEventName = document.getElementById('txtEventName');
			var txtEventDate = document.getElementById('txtEventName');
			var txtVenue = document.getElementById('txtEventName');
			var errString = "";
			if (txtEventName.value == "") {
				errString += "You must enter an Event Name.";
			}
			if (txtEventDate.value == "") {
				errString += "\r\nYou must enter an Event Date and Time.";
			}
			if (txtVenue.value == "") {
				errString += "\r\nYou must enter a venue.";
			}
			if (errString == "")
				return true;
			else {
				alert(errString);
				return false;
			}
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p class="PageTitle">
		Manage Campaigns and Events</p>
	<obout:Grid runat="server" ID="grdCampaign" AutoGenerateColumns="false" PageSize="5"
		DataSourceID="sqlCampaign" FolderStyle="grid_styles/style_1" AllowAddingRecords="true"
		OnInsertCommand="InsertUpdateCampaign" OnUpdateCommand="InsertUpdateCampaign"
		AllowMultiRecordSelection="false">
		<Columns>
			<obout:Column DataField="CampaignID" Visible="false" HeaderText="Campaign ID" ReadOnly="true">
				<TemplateSettings RowEditTemplateControlId="txtCampaignName" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="CampaignName" HeaderText="Campaign Name">
				<TemplateSettings RowEditTemplateControlId="txtCampaignName" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="MarketingBudget" HeaderText="Marketing Budget" DataFormatString="{0:C2}"
				ApplyFormatInEditMode="true">
				<TemplateSettings RowEditTemplateControlId="txtMarketingBudget" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="CampaignType" HeaderText="Campaign Type">
				<TemplateSettings RowEditTemplateControlId="txtCampaignType" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}"
				Width="100" ApplyFormatInEditMode="true">
				<TemplateSettings RowEditTemplateControlId="txtStartDate" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}"
				Width="100" ApplyFormatInEditMode="true">
				<TemplateSettings RowEditTemplateControlId="txtEndDate" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="MarketingSources" Visible="false">
				<TemplateSettings RowEditTemplateControlId="cboSources" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="" HeaderText="" AllowEdit="true" AllowDelete="false">
			</obout:Column>
		</Columns>
		<ClientSideEvents OnBeforeClientInsert="ValidateCampaign" OnBeforeClientUpdate="ValidateCampaign" />
		<TemplateSettings RowEditTemplateId="tplEditCampaign" />
		<Templates>
			<obout:GridTemplate ID="tplEditCampaign" runat="server" ControlID="" ControlPropertyName="">
				<Template>
					<asp:Label ID="lblCampaignID" runat="server" ClientIDMode="Static" Style="display: none" />
					<table class="rowEditTable" cellpadding="6" cellspacing="6">
						<tr>
							<td valign="top" align="right" style="width: 100">
								Campaign Name
							</td>
							<td valign="top">
								<asp:TextBox ID="txtCampaignName" runat="server" ClientIDMode="Static" BackColor="LightGray"
									TabIndex="1" CssClass="regText" />
							</td>
							<td valign="top" align="right" style="width: 100">
								Marketing Budget
							</td>
							<td>
								<asp:TextBox ID="txtMarketingBudget" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="2" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Campaign Type
							</td>
							<td valign="top">
								<asp:TextBox ID="txtCampaignType" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="4" />
							</td>
							<td valign="top" align="right">
								Start Date
							</td>
							<td valign="top">
								<asp:TextBox ID="txtStartDate" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="6" />
								<obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="txtStartDate"
									DatePickerImagePath="images/date_picker1.gif">
								</obout:Calendar>
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								End Date
							</td>
							<td valign="top" nowrap="nowrap">
								<asp:TextBox ID="txtEndDate" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="10" />
								<obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtEndDate"
									DatePickerImagePath="images/date_picker1.gif"/>
							</td>
							<td align="right" valign="top">
								Marketing Sources
							</td>
							<td valign="top">
								<obout:ComboBox ID="cboSources" runat="server" EmptyText="Select multiple sources ..."
									DataSourceID="sqlSources" DataTextField="MarketingSourceName" DataValueField="MarketingSourceID"
									AllowEdit="false" SelectionMode="Multiple" ClientIDMode="Static" BackColor="LightGray" />
							</td>
						</tr>
					</table>
				</Template>
			</obout:GridTemplate>
		</Templates>
		<MasterDetailSettings LoadingMode="OnCallback" />
		<DetailGrids>
			<obout:DetailGrid runat="server" ID="grdEvent" AutoGenerateColumns="false" AllowAddingRecords="true"
				ShowFooter="true" PageSize="5" FolderStyle="grid_styles/style_10" ForeignKeys="CampaignID"
				OnInsertCommand="InsertUpdateEvent" OnUpdateCommand="InsertUpdateEvent" DataSourceID="sqlEvent"
				CSSFolderImages="resources/custom-styles/new" ClientIDMode="Static" AllowMultiRecordSelection="false"
				EnableTypeValidation="false">
				<Columns>
					<obout:Column DataField="CampaignID" ReadOnly="false" Visible="false" HeaderText="Campaign ID">
						<TemplateSettings RowEditTemplateControlId="lblCampaignID" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="EventID" Visible="false" HeaderText="Event ID" ReadOnly="true">
						<TemplateSettings RowEditTemplateControlId="lblEventID" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="EventName" HeaderText="Event Name" Width="300">
						<TemplateSettings RowEditTemplateControlId="txtEventName" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="EventDate" HeaderText="Event Date" DataFormatString="{0:MM/dd/yyyy}"
						Width="100" ApplyFormatInEditMode="true">
						<TemplateSettings RowEditTemplateControlId="txtEventDate" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Venue" HeaderText="Venue" Width="150">
						<TemplateSettings RowEditTemplateControlId="txtVenue" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Notes" Visible="false" HeaderText="Notes" Width="300">
						<TemplateSettings RowEditTemplateControlId="txtNotes" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Location" Visible="true" Width="100">
						<TemplateSettings RowEditTemplateControlId="txtLocation" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="EventLength" Visible="false">
						<TemplateSettings RowEditTemplateControlId="txtEventLength" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Walkins" Visible="false">
						<TemplateSettings RowEditTemplateControlId="txtWalkins" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Appointments" Visible="false">
						<TemplateSettings RowEditTemplateControlId="txtAppointments" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Callbacks" Visible="false">
						<TemplateSettings RowEditTemplateControlId="txtCallbacks" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="OverallPerformance" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlOverallPerformance" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="FacilityInteriorExterior" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlFacilityInteriorExterior" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="VenueQuality" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlVenueQuality" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="Parking" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlParking" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="AudienceReaction" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlAudienceReaction" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="AudienceQuality" Visible="false">
						<TemplateSettings RowEditTemplateControlId="ddlAudienceQuality" RowEditTemplateControlPropertyName="value" />
					</obout:Column>
					<obout:Column DataField="" HeaderText="" AllowEdit="true" AllowDelete="false" Align="center"
						Width="125">
					</obout:Column>
				</Columns>
				<ClientSideEvents OnBeforeClientInsert="ValidateEvent" OnBeforeClientUpdate="ValidateEvent" />
				<TemplateSettings RowEditTemplateId="tplEdit" />
				<Templates>
					<obout:GridTemplate ID="tplEdit" runat="server" ControlID="" ControlPropertyName="">
						<Template>
							<asp:Label ID="lblCampaignID" runat="server" ClientIDMode="Static" Style="display: none" />
							<asp:Label ID="lblEventID" runat="server" ClientIDMode="Static" Style="display: none" />
							<table class="rowEditTable" cellpadding="6" cellspacing="6">
								<tr>
									<td valign="top" align="right" style="width: 100">
										Event Name
									</td>
									<td valign="top">
										<asp:TextBox ID="txtEventName" runat="server" ClientIDMode="Static" BackColor="LightGray"
											TabIndex="1" CssClass="regText" /><font color="red">*</font>
									</td>
									<td valign="top" align="right" style="width: 100">
										Event Date and Time<br />
										(mm/dd/yy hh:mm AM or PM)
									</td>
									<td>
										<asp:TextBox ID="txtEventDate" runat="server" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="2" /><font color="red">*</font>
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Venue
									</td>
									<td valign="top">
										<asp:TextBox ID="txtVenue" runat="server" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="4" /><font color="red">*</font>
									</td>
									<td valign="top" align="right">
										Location (city)
									</td>
									<td valign="top">
										<asp:TextBox ID="txtLocation" runat="server" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="6" /><br />
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Walkins
									</td>
									<td valign="top">
										<asp:TextBox ID="txtWalkins" runat="server" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" Columns="5" TabIndex="10" />
									</td>
									<td align="right" valign="top">
										Appointments
									</td>
									<td valign="top">
										<asp:TextBox ID="txtAppointments" runat="server" Columns="5" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="12" />
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Call Backs
									</td>
									<td valign="top">
										<asp:TextBox ID="txtCallbacks" runat="server" Columns="5" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="12" />
									</td>
									<td align="right" valign="top">
										Overall performance
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlOverallPerformance" runat="server" ClientIDMode="Static"
											BackColor="LightGray" TabIndex="16">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Quality of Facility Interior/Exterior
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlFacilityInteriorExterior" runat="server" ClientIDMode="Static"
											TabIndex="16" BackColor="LightGray">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
									<td align="right" valign="top">
										Venue Location Quality
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlVenueQuality" runat="server" ClientIDMode="Static" TabIndex="16"
											BackColor="LightGray">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Parking at Venue
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlParking" runat="server" ClientIDMode="Static" TabIndex="16"
											BackColor="LightGray">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
									<td align="right" valign="top">
										Audience Reaction
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlAudienceReaction" runat="server" ClientIDMode="Static" TabIndex="16"
											BackColor="LightGray">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
								</tr>
								<tr>
									<td valign="top" align="right">
										Overall Audience Quality
									</td>
									<td valign="top">
										<asp:DropDownList ID="ddlAudienceQuality" runat="server" ClientIDMode="Static" TabIndex="16"
											BackColor="LightGray">
											<asp:ListItem Text="Poor" Value="1" />
											<asp:ListItem Text="Fair" Value="2" />
											<asp:ListItem Text="Excellent" Value="3" />
										</asp:DropDownList>
									</td>
									<td align="right" valign="top">
										Event Length
									</td>
									<td valign="top">
										<asp:TextBox ID="txtEventLength" runat="server" CssClass="regText" ClientIDMode="Static"
											BackColor="LightGray" Columns="5" TabIndex="10" />
									</td>
								</tr>
								<tr>
									<td colspan="4" align="center">
										Notes<br />
										<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="80"
											BackColor="LightGray" />
									</td>
								</tr>
							</table>
						</Template>
					</obout:GridTemplate>
				</Templates>
			</obout:DetailGrid>
		</DetailGrids>
	</obout:Grid>
	<asp:SqlDataSource ID="sqlCampaign" runat="server" SelectCommand="select * from CRM_Campaigns order by CampaignName"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
	<asp:SqlDataSource runat="server" ID="sqlEvent" SelectCommand="SELECT * FROM CRM_Events where CampaignID=@CampaignID"
		ConnectionString="<%$ ConnectionStrings:db %>">
		<SelectParameters>
			<asp:Parameter Name="CampaignID" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="sqlSources" runat="server" ConnectionString="<%$ ConnectionStrings:db %>"
		SelectCommandType="Text" SelectCommand="select * from CRM_MarketingSources where Active_YN=1" />
</asp:Content>
