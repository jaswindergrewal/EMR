<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_stafflogin_edit.aspx.cs" Inherits="admin_stafflogin_edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function setPwd() {
            var Pwd = "<%=Pwd %>";
            var txtText = document.getElementById("Password");
            txtText.value = Pwd;
        }

        function Validate() {
            var EmployeeName = document.getElementById('EmployeeName');
            var Password = document.getElementById('Password');

            var errString = "";
            if (EmployeeName.value == "") {
                errString += "You must enter an Employee Name.";
            }
            if (Password.value == "") {
                errString += "\r\nYou must enter Password.";
            }

            if (errString == "")
                return true;
            else {
                alert(errString);
                return false;
            }
        }
    </script>

    <table width="686" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td colspan="2"><b>Edit Staff Login</b></td>
        </tr>
        <tr valign="top">
            <td width="143">Employee Name </td>
            <td width="517">
                <asp:TextBox runat="server" CssClass="FormField" ID="EmployeeName" size="50" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td>Username</td>
            <td>
                <asp:Label ID="lblUsername" runat="server"></asp:Label></td>
        </tr>

        <tr valign="top">
            <td>Password</td>
            <td>
                <asp:TextBox TextMode="Password" CssClass="FormField" ID="Password" runat="server" ClientIDMode="Static"></asp:TextBox></td>
        </tr>
        <tr valign="top">
            <td>Can Write Prescriptions </td>
            <td>
                <asp:CheckBox ID="CanWritePresc" runat="server" />
            </td>
        </tr>
         <tr valign="top">
            <td>HA REP </td>
            <td>
                <asp:CheckBox ID="chkHARep" runat="server" />
            </td>
        </tr>
        <tr valign="top">
            <td>Active </td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" />
            </td>
        </tr>
        <tr valign="top">
            <td>Access Level </td>
            <td>
                <select name="AccessLevel" class="FormField" id="AccessLevel" runat="server">
                    <option value="staff" selected="selected">General Staff</option>
                    <option value="rn">Nurse</option>
                    <option value="dr">Doctor</option>
                    <option value="emr_admin">EMR Admin</option>
                </select></td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Button CssClass="button" Text="Update" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" OnClientClick="return Validate();" />

                <input name="Button" type="button" class="button" onclick="MM_goToURL('parent', 'admin_stafflogin_list.aspx'); return document.MM_returnValue" value="Cancel"></td>
            <td>
                <p>&nbsp;</p>
            </td>
        </tr>
    </table>

</asp:Content>
