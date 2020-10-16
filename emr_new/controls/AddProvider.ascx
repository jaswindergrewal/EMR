<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddProvider.ascx.cs" Inherits="Controls_AddProvider" %>
<table>
	<tr>
		<th colspan="2">
		Add Provider
		</th>

	</tr>
	<tr>
		<td>
			Name:
		</td>
		<td>
			<asp:TextBox runat="server" ID="ProviderName" Text="" />
		</td>
	</tr>
	<tr>
	<td colspan="2"><asp:Button runat="server" ID="AddButton" Text="Add" OnClick="AddButton_OnClick" /></td>
	</tr>
</table>
