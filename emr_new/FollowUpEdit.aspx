<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="FollowUpEdit.aspx.cs" Inherits="FollowUpEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781">
			<td>
				<strong><font color="#000000">Followup Type Update</font></strong>
			</td>
			<td>
				&nbsp;
			</td>
		</tr>
		<tr>
			<td width="133">
				Name
			</td>
			<td width="414">
				<%= Fup.AptTypeDesc%>
			</td>
		</tr>
		<tr>
			<td>
				Viewable (y/n)
			</td>
			<td>
				<asp:CheckBox ID="cboViewable" runat="server" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" OnClick="btnUpdate_Click" />&nbsp;
				<input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent','FollowupTypeMaint.aspx');return document.MM_returnValue"
					value="Cancel" />
			</td>
		</tr>
	</table>
</asp:Content>
