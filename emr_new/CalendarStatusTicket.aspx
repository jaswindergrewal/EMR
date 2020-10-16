<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CalendarStatusTicket.aspx.cs" Inherits="CalendarStatusTicket" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>


    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    

    <input type="hidden" id="StaffID" runat="server" clientidmode="Static" />
    <div class="CenterPB">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd">
            <ProgressTemplate>
                <div id="loading-div-background">
                    <div id="loading-div" class="ui-corner-all">
                        <img src="images/indicator.gif" alt="Loading.." />
                        <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
                    </div>
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>

            <table cellpadding="6" cellspacing="6">
                <tr>
                    <td class="PageTitle">Status Management
                    </td>
                   
                </tr>
                <tr>
                    <td valign="top">
                        <obout:Grid ID="grdStatus" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                            AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                            AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5"
                            OnUpdateCommand="grdStatus_UpdateInsert" OnInsertCommand="grdStatus_UpdateInsert"
                            OnRebind="grdStatus_Rebind" EnableTypeValidation="false" >
                            <Columns>

                                <obout:Column DataField="StatusId" Visible="false" />
                                <obout:Column DataField="StatusName" HeaderText="Status Name">
                                    <TemplateSettings RowEditTemplateControlId="StatusName" />
                                </obout:Column>
                                <obout:Column DataField="CalledDays" HeaderText="Called Days">
                                    <TemplateSettings RowEditTemplateControlId="CalledDays"  />
                                </obout:Column>
                                <obout:CheckBoxColumn DataField="Ticket" HeaderText="Ticket" />
                                <obout:Column DataField="TicketText" HeaderText="Ticket Name">
                                    <TemplateSettings RowEditTemplateControlId="TicketText" />
                                </obout:Column>
                                 <obout:CheckBoxColumn DataField="Active" HeaderText="Active" />
                                <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="false" />

                            </Columns>


                           <%-- <ClientSideEvents OnBeforeClientUpdate="ValiDateStatus" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientInsert="ValiDateStatus"  />--%>
                        </obout:Grid>
                    </td>
                   
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



