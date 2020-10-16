<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="DepartmentStaff_Add.aspx.cs" Inherits="DepartmentStaff_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.20.js"></script>
    <script type="text/javascript" src="Scripts/jquery.jqGrid.js"></script>
    <script type="text/javascript" src="Scripts/grid.locale-en.js"></script>
    <link href="css/base/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/base/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/Departments.js" type="text/javascript"></script>
    

    <%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="left: 10px; width: 98%;">
        <table border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" style="width: 90% !important" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="3" bgcolor="#D6B781">
                    <p><b>Add Departments</b> </p>
                </td>
                <td>
                    <input type="button" id="btnAddDepartments" value="Add Departments" class="button" />

                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Staff</b></font>
                </td>
                <td>
                    <asp:DropDownList runat="server" CssClass="FormField" ID="ddlStaff" ClientIDMode="Static"/>
                </td>
                <td height="24">&nbsp
                </td>
                <td height="24">&nbsp
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
                <td colspan="3" style="height: 50px; padding-right: 400px;" nowrap="nowrap" valign="middle" align="left">

                    <asp:CheckBoxList runat="server" ClientIDMode="static" ID="cbDepartment" CssClass="DeptCheckBoxClass" RepeatColumns="6" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="button" value="Add" id="btnAdd" class="button" /> &nbsp;
                    <input type="button" value="Cancel" id="btnCancel" class="button" />
                </td>
                <td >
                    
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr bgcolor="#D6B781">
                <td colspan="3" bgcolor="#D6B781">
                    <p><b>List for Employees in Departments</b> </p>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Departments</b></font>
                </td>
                <td>
                    <asp:DropDownList runat="server" CssClass="FormField" ID="ddlDepartments" ClientIDMode="Static" />
                </td>
                <td height="24">&nbsp
                </td>
                <td height="24">&nbsp
                </td>


            </tr>
            <tr>
                <td colspan="4">
                    <div id="DataforEmployee">
                        <table id="grdBindDataEmployee"></table>
                        <div id="DivNoRecord" style="visibility: hidden"><span>No Record</span></div>
                        <div id="pagernav"></div>
                    </div>
                </td>
            </tr>
        </table>


    </div>


    <div id="loading" style="display: none;" visible="false" class="fadePanel ">
        <!-- this is the loading gif -->
        <img src="~/images/indicator.gif" alt="Loading" />
    </div>

    <div id="SemiTransparentBG" class="fadePanel " style="display: none;" visible="false"></div>

    <%-- For Department Div--%>
    <div id="DepartmentPopUp" class="Popup " style="display: none;" visible="false">
        <div class="InnerPopup" style="width: 500px !important; border: double 1px;">
            <img src="images/close_icon.png" id="imgClose" class="closeDiv" />
            <p><strong>Add Department</strong></p>
            <p id="PopUpBody">
                <asp:Label ID="lblDeptName" runat="server" Text="Name:" /><asp:TextBox ID="txtDeptName" CssClass="FormField"
                    runat="server" ClientIDMode="Static" />

            </p>
            <p>
                <input type="button" id="btnOkDept" class="button" value="Add" />
               &nbsp;
                <input type="button" id="btnClose" class="button"
                    value="Close" />
            </p>
        </div>
    </div>
    <%-- End Panel Div--%>
</asp:Content>

