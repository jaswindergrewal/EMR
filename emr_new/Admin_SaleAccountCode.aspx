<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin_SaleAccountCode.aspx.cs" Inherits="Admin_SaleAccountCode" %>

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
                    <p><b>Xero Sales Account Code</b></p>
                </td>                
            </tr>
            <tr>
                <td>Sales Account: <asp:TextBox id="txtSalesAccount" runat="server" ></asp:TextBox>
                <td><asp:Button ID="btnSumit" runat="server" Text="Submit"  CssClass="button" OnClick="btnSumit_Click"/></td>
            </tr>
           
        </table>

   
            </ContentTemplate>
               </asp:UpdatePanel>
</asp:Content>


