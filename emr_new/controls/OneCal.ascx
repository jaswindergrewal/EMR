<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OneCal.ascx.cs" Inherits="OneCal" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="TDD" TagPrefix="Longevity" Src="~/controls/TimeDropDown.ascx" %>

<asp:UpdatePanel ID="updCal" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<asp:Label runat="server" ID="lblProviderName" Text="" Font-Bold="true" />
		<DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" 
			DataEndField="ApptEnd"
			DataStartField="ApptStart" 
			DataTextField="Patient" 
			DataValueField="EventID"
			DataTagFields="Patient, EventID, ApptTypeID"
			DataAllDayField="allday" 
			Days="1" 
			ContextMenuID="DayPilotMenu1" 
			OnEventMenuClick="DayPilotCalendar1_EventMenuClick"
			OnTimeRangeSelected="DayPilotCalendar1_TimeRangeSelected" 
			TimeRangeSelectedHandling="PostBack"
			EventClickHandling="PostBack" 
			EventCorners="Rounded" 
			EventSelectHandling="CallBack"
			ClientObjectName="dpc1" 
			EventEditHandling="CallBack"
			OnCommand="DayPilotCalendar1_Command"
			OnEventClick="DayPilotCalendar1_EventClick"
			OnBeforeEventRender="DayPilotCalendar1_BeforeEventRender"
			DataRecurrenceField="Recur"
			HeaderLevels="1" 
			Width="100%" 
			HeightSpec="BusinessHours"
			OnEventMove="DayPilotCalendar1_EventMove"
			EventMoveHandling="PostBack" 
			OnEventResize="DayPilotCalendar1_EventResize" 
			EventResizeHandling="PostBack" 
			AllDayEventHeight="30">
		</DayPilot:DayPilotCalendar>
		<asp:Label runat="server" ID="lblProvider1" Text="1" Visible="false" />

		<DayPilot:DayPilotMenu ID="DayPilotMenu1" runat="server" MenuTitle="Set Result" CssClassPrefix="menu_" ShowMenuTitle="true">
			<DayPilot:MenuItem Text="Edit..." Action="PostBack" Command="Open"></DayPilot:MenuItem>
			<DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
			<DayPilot:MenuItem Text="Delete" Action="Callback" Command="Delete"></DayPilot:MenuItem>
           <DayPilot:MenuItem Text="sale made" Action="Callback" Command="salemade" ></DayPilot:MenuItem>
		</DayPilot:DayPilotMenu>

        <DayPilot:DayPilotMenu ID="DayPilotMenu_MenuNew" runat="server" MenuTitle="Set Result" CssClassPrefix="menu_" ShowMenuTitle="true"  >
			<DayPilot:MenuItem Text="Edit..." Action="PostBack" Command="Open"></DayPilot:MenuItem>
			<DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
			<DayPilot:MenuItem Text="Delete" Action="Callback" Command="Delete"></DayPilot:MenuItem>
           <DayPilot:MenuItem Text="sale made" Action="Callback" Command="salemade"  ></DayPilot:MenuItem>
		</DayPilot:DayPilotMenu>

        <DayPilot:DayPilotMenu ID="DayPilotMenu2" runat="server" MenuTitle="Set Result" CssClassPrefix="menu_" ShowMenuTitle="true">
			
			<DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
			
		</DayPilot:DayPilotMenu>

	</ContentTemplate>
</asp:UpdatePanel>
