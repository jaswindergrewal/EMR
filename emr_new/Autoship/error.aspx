<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<title>Error</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<p class="regText">An error has occurred in the system.  Details have been sent to IT.</p>

	<p class="regText"><a href="mailto:IT@longevitymedicalclinic.com">Please click here to send an email to IT</a> and let us know in detail what you were doing at the time.</p>

	<p class="regText">Thanks for your patience!</p>

   <p class="regText"> Click <a href="../landingpage.aspx">here</a> to continue.</p>
</asp:Content>

