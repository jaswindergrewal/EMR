<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CombinedSchedule.aspx.cs"
	Inherits="CombinedSchedule" MasterPageFile="Site.master" EnableEventValidation="false"%>

<%--<%@ Register TagName="Cal" TagPrefix="Longevity" Src="~/controls/OneCal.ascx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<div>
		<table >
			<tr id="CalRow" runat="server">
				<td id="AdminStuff" runat="server">
					<asp:Panel runat="server" ID="ProviderPanel" BorderStyle="Solid" BorderWidth="1"
						Width="200">
						<asp:Label ID="Label1" Text="Provider to display" Font-Bold="true" runat="server" />
						<asp:RadioButtonList ID="ProvidersCBox" runat="server" OnSelectedIndexChanged="ProvidersCBox_SelectedIndexChanged"
							AutoPostBack="true" />
					</asp:Panel>
					<asp:Label runat="server" ID="lbl1" Text="Date to show: " /><asp:TextBox runat="server"
						ID="txtStart" Text="" />
					<cc1:CalendarExtender ID="caleExtDate" runat="server" TargetControlID="txtStart" />
					<br />
					<asp:RadioButtonList ID="rdoTime" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Selected="True" Text="30 Minutes" Value="30" />
						<asp:ListItem Text="15 Minutes" Value="15" />
						<asp:ListItem Text="10 Minutes" Value="10" />
						<asp:ListItem Text="5 Minutes" Value="5" />
					</asp:RadioButtonList>
					<asp:Button runat="server" ID="StartSked" Text="Create Schedule" OnClick="StartSked_OnClick"
						Enabled="false" />
					<br />
					<strong>Appointment Type</strong>
					<asp:DropDownList ID="ApptTypeDropDown" runat="server" AutoPostBack="true" DataSourceID="AppointmentTypeSourceAll"
						DataTextField="TypeName" DataValueField="id">
					</asp:DropDownList>
					<br />
					<strong>Status</strong>
					<asp:DropDownList ID="StatusDropDown" runat="server" AutoPostBack="true" DataSourceID="StatusSourceAll"
						DataTextField="StatusName" DataValueField="id">
					</asp:DropDownList>
					<br />
					<asp:ObjectDataSource ID="AppointmentTypeSourceAll" runat="server" TypeName="Calendar.AppointmentTypes"
						SelectMethod="getApptTypeList"></asp:ObjectDataSource>
					<asp:ObjectDataSource ID="StatusSourceAll" runat="server" TypeName="Calendar.Status"
						SelectMethod="getStatusList"></asp:ObjectDataSource>
				</td>
				<td id="Cal" runat="server" visible="false" align="center" height="1000px">
					<asp:Label runat="server" ID="lblProviderName" Text="" Font-Bold="true" />
					<DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" 
                        DataEndField="ApptEnd"
						DataStartField="ApptStart" 
                        DataTextField="Patient" 
                        DataValueField="EventID" 
                        DataTagFields="Patient, EventID, ApptTypeID"
						DataAllDayField="allday" 
                        Days="1"  
                        EventCorners="Rounded"
						EventSelectHandling="CallBack" 
                        ClientObjectName="dpc1" 
                        EventEditHandling="CallBack"
						DataRecurrenceField="Recur" 
                        HeaderLevels="1" Width="750px"  
                        HeightSpec="BusinessHours"
						EventMoveHandling="PostBack" 
                        EventResizeHandling="PostBack" 
                        AllDayEventHeight="30"
						OnBeforeEventRender="DayPilotCalendar1_BeforeEventRender" 
                        OnBeforeHeaderRender="DayPilotCalendar1_BeforeHeaderRender">
					</DayPilot:DayPilotCalendar>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
