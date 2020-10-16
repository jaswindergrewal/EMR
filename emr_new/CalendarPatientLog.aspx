<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="CalendarPatientLog.aspx.cs" Inherits="CalendarPatientLog" %>


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
<script runat="server">

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
</script>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>


    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
                    <td >Status:</td>
                    <td><asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="btnAddStatus" Text="Add" runat="server" OnClick="btnAddStatus_Click"/></td>
                </tr>
            </table>
            <table cellpadding="6" cellspacing="6">
                <tr>
                    <td class="PageTitle">Status Management
                    </td>
                   
                </tr>
                <tr>
                    <td valign="top">
                        <obout:Grid ID="grdStatus" runat="server" AllowAddingRecords="false" AllowFiltering="false"
                            AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                            AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5" OnRebind="grdStatus_Rebind"
                             EnableTypeValidation="false" OnUpdateCommand="grdStatus_UpdateCommand" OnDeleteCommand="grdStatus_DeleteCommand" >
                            <Columns>

                                <obout:Column DataField="StatusId" Visible="false" />
                                <obout:Column DataField="StatusLogId" Visible="false" />
                                <obout:Column DataField="StatusName" HeaderText="Status Name" ReadOnly="true"></obout:Column>
                                 

                                <obout:CheckBoxColumn DataField="IsTicketSet" HeaderText="SET"  />
                                <%--<obout:Column DataField="TicketText" HeaderText="Ticket Name"></obout:Column> --%>
                                <obout:Column DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="true"></obout:Column>
                                <obout:Column DataField="StatusDate" HeaderText="Date" ReadOnly="true"></obout:Column>
                                <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="false" />
                                 <obout:Column ID="Column1" HeaderText="Remove" AllowEdit="false" Width="125" runat="server" AllowDelete="true" />
                            </Columns>


                           <%-- <ClientSideEvents OnBeforeClientUpdate="ValiDateStatus" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientInsert="ValiDateStatus"  />--%>
                        </obout:Grid>
                    </td>
                   
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

