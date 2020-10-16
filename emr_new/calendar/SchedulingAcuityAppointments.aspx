<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchedulingAcuityAppointments.aspx.cs"  MasterPageFile="Site.master" Inherits="calendar_SchedulingAcuityAppointments" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />
 
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div style="margin-left:25px;margin-top:50px;">
        
        <asp:Button ID="btnDownloadAppointments" text="HA Appointments" runat="server" ClientIDMode="Static" OnClick="btnDownloadAppointments_Click"/>
         <asp:Button ID="btnBellamedica" text="Bella Medica Appointments" runat="server" ClientIDMode="Static" OnClick="btnBellamedica_Click"/>
    </div>
    <div id="loadingdiv" runat="server" class="DivCalendarBlock">
        <div id="loading-div" class="ui-corner-all">
            <img src="../images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>
</asp:Content>
