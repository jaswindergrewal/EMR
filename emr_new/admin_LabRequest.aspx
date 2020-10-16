<%@ Page Title="Lab Request Panels and Tests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="admin_LabRequest.aspx.cs" Inherits="admin_LabRequest" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="Scripts/Scrips.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table cellpadding="6" cellspacing="6">
        <tr>
            <td class="PageTitle">Panel Management
            </td>
            <td class="PageTitle">Test Management
            </td>
        </tr>
        <tr>
            <td valign="top">
                <obout:Grid ID="grdPanel" runat="server" AllowAddingRecords="true" AllowFiltering="false" EnableRecordHover="true"
                    AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="UpdateInsert" OnInsertCommand="UpdateInsert"
                    OnRebind="grdPanel_Rebind" EnableTypeValidation="false" OnDeleteCommand="DeletePanelMgmt">
                    <Columns>
                        <obout:Column DataField="LabRequest_PanelID" Visible="false" ReadOnly="true" />
                        <obout:Column DataField="PanelName" HeaderText="Panel Name" />
                        <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                        <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateLabRequestPanels" OnBeforeClientUpdate="ValidateLabRequestPanels" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
            <td valign="top">
                <obout:Grid ID="grdTest" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="UpdateInsert" OnInsertCommand="UpdateInsert"
                    OnRebind="grdTest_Rebind" EnableTypeValidation="false" OnDeleteCommand="DeleteTestMgmt">
                    <Columns>
                        <obout:Column DataField="LabRequest_TestID" Visible="false" ReadOnly="true" HeaderText="ID" />
                        <obout:Column DataField="TestName" HeaderText="Test Name" />
                        <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                        <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateLabRequestTest" OnBeforeClientUpdate="ValidateLabRequestTest" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
        </tr>
    </table>

</asp:Content>

