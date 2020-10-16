<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="lab_report_drilldown.aspx.cs" Inherits="lab_report_drilldown" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" width="550" class="report">
	<tr >

		<td style="width:300px"><b>Observation Time</b></td>
		<td style="width:250px"><b>Observation Value</b></td>

	</tr>
    <tr>
        <td><asp:Label  runat="server"></asp:Label></td>
        <td ><asp:Label ID="Label1"  runat="server"></asp:Label></td>
       
     </tr>
    <asp:Literal ID="litTestDetails" runat="server"></asp:Literal>
    </table>

</asp:Content>

