<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientFormXERO.aspx.cs" Inherits="PatientFormXERO"
    EnableEventValidation="false" %>


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
    <script type="text/javascript" src="Scripts/PatientFormXero.js"></script>
    <style type="text/css">
        .ui-jqgrid tr.jqgrow td.wrap {
            white-space: normal !important;
            height: auto;
            padding-top: 2px;
        }

        .ui-jqgrid tr.jqgrow td {
            vertical-align: text-top;
        }

                 .ModelLbl {
             font-size:13px;
             font-family:Verdana, Arial, sans-serif;
         }
         .floatLnPadding {
             padding:2px 1px 3px 1px;
             float:left;
         }
         .floatLnMargin {
             margin:2px 1px 3px 1px;
             float:left;
         }
         .BoldL {
             font-weight:500;
             margin-left:2px;
         }
         .DivSearch {
             width:100%;
             float:left;
             border:1px solid #ccc; 
         }
         .DivSearch1 {
             width:100%;
             float:left;
             border:0px solid #aaaaaa; 
         }
         .DialogClass {
             top:50px !important;
             left:225px !important;
         }
         .DialogClass1 {
             top:150px !important;
             left:225px !important;
         }
         .MatchedRow td {
             background-color:#666666;
         }
         .DivSet1 {
             border-right:1px solid #ccc;
             float:left;
             /*width:29.77%;*/
              width:31%;
         }
         .DivSet2 {
             border-right:0px solid #ccc;
             float:left;
             /*width:29.77%;*/
             width:31%;
         }
         .DivSet3 {
             float:left;
             /*padding:1.9px 18px 1.9px 18.5px*/
             padding:1.9px 3px 1.9px 3px;
         }

         .ChangeBackground {
             background-color: #ccc;
         }
         /*.wrap1 {
             width:100px !important;
             color:red;
         }*/
         #Tabs UL LI A {
             text-align:center;
             margin-left:3px;
             margin-right:3px;
             padding-right:5px;
             padding-left:5px;
             width:135px;
         }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="Tabs" style="margin-top: 10px; margin-left: 15px; z-index: 1000;">
        <ul class="tabs">
            <li><a href="#" runat="server" id="A2" clientidmode="Static">TaxRates</a></li>
            <li><a href="#" runat="server" id="A3" clientidmode="Static">CSV</a></li>
            <li><a href="#" id="A4" runat="server" clientidmode="Static" onclick="XeroPatientsData('#grdXeroPatients','#pagernav1','1')">Unmatched Patients</a></li>
            <li><a href="#" id="A1" runat="server" clientidmode="Static" >Accounts</a></li>
        </ul>
    </div>
    <div class="uiform">
        <div id="Div2" class="panes">

            <div id="TaxRates">
                <asp:Button ID="btnTaxrateImport" CssClass="button" Text="Import file" runat="server" OnClick="btnTaxrateImport_Click" ClientIDMode="Static" />
                <asp:FileUpload ID="fldUplodTax" runat="server" />
            </div>
            <div id="CSVUpload">
                <asp:Button ID="Button1" CssClass="button" Text="Upload" runat="server" OnClick="Upload" ClientIDMode="Static" />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Label ID="lblerror" runat="server"></asp:Label>
            </div>

            <div id="XeroPatients">
                <asp:Button ID="btnImportContacts" CssClass="button" OnClick="btnImportContacts_Click" runat="server" Text="Get Xero Contacts" />
                <table id="grdXeroPatients"></table>
                <div id="DivNoRecord1" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav1"></div>
            </div>
             <div id="Accounts">
                <asp:Button ID="btnAccounts" CssClass="button" OnClick="btnAccounts_Click" runat="server" Text="Get Xero Accounts" />
               
            </div>

        </div>

    </div>

     <div id="MatchDialog">
    <div class="DivSearch">
        <div class="DivSet1"><label class="floatLnPadding ModelLbl BoldL" id="FirstNameLbl">First Name:-</label><label class="floatLnPadding ModelLbl" id="FirstName" title=""></label></div>
        <div class="DivSet1"><label class="floatLnPadding ModelLbl BoldL" id="LastNameLbl">Last Name:-</label><label class="floatLnPadding ModelLbl" id="LastName" title=""></label></div>
       <%-- <div class="DivSet2"><label class="floatLnPadding ModelLbl BoldL" id="EmailIdLbl">Email:-</label><label class="floatLnPadding ModelLbl" id="EmailId" title=""></label></div>--%>
        <input type="hidden" id="XeroContactId" />
    </div><br />
    <div class="DivSearch">
        <div class="DivSet1"><label class="floatLnPadding ModelLbl BoldL" id="FirstNameLblT">First Name:-</label><%--<input class="floatLnMargin" type="text" placeholder="First Name" id="FirstNameS" runat="server"/>--%>
        <input class="floatLnMargin" type="text" id="FirstNameS"/><%--<asp:TextBox class="floatLnMargin" id="FirstNameS" runat="server"></asp:TextBox>--%></div>
        <div class="DivSet1"><label class="floatLnPadding ModelLbl BoldL" id="LastNameLblT">Last Name:-</label><%--<input class="floatLnMargin" type="text" placeholder="Last Name" id="LastNameS" runat="server"/>--%><input class="floatLnMargin" type="text" placeholder="Last Name" id="LastNameS"/></div>
      <%--  <div class="DivSet2"><label class="floatLnPadding ModelLbl BoldL" id="EmailIdLblT">Email:-</label>--%><%--<input class="floatLnMargin" type="text" placeholder="Email" id="EmailS" runat="server"/>--%><input class="floatLnMargin" type="text" placeholder="Email" id="EmailS"/></div>
         <div class="DivSet3"><input id="SearchPatientsXeroButton" class="floatLnPadding" type="button" value="Search" onclick="SearchMatchXeroPatientsData('#grdXeroPatientsMatchSearch', '#pagernav2', '2')" /></div>
     
        <div class="DivSearch1">
              <div id="XeroPatientsMatchSearch">
                <%--<asp:Button ID="Button1" CssClass="button" OnClick="btnImportContacts_Click" runat="server" Text="Get Contacts" />--%>
                <%-- <asp:Button ID="Button2" CssClass="button" Text="Import all checked" runat="server" OnClientClick="btnImport_click();" OnClick="btnImport_Click" ClientIDMode="Static"/>--%>
                <table id="grdXeroPatientsMatchSearch"></table>
                <div id="DivNoRecord6" style="visibility: hidden"><span>No Record</span></div>
                <div id="pagernav6"></div>
            </div>
           
    </div>
</div>
</asp:Content>




