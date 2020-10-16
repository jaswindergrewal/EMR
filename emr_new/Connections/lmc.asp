<%
' FileName="Connection_odbc_conn_dsn.htm"
' Type="ADO" 
' DesigntimeType="ADO"
' HTTP="false"
' Catalog=""
' Schema=""
MM_lmc_STRING = "Provider=SQLOLEDb;Server=database01;Database=emr_dev;Trusted_Connection=yes"
' MM_lmc_STRING = "DSN=lmc_dev"

'check for cookie
'if no cookie, redirect to landingpage.aspx
'set session vatiables from cookie
Session("UserID") = Request.Cookies("session")("UserID")
Session("OpenTickets") = Request.Cookies("session")("OpenTickets")
Session("MM_Username") = Request.Cookies("session")("MM_Username")
Session("Access_Level") = Request.Cookies("session")("Access_Level")
Session("MM_UserAuthorization") = Request.Cookies("session")("MM_UserAuthorization")
%>