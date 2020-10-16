<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProblemSuppliment.aspx.cs"  EnableEventValidation="false" Inherits="ProblemSuppliment" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
    TagPrefix="NineRays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="~/resources/custom-styles/persist/style.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    
    <cc1:TabContainer ID="Container" runat="server" CssClass="lmc_tab">
        <cc1:TabPanel ID="SkedPanel" runat="server" HeaderText="" CssClass="TabPanel">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="upPpanel1">
                    <ProgressTemplate>
                        <img src="../images/indicator.gif" alt="Loading" />
                        <strong>Please Wait . . .</strong>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <flan:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server"
                    ControlToOverlayID="SkedDiv" CssClass="updateProgress" TargetControlID="UpdateProgress1" />
                <asp:UpdatePanel ID="upPpanel1" runat="server">
                    <ContentTemplate>
                        <div id="Layer16" style="background-image: url(../images/export/beige_back.gif); border: 1px none #000000; width: 1000px;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="MenuBox">
                                <tr>
                                    <td width="100%">
                                        
                                        <asp:Button ID="btnCancelChanges" runat="server" Text="Cancel changes" OnClick="btnCancelChanges_Click"
                                            CssClass="CancelButon" />
                                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save changes" OnClick="btnSaveChanges_Click"
                                            CssClass="SaveButon" />
                                    </td>    
                                </tr>
                            </table>
                        </div>
                        <table  width="100%" cellpadding="2" cellspacing="2" style="background-image: url(../images/export/beige_back.gif);">
                            <tr>
                                <th class="largeheading">Suppliments
                                </th>
                                <th class="largeheading">Problem List<br />
                                    <asp:Label ID="lblNotSaved" runat="server" Text="Changes not saved!" ForeColor="Red"
                                        Visible="false" />
                                </th>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <div class="ProductList">
                                        <NineRays:FlyTreeView ID="SupplimentList" runat="server" Padding="3px" FadeEffect="True" BackColor="White" 
                                            DragDropName="" DragDropMode="Copy" BackgroundImage="~/images/export/beige_back.gif"
                                            ExpandLevel="2" CssClass="regText">
                                            <HoverStyle Font-Underline="True" />
                                        </NineRays:FlyTreeView>
                                    </div>
                                </td>
                                <td valign="top">
                                    <div class="ProductList" id="SkedDiv" runat="server">
                                        <NineRays:FlyTreeView ID="Sked" runat="server" Padding="3px" FadeEffect="True" BackColor="White"
                                            DragDropAcceptNames="Product" PostBackOnDropAccept="true" OnNodeInserted="OnNodeMoved"
                                            ExpandLevel="2" BackgroundImage="~/images/export/beige_back.gif" CssClass="regText">
                                            <HoverStyle Font-Underline="True" />
                                        </NineRays:FlyTreeView>
                                    </div>
                                </td>
                            </tr>
                        </table>

                       


                       

                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        

    </cc1:TabContainer>
    
    
</asp:Content>