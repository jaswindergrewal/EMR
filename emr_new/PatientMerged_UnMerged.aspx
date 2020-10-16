<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientMerged_UnMerged.aspx.cs" Inherits="PatientMerged_UnMerged" %>


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
    <script type="text/javascript" src="Scripts/PatientMerge.js"></script>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnStaff" Value="0" runat="server" ClientIDMode="Static" />
    <div id="Tabs" style="margin-top: 10px; margin-left: 15px; z-index: 1000px;">
        <ul class="tabs">
            <li id="liGeneralTab"><a href="#" id="lnkMerging" runat="server" clientidmode="Static" onclick="BindDataToMerge('#grdBindDataToMerge','#pagernav1','1')">Merged</a></li>
            <li><a href="#" runat="server" id="lnkUnMerging" clientidmode="Static" onclick="BindDataToUnMerged('#grdBindDataToUnMerged','#pagernav2','2')">UN Merged</a></li>
            
        </ul>
    </div>
    <div class="uiform">
        <div id="Div2" class="panes" >
            <div id="DataToMerge">
                <asp:HiddenField ID="hdnPatientID" Value="0" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMergedPatientID" Value="0" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPatientName" Value="0" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMergedPatientName" Value="0" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnRowId" Value="0" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnMerge" CssClass="button" runat="server" OnClientClick="return fnMergePatientData();" Text="Merged Data"/><br />
                <table id="grdBindDataToMerge"></table>
                <div id="DivNoRecord1" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav1"></div>
            </div>
            <div id="DataToUnMerged">
                <table id="grdBindDataToUnMerged"></table>
                <div id="DivNoRecord2" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav2"></div>
            </div>
            
        </div>
    </div>
    


</asp:Content>

