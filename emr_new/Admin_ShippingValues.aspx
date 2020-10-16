<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin_ShippingValues.aspx.cs" Inherits="Admin_ShippingValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
       <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td  colspan="2">
                    <p><b>Shipping Values</b></p>
                </td>
                
            </tr>
            <tr>
                <td> Shipping Fee: </td>
                <td><asp:TextBox runat="server" ID="txtShippingFee"></asp:TextBox></td>
              
            </tr>
            <tr>
             <td> Order Limit: </td>
                <td><asp:TextBox runat="server" ID="txtOrderLimit"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnSumit" runat="server" Text="Submit" CssClass="button" OnClick="btnSumit_Click"/> &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"/>
                </td>
            </tr>
            

        </table>
</asp:Content>

