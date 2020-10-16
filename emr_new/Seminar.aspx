<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/Site.master" AutoEventWireup="true"
	CodeFile="Seminar.aspx.cs" Inherits="CRM_Seminar" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<p class="PageTitle">
		Manage Seminar</p>
	<p class="regText">
		Event:
		<asp:DropDownList ID="ddlEvent" runat="server" DataSourceID="sqlEvent" DataTextField="EventName"
			DataValueField="EventID" />
	</p>
	<p class="regText">
		Clinic:
		<asp:DropDownList ID="ddlClinic" runat="server" DataSourceID="sqlClinic" DataTextField="ClinicName"
			DataValueField="ClinicID" />
	</p>
	<asp:SqlDataSource ID="sqlEvent" runat="server" SelectCommand="select * from CRM_Events order by EventName"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
	<asp:SqlDataSource ID="sqlClinic" runat="server" SelectCommand="select * from Clinics order by ClinicName"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
</asp:Content>
