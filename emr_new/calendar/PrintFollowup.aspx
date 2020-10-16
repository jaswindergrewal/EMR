<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintFollowup.aspx.cs" Inherits="PrintFollowup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Longevity Calendar</title>
	<link href='css/Calendar.css' type="text/css" rel="stylesheet" />
	<link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
	<link href="css/main.css" rel="stylesheet" type="text/css" />
	<script language="javascript" type="text/javascript">
		function openNewWin(url) {
			var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');
			x.focus();
		} 
	</script>
	<script language="JavaScript" type="text/JavaScript">
<!--
		function MM_goToURL() { //v3.0
			var i, args = MM_goToURL.arguments; document.MM_returnValue = false;
			for (i = 0; i < (args.length - 1); i += 2) eval(args[i] + ".location='" + args[i + 1] + "'");
		}
//-->
	</script>
	<!-- InstanceEndEditable -->
	<script language="JavaScript" type="text/JavaScript">
<!--
		function MM_goToURL() { //v3.0
			var i, args = MM_goToURL.arguments; document.MM_returnValue = false;
			for (i = 0; i < (args.length - 1); i += 2) eval(args[i] + ".location='" + args[i + 1] + "'");
		}

		function MM_findObj(n, d) { //v4.01
			var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
				d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
			}
			if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
			for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
			if (!x && d.getElementById) x = d.getElementById(n); return x;
		}

		function MM_showHideLayers() { //v6.0
			var i, p, v, obj, args = MM_showHideLayers.arguments;
			for (i = 0; i < (args.length - 2); i += 3) if ((obj = MM_findObj(args[i])) != null) {
				v = args[i + 2];
				if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
				obj.visibility = v;
			}
		}
//-->
	</script>
	<script language="JavaScript" type="text/JavaScript">
<!--
		function MM_reloadPage(init) {  //reloads the window if Nav4 resized
			if (init == true) with (navigator) {
				if ((appName == "Netscape") && (parseInt(appVersion) == 4)) {
					document.MM_pgW = innerWidth; document.MM_pgH = innerHeight; onresize = MM_reloadPage;
				}
			}
			else if (innerWidth != document.MM_pgW || innerHeight != document.MM_pgH) location.reload();
		}
		MM_reloadPage(true);
//-->
	</script>
	<script language="javascript" type="text/javascript">
		function TogglePattern() {
			var tble = document.getElementById('recurrencePattern');
			var cBox = document.getElementById('cbRecurring');
			if (cBox.checked) {
				tble.style = 'display:inherit';
			}
			else {
				tble.style = 'display:none';
			}

		}


	
	</script>
	<style type="text/css">
		.MyCalendar .ajax__calendar_container
		{
			border: 1px solid #646464;
			background-color: lemonchiffon;
			color: red;
		}
	</style>
</head>
<body background="../images/export/beige_back.gif">
	<div id="Layer166" style="position: absolute; left: 7px; top: 9px; width: 22px; height: 22px;
		z-index: 1">
		<img name="layout1_r1_c1" src="../images/export/layout1_r1_c1.gif" width="100" height="97"
			border="0" alt="" />
	</div>
	<div id="Layer21" style="position: absolute; left: 121px; top: 13px; z-index: 2">
		<img src="../images/lmc_logo.gif" width="180" height="88" alt="" />
	</div>
	<p>
		&nbsp;</p>
	<p>
		&nbsp;</p>
	<br>
	<p>
		&nbsp;</p>
	<br>
	<form id="form1" runat="server">
	<div>
		<asp:Table ID="tbl" runat="server" Width="600" BorderStyle="Groove" BorderWidth="1px" BorderColor="LightGray" />
	</div>
	</form>
</body>
</html>
