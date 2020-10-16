<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_prospects.aspx.cs" Inherits="CRM_admin_prospects" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<style type="text/css">
		.long-states .ob_iCboICBC li
		{
			float: left;
			width: 105px;
		}
		
		.short-states .ob_iCboICBC li
		{
			float: left;
			width: 35px;
		}
		
		/* For IE6 */
		* HTML .long-states .ob_iCboICBC li
		{
			-width: 95px;
		}
		
		* HTML .short-states .ob_iCboICBC li
		{
			-width: 25px;
		}
		
		* HTML .ob_iCboICBC li i
		{
			-visibility: hidden;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p class="PageTitle">
		Manage Proepects</p>
	<obout:Grid ID="grdProspect" runat="server" AllowAddingRecords="true" AllowColumnReordering="false"
		AllowColumnResizing="true" AllowFiltering="true" AllowPageSizeSelection="true"
		AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" CallbackMode="true"
		FolderStyle="grid_styles/style_1" DataSourceID="sqlProspect" OnInsertCommand="InsertUpdateProspect"
		OnUpdateCommand="InsertUpdateProspect" EnableTypeValidation="false">
		<FilteringSettings InitialState="Hidden" FilterPosition="Top" />
		<Columns>
			<obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server">
			</obout:Column>
			<obout:Column DataField="ProspectID" Visible="false">
				<TemplateSettings RowEditTemplateControlId="lblProspectID" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="FirstName" HeaderText="First Name" Width="100">
				<TemplateSettings RowEditTemplateControlId="txtFirstName" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="LastName" HeaderText="Last Name" Width="100">
				<TemplateSettings RowEditTemplateControlId="txtLastName" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="Address" HeaderText="Address" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtAddress" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="City" HeaderText="City" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtCity" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="State" HeaderText="State" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtState" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="Zip" HeaderText="Zip" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtZip" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="MainPhone" HeaderText="Main Phone" Width="100">
				<TemplateSettings RowEditTemplateControlId="txtMainPhone" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="AltPhone" HeaderText="Alt Phone" Width="100">
				<TemplateSettings RowEditTemplateControlId="txtAltPhone" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="Email" HeaderText="Email" Width="100">
				<TemplateSettings RowEditTemplateControlId="txtEmail" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="ContactMethod" HeaderText="Contact Method" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtLastName" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="StatusID" HeaderText="Status" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="ddlStatus" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:CheckBoxColumn DataField="Flagged" HeaderText="Flagged" Width="100" Visible="false">
				<TemplateSettings RowEditTemplateControlId="cbFlagged" RowEditTemplateControlPropertyName="checked" />
			</obout:CheckBoxColumn>
			<obout:Column DataField="MarketingSources" Visible="false">
				<TemplateSettings RowEditTemplateControlId="cboSources" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
			<obout:Column DataField="Notes" Visible="false">
				<TemplateSettings RowEditTemplateControlId="txtNotes" RowEditTemplateControlPropertyName="value" />
			</obout:Column>
		</Columns>
		<TemplateSettings RowEditTemplateId="tplEditProspect" />
		<Templates>
			<obout:GridTemplate ID="tplEditProspect" runat="server" ControlID="" ControlPropertyName="">
				<Template>
					<asp:Label ID="lblProspectID" runat="server" ClientIDMode="Static" />
					<table class="rowEditTable" cellpadding="6" cellspacing="6">
						<tr>
							<td valign="top" align="right" style="width: 100">
								First Name
							</td>
							<td valign="top">
								<asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" BackColor="LightGray"
									TabIndex="1" CssClass="regText" />
							</td>
							<td valign="top" align="right" style="width: 100">
								Last Name
							</td>
							<td>
								<asp:TextBox ID="txtLastName" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="2" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Address
							</td>
							<td valign="top">
								<asp:TextBox ID="txtAddress" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="3" />
							</td>
							<td valign="top" align="right">
								City
							</td>
							<td valign="top">
								<asp:TextBox ID="txtCity" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="4" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								State
							</td>
							<td valign="top" nowrap="nowrap">
								<asp:TextBox ID="txtState" runat="server" Columns="2" BackColor="LightGray" TabIndex="5" />
							</td>
							<td align="right" valign="top">
								Zip
							</td>
							<td valign="top">
								<asp:TextBox ID="txtZip" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="6" Columns="6" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Main Phone
							</td>
							<td valign="top">
								<asp:TextBox ID="txtMainPhone" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="7" />
							</td>
							<td valign="top" align="right">
								Alternate Phone
							</td>
							<td valign="top">
								<asp:TextBox ID="txtAltPhone" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="8" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Email
							</td>
							<td valign="top">
								<asp:TextBox ID="txtEmail" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="9" />
							</td>
							<td valign="top" align="right">
								Preferres Contact Method
							</td>
							<td valign="top">
								<asp:TextBox ID="txtContactMethod" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TabIndex="10" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Status
							</td>
							<td valign="top">
								<asp:DropDownList ID="ddlStatus" DataSourceID="sqlStatus" runat="server" DataTextField="StatusName"
									DataValueField="StatusID" BackColor="LightGray" TabIndex="11" />
							</td>
							<td valign="top" align="right">
								Flagged
							</td>
							<td valign="top">
								<asp:CheckBox ID="cbFlagged" runat="server" Text="" BackColor="LightGray" TabIndex="12" />
							</td>
						</tr>
						<tr>
							<td valign="top" align="right">
								Marketing Sources
							</td>
							<td valign="top">
								<obout:ComboBox ID="cboSources" runat="server" EmptyText="Select multiple sources ..."
									DataSourceID="sqlSources" DataTextField="MarketingSourceName" DataValueField="MarketingSourceID"
									AllowEdit="false" SelectionMode="Multiple" ClientIDMode="Static" BackColor="LightGray"
									TabIndex="13" />
							</td>
							<td valign="top" align="right">
								Notes
							</td>
							<td valign="top">
								<asp:TextBox ID="txtNotes" runat="server" CssClass="regText" ClientIDMode="Static"
									BackColor="LightGray" TextMode="MultiLine" Rows="4" Columns="20" TabIndex="14" />
							</td>
						</tr>
					</table>
				</Template>
			</obout:GridTemplate>
		</Templates>
	</obout:Grid>
	<asp:SqlDataSource ID="sqlProspect" SelectCommand="select * from CRM_Prospects" runat="server"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
	<asp:SqlDataSource ID="sqlStatus" SelectCommand="select * from CRM_Status where Active_YN=1"
		runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
	<asp:SqlDataSource ID="sqlSources" runat="server" ConnectionString="<%$ ConnectionStrings:db %>"
		SelectCommandType="Text" SelectCommand="select * from CRM_MarketingSources where Active_YN=1" />
</asp:Content>
