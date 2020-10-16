<%@ Page Title="Affiliate Events and Status" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_reseller_data.aspx.cs" Inherits="admin_reseller_data" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="Scripts/Scrips.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table cellpadding="6" cellspacing="6">
        <tr>
            <td class="PageTitle">Status Management
            </td>
            <td class="PageTitle">Event Management
            </td>
            <td class="PageTitle">Marketing Source
            </td>
        </tr>
        <tr>
            <td valign="top">
                <obout:Grid ID="grdStatus" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="grdStatus_UpdateInsert" OnInsertCommand="grdStatus_UpdateInsert" OnDeleteCommand="DeleteStatusManagement"
                    OnRebind="grdStatus_Rebind" EnableTypeValidation="false">
                    <Columns>
                        <obout:Column DataField="StatusID" Visible="false" />
                        <obout:Column DataField="Status" HeaderText="Status Name" />
                        <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                        <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateResellerStatus" OnBeforeClientUpdate="ValidateResellerStatus" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />

                </obout:Grid>
            </td>
            <td valign="top">

                <obout:Grid ID="grdEvent" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="grdEvent_UpdateInsert" OnInsertCommand="grdEvent_UpdateInsert" OnDeleteCommand="DeleteEventManagement"
                    OnRebind="grdEvent_Rebind" EnableTypeValidation="false">
                    <Columns>
                        <obout:Column DataField="EventID" Visible="false" />
                        <obout:Column DataField="EventName" HeaderText="Event Name" />
                        <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                        <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateEventManagement" OnBeforeClientUpdate="ValidateEventManagement" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
            <td valign="top">

                <obout:Grid ID="grdSource" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="grdSource_UpdateInsert" OnInsertCommand="grdSource_UpdateInsert" OnDeleteCommand="DeleteMarketingSourceManagement"
                    OnRebind="grdSource_Rebind" EnableTypeValidation="false">
                    <Columns>
                        <obout:Column DataField="ResellerMarketingSourceID" Visible="false" />
                        <obout:Column DataField="SourceName" HeaderText="Marketing Source Name" />
                        <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                        <obout:Column ID="Column3" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateMarketingSource" OnBeforeClientUpdate="ValidateMarketingSource" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
        </tr>
    </table>

</asp:Content>
