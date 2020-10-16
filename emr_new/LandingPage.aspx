<%@ Page Title="My EMR" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LandingPage.aspx.cs" Inherits="LandingPage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

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

        html, body, iframe {  width:100%;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style ="width:100%;">
        <tr>
            <td width="50%"><input type="button" value="Ticket" class="button" onclick="ShowDiv(1);"/>
    <input type="button" value="Calendar" class="button" onclick="ShowDiv(2);"/></td>
            <td>  <p class="PageTitle"><asp:Label ID="lblticketName" runat="server" ClientIDMode="Static"></asp:Label></p></td>
        </tr>
        <tr>
            <td colspan="2"> <iframe frameborder="0" id="PageContents" runat="server"  height="1000px"
                        src="" style="overflow: auto;" /></td>
        </tr>
    </table>
    
    

   
</asp:Content>