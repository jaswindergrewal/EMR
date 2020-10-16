<%@ Page Title="Manage External Labs and Panels" Language="C#" MasterPageFile="sub.master" AutoEventWireup="true" CodeFile="Panels.aspx.cs" Inherits="External_Panels" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/Scrips.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <obout:Grid ID="grdPanel" runat="server" CallbackMode="true" Serialize="true" AllowDataAccessOnServer="true"
        AllowAddingRecords="true" AllowColumnResizing="true" AutoGenerateColumns="false"
        EnableTypeValidation="false" FolderStyle="../grid_styles/Style_7"
        OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord">
        <Columns>
            <obout:Column DataField="ExternalPanelsID" Visible="false" />
            <obout:Column DataField="PanelName" HeaderText="PanelName" Width="500px" />
            <obout:Column ID="Column1" DataField="" HeaderText="" Width="125" AllowEdit="true" AllowDelete="true" runat="server" />
        </Columns>
        <ClientSideEvents OnBeforeClientInsert="ValidatePanels" OnBeforeClientUpdate="ValidatePanels" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" OnBeforeClientDelete="ConfirmDelete" />
    </obout:Grid>
</asp:Content>

