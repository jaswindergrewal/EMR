<%@ Page Title="Remove Quick Books Match " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_QBMatches.aspx.cs" Inherits="admin_QBMatches" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<style type="text/css">
		.tdText
		{
			font: 11px Verdana;
			color: #333333;
		}
		.option2
		{
			font: 11px Verdana;
			color: #0033cc;
			padding-left: 4px;
			padding-right: 4px;
		}
		a
		{
			font: 11px Verdana;
			color: #315686;
			text-decoration: underline;
		}
		
		a:hover
		{
			color: crimson;
		}
	</style>
	<script type="text/javascript">
		function onDoubleClick(iRecordIndex) {
			grdQBMatch.editRecord(iRecordIndex);
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="PageTitle">
			<td>
				Remove Quick Books Match
			</td>
		</tr>
		<tr>
			<td>
				<p class="regText">
					Select a name from the drop down with the name of the patient whose watch you wish
					to break.</p>
			</td>
		</tr>
		<tr>
			<td>
				<asp:DropDownList ID="ddlQBCustomers" runat="server" CssClass="FormField" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Button ID="btnBreakLink" runat="server" Text="Remove match" OnClick="btnBreakLink_CLick"
					CssClass="button" />
				<cc1:ConfirmButtonExtender ID="cnfBreakLink" runat="server" TargetControlID="btnBreakLink"
					ConfirmText="You are about to break a match to QuickBooks for this patient." />
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;
			</td>
		</tr>
		<tr bgcolor="#D6B781" class="PageTitle">
			<td>
				Add Quick Books Match
			</td>
		</tr>
		<tr>
			<td>
                <%--DataSourceID="sqlMatch" --%>
				<obout:Grid ID="grdQBMatch" runat="server" 
							AutoGenerateColumns="false" 
							DataKeyNames="PatientID,FullName,Street Address,City,State,Zip,Home Phone,Inactive,LastContact"							
							AllowPaging="true" 
							PageSize="25" 
							CellPadding="6" CellSpacing="6"
							CallbackMode="true" 
							Serialize="true" 
							AllowRecordSelection="false" 
							AllowGrouping="false"
							AllowColumnResizing="false" 
							AllowAddingRecords="false" 
							AllowFiltering="true"
							OnUpdateCommand="grdQBMatch_UpdateCommand" 
							FolderStyle="grid_styles/style_1">
					<ClientSideEvents OnClientDblClick="onDoubleClick" />
					<Columns>
						<obout:Column DataField="PatientID" Visible="false" />
						<obout:Column DataField="FullName" HeaderText="Full Name" runat="server" ReadOnly="true" />
						<obout:Column ID="QBCustomer" DataField="QBCust" HeaderText="QB Customers" runat="server">
							<TemplateSettings TemplateId="TemplateQbCust" EditTemplateId="TemplateEditQBCust" />
						</obout:Column>
						<obout:Column DataField="ShippingStreet" HeaderText="Street Address" runat="server"
							ReadOnly="true" />
						<obout:Column DataField="ShippingCity" HeaderText="City" runat="server" Width="100px"
							ReadOnly="true" />
						<obout:Column DataField="ShippingState" HeaderText="State" Width="50px" runat="server"
							ReadOnly="true" />
						<obout:Column DataField="ShippingZip" HeaderText="Zip" Width="75px" runat="server"
							ReadOnly="true" />
						<obout:Column DataField="HomePhone" HeaderText="Home Phone" runat="server" ReadOnly="true" />
						<obout:CheckBoxColumn DataField="InActive" HeaderText="InActive" runat="server" ReadOnly="true" />
						<obout:Column DataField="Notes" HeaderText="Notes" runat="server" Width="100px" />
						<obout:Column DataField="LastContact" HeaderText="Last Contact" runat="server" ReadOnly="true" />
						<obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" />
					</Columns>
					<Templates>
						<obout:GridTemplate ID="TemplateQbCust" runat="server">
							<Template>
								<%# Container.DataItem["QBCust"]%>
							</Template>
						</obout:GridTemplate>
						<obout:GridTemplate ID="TemplateEditQBCust" runat="server" ControlID="ddlNoMatch" ControlPropertyName="value">
							<Template>
								<asp:DropDownList ID="ddlNoMatch" runat="server" DataSourceID="sqlNotMatched" DataTextField="FullName"
									DataValueField="ListID" CssClass="FormField" />
								<asp:SqlDataSource ID="sqlNotMatched" runat="server" SelectCommand="select 'No Match' AS FullName, 'None' AS ListID,'1111111' as HiddenName UNION select (FullName + ' '+  Address1 + ' ' + City + ' PH: ' + Phone) AS FullName, ListID,FullName as HiddenName from QB_Customers q left outer join QB_Match m on q.ListID=m.QBid where m.QBid is null order by HiddenName"
									ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
							</Template>
						</obout:GridTemplate>
					</Templates>
				</obout:Grid>
			</td>
		</tr>
	</table>
	<%--<asp:SqlDataSource ID="sqlMatch" runat="server" SelectCommand="select 'Edit to view QB Customers' as QBCust, p.PatientID, (LastName + ', ' + FirstName) AS FullName, ShippingStreet, ShippingCity, ShippingState, ShippingZip,p.HomePhone,InActive, p.Notes,(select top 1 ContactDateEntered from Contact_tbl  where PatientID=p.PatientID order by ContactDateEntered desc) as LastContact from Patients p left outer join QB_Match m on p.PatientID=m.PatientID where QBid is null AND FirstName <> '' AND LastName <> '' AND p.PatientID <> 7447 AND LastName not like '%tester%' AND p.Medical=1 order by LastContact desc"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />--%>
</asp:Content>
