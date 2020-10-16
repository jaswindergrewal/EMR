<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
	CodeFile="admin_crm.aspx.cs" Inherits="CRM_admin_crm" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table cellpadding="6" cellspacing="6">
		<tr>
			<td class="PageTitle">
				Status Management
			</td>
			<td class="PageTitle">
				Marketing Source Management
			</td>
		</tr>
		<tr>
			<td valign="top">
				<obout:Grid ID="grdStatus" runat="server" AllowAddingRecords="true" AllowFiltering="false"
					AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
					AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
					OnUpdateCommand="grdStatus_UpdateInsert" OnInsertCommand="grdStatus_UpdateInsert"
					OnRebind="grdStatus_Rebind" EnableTypeValidation="false" DataSourceID="sqlStatus">
					<Columns>
						<obout:Column DataField="StatusID" Visible="false" />
						<obout:Column DataField="StatusName" HeaderText="Status Name" />
						<obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
						<obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" />
					</Columns>
				</obout:Grid>
			</td>
			<td valign="top">
				<obout:Grid ID="grdMSource" runat="server" AllowAddingRecords="true" AllowFiltering="false"
					AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
					AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
					OnUpdateCommand="grdMSource_UpdateInsert" OnInsertCommand="grdMSource_UpdateInsert"
					OnRebind="grdMSource_Rebind" EnableTypeValidation="false" DataSourceID="sqlMarketingSource">
					<Columns>
						<obout:Column DataField="MarketingSourceID" Visible="false" />
						<obout:Column DataField="MarketingSourceName" HeaderText="Marketing Source Name" />
						<obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
						<obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" />
					</Columns>
				</obout:Grid>
			</td>
		</tr>
	</table>
	<asp:SqlDataSource ID="sqlStatus" runat="server" SelectCommand="select * from CRM_Status where Active_YN=1  order by StatusName"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
	<asp:SqlDataSource ID="sqlMarketingSource" runat="server" SelectCommand="select * from CRM_MarketingSources where Active_YN=1  order by MarketingSourceName"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
</asp:Content>
