<%@ Page Title="Import Web Contacts" Language="C#" MasterPageFile="~/Outside.master" AutoEventWireup="true"
	CodeFile="admin_SharepointPatientTracking.aspx.cs" Inherits="admin_SharepointPatientTracking" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<p style="font-size:large;">Import Web Contacts to SharePoint</p>
	<asp:FileUpload ID="FileUpload1" runat="server" />
	<br /><br />
	<asp:Button ID="btnImport" runat="server" Text="Go" CssClass="button" />
</asp:Content>
