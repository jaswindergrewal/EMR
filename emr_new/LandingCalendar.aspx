<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/sub.master" CodeFile="LandingCalendar.aspx.cs" Inherits="LandingCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

        <script type="text/javascript" src="Scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.20.js"></script>
    <script type="text/javascript" src="Scripts/jquery.jqGrid.js"></script>
    <script type="text/javascript" src="Scripts/grid.locale-en.js"></script>
    <link href="css/base/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/base/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/Tabs/jquery.tools.min(1).js" type="text/javascript"></script>
    <script src="Scripts/Tabs/Tabs.js" type="text/javascript"></script>
    <link href="Scripts/Tabs/Tabs.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Tabs/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/LandingPage.js"></script>
    <style type="text/css">

        .ui-jqgrid tr.jqgrow td.wrap {
    white-space: normal !important;
    height:auto;
    padding-top:2px;
}

.ui-jqgrid tr.jqgrow td
{
    vertical-align:text-top;
}
    </style>
    <script>

        $(document).ready(function () {

          
            BindCalendarTicketData('#grdcalActiveTickets1', '#pagernavcal1', '1');

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <div id="Tabs" style="margin-top: 10px; margin-left: 15px; z-index: 1000;">
        <ul class="tabs">
            <li id="liGeneralTab"><a href="#" id="lnkGeneralTab" runat="server" clientidmode="Static" onclick="BindCalendarTicketData('#grdcalActiveTickets1','#pagernavcal1','1')">calendar Tickets</a></li>
            <li><a href="#" runat="server" id="lnkGroupTickets" clientidmode="Static" onclick="BindCalendarTicketData('#grdGroupTickets2','#pagernav2','2')">Group Tickets Due Now</a></li>
            <li><a href="#" runat="server" id="lnkFutureTickets" clientidmode="Static" onclick="BindCalendarTicketData('#grdFutureTickets3','#pagernav3','3')">My Future Tickets</a></li>
            <li><a href="#" runat="server" id="lnkGroupFuture" clientidmode="Static" onclick="BindCalendarTicketData('#grdGroupFuture4','#pagernav4','4')">Group Future Tickets</a></li>
            <li><a href="#" runat="server" id="lnkActiveICreated" clientidmode="Static" onclick="BindCalendarTicketData('#grdActiveICreated5','#pagernav5','5')">Active Tickets I Created</a></li>
            <li><a href="#" runat="server" id="lnkClosedICreated" clientidmode="Static" onclick="BindCalendarTicketData('#grdClosedICreated6','#pagernav6','6')">Closed Tickets I Created</a></li>
            
        </ul>
    </div>
    <div class="uiform">
        <div id="Div2" class="panes" >
            <div id="GeneralInfo">
                <table id="grdcalActiveTickets1"></table>
                <div id="DivNoRecordcal1" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernavcal1"></div>
            </div>
            <div id="GroupTickets">
                <table id="grdGroupTickets2"></table>
                <div id="DivNoRecord2" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav2"></div>
            </div>
            <div id="FutureTickets">
                <table id="grdFutureTickets3"></table>
                <div id="DivNoRecord3" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav3"></div>
            </div>
            <div id="GroupFuture">
                <table id="grdGroupFuture4"></table>
                <div id="DivNoRecord4" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav4"></div>
            </div>
            <div id="ActiveICreated">
                <table id="grdActiveICreated5"></table>
                <div id="DivNoRecord5" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav5"></div>
            </div>
            <div id="ClosedICreated" style="height: 100%; width: 100%">
                <table id="grdClosedICreated6"></table>
                <div id="DivNoRecord6" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav6"></div>
            </div>

           
        </div>
    </div>
</asp:Content>