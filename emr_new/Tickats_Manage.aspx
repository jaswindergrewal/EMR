<%@ Page Title="Manage Automatic Ticket Processes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Tickats_Manage.aspx.cs" Inherits="ManageTickets" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="Scripts/Scrips.js" type="text/javascript"></script>

    <script type="text/javascript">
        function onDoubleClick(iRecordIndex) {
            grdTicketManage.editRecord(iRecordIndex);
        }


        //function OnUpdateValidation() {
        //    var txtProcessName = document.getElementById('ctl00$MainContent$grdTicketManage$ob_grdTicketManageEditControl1');
        //    if (txtProcessName.value == "") {
        //        alert('You must enter a Process name before updating items.');
        //        return false
        //    }
        //    return true;
        //}


    </script>

    <style type="text/css">
        #grdTicketManage
        {
            width: 98% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table>
        <tr>
            <td class="PageTitle" align="center">Manage Automatic Ticket Processes
            </td>
        </tr>
        <tr>
            <td class="PageTitle" align="center">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divMain" style="background-color: Transparent; padding-left: 2px; padding-right: 2px; width: 100%">
        <obout:Grid ID="grdTicketManage" runat="server" ShowLoadingMessage="false" AllowPaging="true" Width="750px"
            PageSize="10" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="false"
            AllowPageSizeSelection="true" Serialize="true" CellPadding="0" CellSpacing="0"
            CallbackMode="true" AllowGrouping="false" AllowColumnResizing="true" AllowFiltering="true"
            OnUpdateCommand="UpdateRecord" FolderStyle="grid_styles/Style_7" Style="padding-left: 2px; padding-right: 2px;">
            <Columns>
                <obout:Column DataField="ProcessID" Visible="false" />
                <obout:Column DataField="ProcessName" HeaderText="Process" Width="20%">
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                        <obout:FilterOption IsDefault="false" Type="StartsWith" />
                        <obout:FilterOption IsDefault="false" Type="EndsWith" />
                    </FilterOptions>
                </obout:Column>

                <obout:Column DataField="Interval" HeaderText="Interval in seconds"
                    ShowFilterCriterias="false" AllowFilter="false" Width="20%">
                </obout:Column>
                <obout:CheckBoxColumn DataField="Enabled" HeaderText="Enabled"
                    ShowFilterCriterias="false" AllowFilter="false" Width="10%" />

                <obout:Column DataField="Note" HeaderText="Note"
                    ShowFilterCriterias="false" AllowFilter="false" Width="30%" />

                <obout:Column ID="Column1" HeaderText="EDIT" AllowEdit="true" AllowDelete="false"
                    Width="17%" runat="server" />
            </Columns>
            <PagingSettings ShowRecordsCount="false" />
            <%--<ClientSideEvents OnClientDblClick="onDoubleClick" OnBeforeClientUpdate="OnUpdateValidation" />--%>
            <ClientSideEvents OnClientDblClick="onDoubleClick" OnBeforeClientInsert="ValidateTicketProcessing" OnBeforeClientUpdate="ValidateTicketProcessing" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" />

        </obout:Grid>
    </div>

</asp:Content>
