<%@ Page Title="EMR Admin" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="XEROCredential.aspx.cs" Inherits="XEROCredential" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">

        .XeroCredentialCss {
            border:1px solid #003366;
            text-align:center;
            background-color:#E7D5B4;
            width:50%;
            margin-left: auto;
            margin-right: auto;
        }

        .XeroCredentialCssHeading {
             background-color:#d6b781;
              border:1px solid #003366;
             height:20px;
             font-size:18px;
        }
        .XeroCTable {
            padding:10px 20px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#DetailsOfXeroC").show();
            $("#EditOfXeroC").hide(); 
            $("#EditXeroCredentialsButton").click(function () {
                $("#DetailsOfXeroC").hide();
                $("#EditOfXeroC").show();
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="width:100%;">
    <div class="XeroCredentialCss" id="DetailsOfXeroC">
        <div class="XeroCredentialCssHeading">
        <label>Xero Credential</label></div>
    <table class="XeroCTable">
        <tr><td><asp:Label runat="server" ID="XeroConsumerKeyLbl" Text="Xero Consumer Key:-"></asp:Label></td>
        <td><asp:Label runat="server" ID="XeroConsumerKey"></asp:Label></td></tr>
         <tr><td><asp:Label runat="server" ID="XeroConsumerSecretLbl" Text="Xero Consumer Secret:-"></asp:Label></td>
        <td><asp:Label runat="server" ID="XeroConsumerSecret"></asp:Label> <asp:HiddenField ID="IDXeroCredentials" runat ="server" /> </td></tr>
     <tr><td></td>
            <td> <input type="button" name="Edit" value="Edit" id="EditXeroCredentialsButton"/></td></tr>
    </table>
    </div>
    <div class="XeroCredentialCss" id="EditOfXeroC">
        <div class="XeroCredentialCssHeading">
        <label>Xero Credential</label></div>
    <table class="XeroCTable">
        <tr><td><asp:Label runat="server" ID="XeroConsumerKeyLblEdit" Text="Xero Consumer Key:-"></asp:Label></td>
        <td><asp:TextBox runat="server" ID="XeroConsumerKeyEdit" Columns="40"></asp:TextBox></td></tr>
         <tr><td><asp:Label runat="server" ID="XeroConsumerSecretLblEdit" Text="Xero Consumer Secret:-"></asp:Label></td>
        <td><asp:TextBox runat="server" ID="XeroConsumerSecretEdit" Columns="40"></asp:TextBox></td></tr>
        <tr><td></td>
            <td><asp:Button runat="server" ID="SaveXeroCredentialsButton" Text="Save" onclick="SaveXeroCredentialsButton_Click"/></td></tr>
    </table>
    </div>
       </div>
</asp:Content>