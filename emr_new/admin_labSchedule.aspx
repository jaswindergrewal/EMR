<%@ Page Title="Recent Draw Groups and Tests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_labSchedule.aspx.cs" Inherits="admin_labSchedule" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="Scripts/Scrips.js" type="text/javascript"></script>
    <script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.filter_input.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table cellpadding="6" cellspacing="6">
        <tr>
            <td class="PageTitle">Group Management
            </td>
            <td class="PageTitle">Test Management
            </td>
        </tr>
        <tr>
            <td valign="top">

                <obout:Grid ID="grdGroup" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="true" PageSize="20" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="grdGroup_UpdateInsert" OnInsertCommand="grdGroup_UpdateInsert"
                    OnRebind="grdGroup_Rebind" EnableTypeValidation="false" OnDeleteCommand="DeleteRecord">
                    <Columns>
                        <obout:Column DataField="GroupID" HeaderText="ID" Width="50" ReadOnly="true" />
                        <obout:Column DataField="GroupName" HeaderText="Group Name" />
                        <obout:Column DataField="Male" HeaderText="Male Days" Width="80" />
                        <obout:Column DataField="Female" HeaderText="Female Days" Width="100" />
                        <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" AllowDelete="true" Width="125" runat="server" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateGroupForLabSchedule" OnBeforeClientUpdate="ValidateGroupForLabSchedule" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
            <td valign="top">

                <obout:Grid ID="grdTest" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                    AllowPageSizeSelection="false" AllowPaging="true" PageSize="20" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
                    OnUpdateCommand="grdTest_UpdateInsert" OnInsertCommand="grdTest_UpdateInsert" OnDeleteCommand="DeleteLabTest"
                    OnRebind="grdTest_Rebind" EnableTypeValidation="false">
                    <Columns>
                        <obout:Column DataField="TestID" Visible="false" />
                        <obout:Column DataField="GroupID" HeaderText="Assigned Group ID" />
                        <obout:Column DataField="TestName" HeaderText="Exact Lab Test Name" Width="300" />
                        <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="ValidateTestForLabSchedule" OnBeforeClientUpdate="ValidateTestForLabSchedule" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
                </obout:Grid>
            </td>
        </tr>
    </table>
</asp:Content>
