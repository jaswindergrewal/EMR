<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="patient_aestheticNotes_all.aspx.cs" Inherits="patient_aestheticNotes_all" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
    <div width="98%" runat="server" id="divTitleText"></div>
    <obout:Grid ID="grdAestheticNotes" runat="server" ShowLoadingMessage="False" AutoGenerateColumns="False"
        AllowAddingRecords="False" FolderStyle="grid_styles/Style_7" AllowFiltering="True"
        AllowRecordSelection="False" AllowColumnResizing="False" AllowPageSizeSelection="true" Width="98%"
        >
        <Columns>
            <obout:Column ID="Date" HeaderText="Date" DataField="ContactDateEntered" Width="100" DataFormatString="{0:MM/dd/yyyy}" DataFormatString_GroupHeader="{0:MM/dd/yyyy}" Wrap="True" Index="0">
               
            </obout:Column>
            <obout:Column ID="EnteredBy" HeaderText="Entered By" DataField="EnteredBy" Width="125" Wrap="True"
                Index="1" AllowFilter="true" />
            <obout:Column ID="MessageBody" HeaderText="Notes" DataField="MessageBody" 
                Index="2" AllowFilter="true" />
           
        </Columns>
        <FilteringSettings InitialState="Visible" FilterPosition="Top" />
        <PagingSettings ShowRecordsCount="False" />
        
    </obout:Grid>

</asp:Content>
