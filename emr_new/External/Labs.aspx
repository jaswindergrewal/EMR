<%@ Page Title="" Language="C#" MasterPageFile="sub.master" AutoEventWireup="true" CodeFile="Labs.aspx.cs" Inherits="External_Labs" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        function ConfirmDelete() {
            var message = confirm('Are you sure you want to delete this item?');
            if (message != 0) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <span class="tdText">Panel:</span><br />
    <obout:ComboBox runat="server" ID="cboPanels" Width="250" Height="200"
        Mode="ComboBox" EmptyText="Select a panel ..."
        DataTextField="PanelName" DataValueField="ExternalPanelsID" />
    <asp:Button ID="btnGoPanel" runat="server" Text="Go" OnClick="btnGoClick" class="button" />
    <%--<asp:SqlDataSource ID="sds1" runat="server" SelectCommand="select * from ExternalPanels order by PanelName"></asp:SqlDataSource>--%>
    <p>&nbsp;</p>
    <span class="tdText">Currently Assigned Tests:</span><br />
    <br />
    <obout:Grid ID="grdAssigned" runat="server" CallbackMode="true" Serialize="true"
        FolderStyle="../grid_styles/Style_7" AutoGenerateColumns="false" AllowAddingRecords="false"
        AllowRecordSelection="true" AllowFiltering="false" AllowGrouping="false" EnableRecordHover="false" OnDeleteCommand="DeleteRecord">
        <Columns>
            <obout:Column ID="Column1" DataField="" HeaderText="" Width="125" AllowEdit="false" AllowDelete="true" runat="server" />
            <obout:Column ID="Column2" DataField="ExternaLabName" HeaderText="Lab Name" Width="1000" runat="server" />
            <obout:Column ID="Column3" DataField="ExternalLabListID" Visible="false" runat="server" />
        </Columns>
        <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
    </obout:Grid>
    <p>&nbsp;</p>
    <span class="tdText">Available Tests:
        <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssignClick" class="button" /></span><br />
    <br />
    <obout:Grid ID="grdAvailable" runat="server" CallbackMode="true" Serialize="true"
        FolderStyle="../grid_styles/Style_7" AutoGenerateColumns="false" AllowAddingRecords="false"
        AllowRecordSelection="true" AllowFiltering="true" AllowGrouping="false" EnableRecordHover="false" PageSize="50">
        <Columns>
            <obout:CheckBoxSelectColumn ShowHeaderCheckBox="false" ControlType="Standard" />
            <obout:Column ID="Column5" DataField="ExternaLabName" HeaderText="Lab Name" Width="1000" runat="server" />
            <obout:Column ID="Column6" DataField="ExternalLabListID" Visible="false" runat="server" />
        </Columns>
    </obout:Grid>
</asp:Content>

