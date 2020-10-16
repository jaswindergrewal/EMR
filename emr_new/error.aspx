<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p class="regText">An error has occurred in the system.  Details have been sent to IT.</p>

            <div id="DivErrorMessage" runat="server"></div>
            <p class="regText"><a href="mailto:IT@longevitymedicalclinic.com">Please click here to send an email to IT</a> and let us know in detail what you were doing at the time.</p>


            <p class="regText">Thanks for your patience!</p>

            <p class="regText"><a href="~/LandingPage.aspx">Click here</a> to continue.</p>
        </div>
    </form>
</body>
</html>
