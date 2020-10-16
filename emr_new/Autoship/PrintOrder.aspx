<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOrder.aspx.cs" Inherits="PrintOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Print Order</title>

</head>
<body>
	<form id="form1" runat="server">
	
	<div id="divArea">
	<asp:Label ID="lblContent" runat="server" />
	</div>
	</form>
</body>
</html>
	<script type="text/javascript" language="javascript">
		window.print();
	</script>