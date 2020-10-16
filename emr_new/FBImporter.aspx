<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FBImporter.aspx.cs" Inherits="FBImporter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script>
        function msg(EventName) {
            alert("EventName: " + EventName +" Not exists.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button Text="import FB data" ID="FbImporter" runat="server" OnClick="FbImporter_Click" />
</asp:Content>

