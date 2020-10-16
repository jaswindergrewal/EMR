<%@ Page Title="Add New Staff Login" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_stafflogin_add.aspx.cs" Inherits="admin_stafflogin_add" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">

        //function to restrict the Special characters 
        function Restrictspecialchar(e) {
            var key;
            key = e.which ? e.which : e.keyCode;
            if ((key > 64 && key < 91) || (key > 96 && key < 123) || key == 8 || key == 32 || (key >= 48 && key <= 57) || key == 46)
                return true;
            else
                return false;

        }

        //function to validate data before insertion
        function Validate() {

            var txtEmpName = document.getElementById('txtEmployeeName');
            var txtUserName = document.getElementById('txtUsername');
            var txtPassword = document.getElementById('txtPassword');
            var errString = "";

            if (txtEmpName.value == "") {

                errString += "You must enter an Employee Name.";

            }
            if (txtUserName.value == "") {
                errString += "\r\nYou must enter an User Name.";

            }
            if (txtPassword.value == "") {
                errString += "\r\nYou must enter Password.";

            }

            if (errString == "") {

                ProcessStaff();

            }
            else {
                alert(errString);
                return false;
            }
        }

        //Insert data in staff table
        function ProcessStaff() {

            var txtEmpName = document.getElementById('txtEmployeeName').value;
            var txtUserName = document.getElementById('txtUsername').value;
            var txtPassword = document.getElementById('txtPassword').value;
            var presciption = $('#MainContent_CanWritePresc').is(':checked');
            var HARep = $('#MainContent_chkHARep').is(':checked');
            var StaffType = $("#MainContent_drpAceesslevel").val();
            var postData = new Object();
            postData.StaffName = txtEmpName;
            postData.StaffUserName = txtUserName;
            postData.StaffPassword = txtPassword;
            postData.prescription = presciption;
            postData.StaffType = StaffType;
            postData.HARep = HARep;

            $.ajax({
                type: "POST",
                url: "admin_stafflogin_add.aspx/InsertStaff",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    if (res == "false") {
                        alert("user already exists");
                    }
                    else {
                        window.location.href = location.href;
                        window.location.replace("admin_stafflogin_list.aspx");
                        //var url = location.href;
                        //var splitUrl = url.split("emr_new");
                        //alert("staff inserted");
                        //window.location.replace(splitUrl[0] + "emr_new/admin_stafflogin_list.aspx");
                    }

                }
            });
        }



    </script>

    <table width="686" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td colspan="2"><b>Add New Staff Login</b></td>
        </tr>
        <tr valign="top">
            <td width="143">Employee Name<span class="Validation_StarMark_Color">*</span> </td>
            <td width="517">
                <asp:TextBox runat="server" CssClass="FormField" TabIndex="1" ID="txtEmployeeName" size="50" onkeypress="return Restrictspecialchar(event)" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td>Username<span class="Validation_StarMark_Color">*</span></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" TabIndex="2" ID="txtUsername" size="50" onkeypress="return Restrictspecialchar(event)" ClientIDMode="Static"></asp:TextBox></td>
        </tr>

        <tr valign="top">
            <td>Password<span class="Validation_StarMark_Color">*</span></td>
            <td>
                <asp:TextBox TextMode="Password" CssClass="FormField" TabIndex="4" ID="txtPassword" runat="server" ClientIDMode="Static"></asp:TextBox></td>
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
            <td>Access Level </td>
            <td>
                <asp:DropDownList ID="drpAceesslevel" runat="server" CssClass="FormField" TabIndex="5">
                    <asp:ListItem Value="staff">General Staff</asp:ListItem>
                    <asp:ListItem Value="rn">Nurse</asp:ListItem>
                    <asp:ListItem Value="dr">Doctor</asp:ListItem>
                    <asp:ListItem Value="emr_admin">EMR Admin</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr valign="top">
            <td>

                <input type="button" id="btnAdd" value="Submit" onclick="Validate()" class="button" />


            </td>
            <td>
                <p>&nbsp;</p>
            </td>
        </tr>
    </table>


</asp:Content>
