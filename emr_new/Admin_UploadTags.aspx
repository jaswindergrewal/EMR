<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_UploadTags.aspx.cs" MasterPageFile="~/Site.master" Inherits="Admin_UploadTags" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td class="PageTitle" align="center">Edit Upload Tags List
            </td>
        </tr>
        <tr>
            <td class="PageTitle" align="center">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divMain" style="background-color: Transparent; padding-left: 2px; padding-right: 2px; width: 98%">
                <asp:CheckBox ID="chkDisabled" runat="server" Text="Show only items that are disabled"
            CssClass="regText" OnCheckedChanged="chkDisabled_CheckedChanged" AutoPostBack="true" />
        
        <obout:Grid ID="grdtaglist" runat="server" AllowAddingRecords="true" AllowFiltering="false" AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
         AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5" OnUpdateCommand="grdtaglist_UpdateInsert" OnInsertCommand="grdtaglist_UpdateInsert"
         OnRebind="grdtaglist_Rebind" OnDeleteCommand="grdtaglist_Delete">
           
            <Columns>
                <obout:Column DataField="Id" Visible="false" />
                <obout:Column DataField="Name"  HeaderText="Name" ItemStyle-Wrap="true" Width="50%" Wrap="true">
                  
                </obout:Column>
                 <obout:Column DataField="Disabled"  HeaderText="Disabled" >
                  
                </obout:Column>
               
                <obout:Column ID="Column1" HeaderText="EDIT" AllowEdit="true" AllowDelete="true"
                    runat="server" />
            </Columns>

           
            <PagingSettings ShowRecordsCount="false" />

            <ClientSideEvents OnBeforeClientInsert="ValidateTags" OnBeforeClientUpdate="ValidateTags" OnClientUpdate="MessageAfterUpdateRecords" OnBeforeClientDelete="ConfirmDelete" />

        </obout:Grid>

   </div>
</asp:Content>
