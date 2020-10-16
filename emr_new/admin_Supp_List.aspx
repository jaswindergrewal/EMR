<%@ Page Title="Edit Supplement List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_Supp_List.aspx.cs" Inherits="admin_Supp_List" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td class="PageTitle" align="center">Edit Supplement List
            </td>
        </tr>
        <tr>
            <td class="PageTitle" align="center">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <div class="persist" style="background-color: Transparent; padding-left: 0px;">
        <obout:Grid ID="Grid1" runat="server" ShowLoadingMessage="false" AllowPaging="true" EnableRecordHover="true"
            PageSize="25" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="false"
            AllowPageSizeSelection="true" Serialize="true" CellPadding="0" CellSpacing="0" OnDeleteCommand="DeleteSupplimentList"
            CallbackMode="true" AllowGrouping="false" AllowColumnResizing="true" AllowFiltering="true"
            OnUpdateCommand="UpdateRecord" FolderStyle="grid_styles/Style_7" Width="85%">
            <Columns>
                <obout:Column DataField="ProductID" Visible="false" />
                <obout:Column DataField="ProductName" HeaderText="Suppliment Name" Width="50%">
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                        <obout:FilterOption IsDefault="false" Type="StartsWith" />
                        <obout:FilterOption IsDefault="false" Type="EndsWith" />
                    </FilterOptions>
                </obout:Column>
                <obout:CheckBoxColumn DataField="Viewable" HeaderText="Viewable"
                    ShowFilterCriterias="false" AllowFilter="false" Width="10%" />
                <obout:CheckBoxColumn DataField="Reviewed" HeaderText="Reviewed" ShowFilterCriterias="false"
                    AllowFilter="false" runat="server" Width="10%">
                </obout:CheckBoxColumn>
                <obout:Column ID="Column1" HeaderText="EDIT" AllowEdit="true" AllowDelete="true"
                    Width="10%" runat="server" />
            </Columns>
            <PagingSettings ShowRecordsCount="false" />
            <ClientSideEvents OnBeforeClientInsert="ValidateSupplement" OnBeforeClientUpdate="ValidateSupplement" OnClientUpdate="MessageAfterUpdateRecords" OnBeforeClientDelete="ConfirmDelete" />

        </obout:Grid>
    </div>

</asp:Content>
