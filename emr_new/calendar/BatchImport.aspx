<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchImport.aspx.cs" Inherits="BatchImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<p>
			<asp:FileUpload ID="batchUpload" runat="server"></asp:FileUpload></p>
		<p>
			<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Import" /></p>

	</div>
	</form>
</body>
</html>
