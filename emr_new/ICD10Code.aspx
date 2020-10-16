<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ICD10Code.aspx.cs" Inherits="ICD10Code" %>

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
                    <td class="PageTitle">ICD 10 Codes
                    </td>

                </tr>
                <tr>
                    <td valign="top">
                        <obout:Grid ID="grdIcdCodes" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                            AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                            AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5"
                            OnUpdateCommand="grdIcdCodes_InsertCommand" OnInsertCommand="grdIcdCodes_InsertCommand"
                            OnRebind="grdIcdCodes_Rebind" EnableTypeValidation="false">
                            <Columns>

                                <obout:Column DataField="Id" Visible="false" />
                                <obout:Column DataField="ICD10Code" HeaderText="ICD Codes">
                                   
                                </obout:Column>
                                <obout:Column DataField="Description" HeaderText="Description">
                                    
                                </obout:Column>

                                <obout:Column DataField="Gender" HeaderText="Gender">
                                    <TemplateSettings EditTemplateId="tplEditGender" />
                                </obout:Column>
                                <obout:CheckBoxColumn DataField="IsActive" HeaderText="Active" />
                                <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="false" />

                            </Columns>
                            <Templates>
                                <obout:GridTemplate runat="server" ID="tplEditGender"
                                    ControlID="ddlGender" ControlPropertyName="value">
                                    <Template>
                                        <asp:DropDownList runat="server" ID="ddlGender" AppendDataBoundItems="true" Width="55px" CssClass="FormField">
                                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                        </asp:DropDownList>
                                    </Template>
                                </obout:GridTemplate>

                            </Templates>

                            <%-- <ClientSideEvents OnBeforeClientUpdate="ValiDateStatus" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientInsert="ValiDateStatus"  />--%>
                        </obout:Grid>
                    </td>

                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




