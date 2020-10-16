<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FrequencyControl.ascx.cs"
	Inherits="Controls_FrequencyControl" %>
<asp:DropDownList runat="server" ID="ddFrequemcy" DataSourceID="FrequencySource"
	DataTextField="FrequencyName" DataValueField="FrequencyMonths"
	OnSelectedIndexChanged="ddFrequemcy_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
<asp:SqlDataSource runat="server" ID="FrequencySource" ProviderName="System.Data.SqlClient"
	SelectCommand="SELECT * FROM Frequencies order by [FrequencyID]"></asp:SqlDataSource>
