<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Longerity Medical Clinic::Login</title>

   <%-- <link href="css/jqueryui/southstreet/jquery-ui-min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="css/Common.css" />

</head>
<body style="background:url(images/export/beige_back.gif);">
    <form id="form1" runat="server">
         <div id="Layer166" style="position: absolute; left: 10px; top: 10px; width: 22px; height: 22px; z-index: 1">
            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/export/layout1_r1_c1.gif"
                Width="100" Height="97" border="0" alt="" />
        </div>
        <div id="Layer21" style="position: absolute; left: 121px; top: 13px; z-index: 2">
            <img src="images/lmc_logo.gif" width="180" height="88" alt="" />
        </div>
        <div id="main">
            
            <div id="tray" class="box">
                <p class="f-left box">
                    
                      
                    <%-- &nbsp;&nbsp;&nbsp;&nbsp; 
                    <img src="Images/lmc_logo.gif" alt="Longerity Medical Clinic" title="Longerity Medical Clinic" />--%>
                </p>
                <p class="f-right">
                </p>
            </div>
            <h1 style="border: 1px solid #AFAFAF;"></h1>
            <div id="cols" class="box">
                <div id="Logincontent" class="box" style="margin-left: 400px ! important; width: 450px; height: 200px!important;">
                    <div style="margin-left: 120px; padding-top: 10px;">
                        <asp:Label ID="lblMessage" runat="server" CssClass="errorMessage" Visible="false"></asp:Label>
                    </div>
                    <table style="text-align: center; margin-left: 50px;">
                        <tr>
                            <td>
                                <h2>Login</h2>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td><span class="requiredStarMark">*</span>User Name :
                            </td>
                            <td>&nbsp;<asp:TextBox ID="txtUserName" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>

                            <td colspan="2" style="padding-right: 48px;">
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                    CssClass="aspDotNetValidation" ErrorMessage="Please enter your user name." SetFocusOnError="true" ControlToValidate="txtUserName" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="requiredStarMark">*</span>Password : 
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 48px;">
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                    CssClass="aspDotNetValidation" ErrorMessage="Please enter your password." SetFocusOnError="true" ControlToValidate="txtUserName" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding-left: 260px;">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="Login" CssClass="btnLogin" OnClick="btnLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
