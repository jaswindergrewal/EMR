<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdminEmailTemplate.aspx.cs" Inherits="AdminEmailTemplate" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
           <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td  colspan="2">
                    <p><b>Email Template</b></p>
                </td>
                
            </tr>
            <tr>
                <td>Type: <asp:DropDownList id="ddlAppointmentType" AutoPostBack="true" OnSelectedIndexChanged="ddlAppointmentType_SelectedIndexChanged" runat="server" ClientIDMode="Static"></asp:DropDownList></td>
                <td><asp:Button ID="btnSumit" runat="server" Text="Submit" CssClass="button" OnClick="btnSumit_Click"/></td>
            </tr>
            <tr>
                <td colspan="2">Choose the common URL that need to set in a form</td>
                
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnUserName" runat="server" Text="UserName" OnClick="btnUserName_Click" CssClass="button" />&nbsp;<asp:Button ID="btnURL" runat="server" Text="URL" CssClass="button" OnClick="btnURL_Click"/></td>
                
            </tr>
            

        </table>

    <table width="600" height="40%" border="0" cellpadding="0" cellspacing="0" class="border">

            <tr valign="top" align="left">
                <td valign="top">
                    <table width="100%" border="0">
                        <tr valign="top">
                            <td width="100%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr valign="top">
                                        <td bgcolor="#FFFFFF">
                                            <obout:Editor ID="edContent" runat="server" Height="300px" Width="600px">
                                                <TopToolbar Appearance="Custom">
                                                    <AddButtons>
                                                        <obout:Bold />
                                                        <obout:Italic />
                                                        <obout:Underline />
                                                        <obout:StrikeThrough />
                                                        <obout:HorizontalSeparator />
                                                        <obout:FontName />
                                                        <obout:FontSize />
                                                        <obout:VerticalSeparator />
                                                        <obout:Undo />
                                                        <obout:Redo />
                                                        <obout:HorizontalSeparator />
                                                        <obout:PasteWord />
                                                        <obout:HorizontalSeparator />
                                                        <obout:JustifyLeft />
                                                        <obout:JustifyCenter />
                                                        <obout:JustifyRight />
                                                        <obout:JustifyFull />
                                                        <obout:HorizontalSeparator />
                                                        <obout:SpellCheck />
                                                       
                                                    </AddButtons>
                                                </TopToolbar>
                                            </obout:Editor>
                                            <asp:RequiredFieldValidator ID="rfvContent" ControlToValidate="edContent" EnableClientScript="True" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                          
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            </ContentTemplate>
               </asp:UpdatePanel>
</asp:Content>

