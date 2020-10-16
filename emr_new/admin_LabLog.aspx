<%@ Page Title="View Lab Log" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_LabLog.aspx.cs" Inherits="admin_LabLog" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="PageTitle">
			<td>
				View Lab Log
			</td>
		</tr>
		<tr>
			<td>
				<obout:Grid ID="grdLabLog" runat="server" AutoGenerateColumns="false" 
					 AllowPaging="true" PageSize="25" CellPadding="6" CellSpacing="6"
					CallbackMode="true" Serialize="true" AllowRecordSelection="false" AllowGrouping="false"
					AllowColumnResizing="false" AllowAddingRecords="false" AllowFiltering="true" Width="100%"
					FolderStyle="grid_styles/Style_7">
					<Columns>
						<obout:Column DataField="PatientName" HeaderText="Patient" runat="server" width="25%" ReadOnly="true" />
						<obout:Column DataField="Birthday" HeaderText="Birthday" runat="server" width="25%" ReadOnly="true" />
						<obout:Column DataField="LabName" HeaderText="Lab" runat="server" width="25%" />
						<obout:Column DataField="DateImported" HeaderText="Date Imported" runat="server" width="25%"
							ReadOnly="true" />
					</Columns>
					</obout:Grid>
			</td>
		</tr>
	</table>
</asp:Content>
