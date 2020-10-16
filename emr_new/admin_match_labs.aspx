<%@ Page Title="Match Labs" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="admin_match_labs.aspx.cs" Inherits="admin_match_labs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<asp:Button ID="btnProcessSelected" runat="server" Text="Process All Checked" CssClass="button"
		OnClick="btnProcessSelected_Click" />
    <br />
     <br /> 
    Select Year: <asp:DropDownList ID="ddAcdYear" runat="server" Height="22px" AutoPostBack="true" OnSelectedIndexChanged="ddAcdYear_SelectedIndexChanged"
                Width="150px" >
            </asp:DropDownList>
	<obout:Grid ID="grdMatchLabs" runat="server" ShowLoadingMessage="true" AllowPaging="true"
		PageSize="50" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="false" CallbackMode="true" ViewStateMode="Inherit"
		AllowColumnReordering="false" AllowPageSizeSelection="true" Serialize="true"
		 FolderStyle="grid_styles/Style_7" AllowFiltering="false"
		ShowFooter="true">
		<Columns>
			<obout:CheckBoxSelectColumn HeaderText="Select All" Width="100" ControlType="Standard" />
			<obout:Column DataField="Patient" HeaderText="Patient" Width="250" />
			<obout:Column DataField="Followup" HeaderText="Lab Request" Wrap="true" Width="300"
				ParseHTML="true" />
			<obout:Column DataField="Range" HeaderText="Range Start" />
			<obout:Column DataField="AppointmentType" HeaderText="Appointment Type" />
			<obout:Column DataField="ApptStart" HeaderText="Appointment Date" />
			<obout:Column DataField="Followup_ID" Visible="false" />
            <obout:Column DataField="AppointmentID" Visible="false" />
		</Columns>
		<LocalizationSettings NoRecordsText="No changes needed." />
	</obout:Grid>
</asp:Content>
