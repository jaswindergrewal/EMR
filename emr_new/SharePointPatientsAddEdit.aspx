<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SharePointPatientsAddEdit.aspx.cs" Inherits="SharePointPatientsAddEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="Scripts/jquery-1.8.3.js"></script>
        <script type="text/javascript" src="Scripts/Common.js"></script>
    <script type="text/javascript">
        function fncheck() {
            debugger;
            var Clinic = $("#ddlClinic").val();
            if (Clinic == "0") {
                alert('Select Clinic...');
                Clinic.focus();
                return false;
            }
            var Provider = $("#ddlProvider").val();
            if (Provider == "0") {
                alert('Select Provider...');
                Provider.focus();
                return false;
            }
            var StartRange = $("#txtStartRange").val();
            if (StartRange == "") {
                alert('Start Range should not be empty...');
                StartRange.focus();
                return false;
            }
            var Name = $("#txtFirstName").val(); //document.getElementById("<%=txtFirstName.ClientID%>").value.trim();
            if (Name == "") {
                alert('First Name should not be empty...');
                Name.focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" border="0" cellpadding="5" class="borderText">
        <tr>
            <td><strong>Clinic:</strong></td>
            <td>
                <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormField clinic" Width="150px"></asp:DropDownList></td>

        </tr>
        <tr>
            <td><strong>Provider:</strong></td>
            <td>
                <asp:DropDownList ID="ddlProvider" runat="server" CssClass="FormField clinic" Width="150px"></asp:DropDownList></td>

        </tr>
        <tr>
            <td><strong>Start Range:</strong></td>
            <td>
                <asp:TextBox ID="txtStartRange" runat="server" ClientIDMode="Static"/>
                <cc1:CalendarExtender ID="CalStartRange" runat="server" TargetControlID="txtStartRange" />
            </td>
        </tr>
        <tr>
            <td><strong>End Range:</strong></td>
            <td>
                <asp:TextBox ID="txtEndRange" runat="server" />
                <cc1:CalendarExtender ID="CalEndRange" runat="server" TargetControlID="txtEndRange" />

                <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Start date must come before end date."
                    ControlToValidate="txtStartRange" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEndRange"
                    Display="Dynamic" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td><strong>First Name:</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtFirstName" ClientIDMode="Static"></asp:TextBox></td>

        </tr>
        <tr>
            <td><strong>Last Name:</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtLastName" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>duration:</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtDuration" ClientIDMode="Static" onkeypress="return check_digit(event,this,8,2);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>Phone:</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtPhone" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>Note</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtNotes" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="button" runat="server" OnClientClick="javascript:return fnCheck()"/>
                <%--<input name="button" type="button" class="button" id="button" value="Update" onclick="Validate('Edit')" />
                --%> &nbsp;
                <input name="cancel" type="submit" class="button" id="cancel" onclick="MM_goToURL('parent', 'ListSharePointPatients.aspx'); return document.MM_returnValue" value="Cancel" /></td>
        </tr>
    </table>
    <asp:HiddenField ID='hdnDiagnosisID' runat="server" ClientIDMode="Static" />
</asp:Content>


