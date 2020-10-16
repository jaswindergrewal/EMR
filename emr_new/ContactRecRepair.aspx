<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="ContactRecRepair.aspx.cs" Inherits="ContactRecUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
	TagPrefix="cc2" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<div>
		<input type="hidden" id="inpPatientID" runat="server" />
		<table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781">
				<td>
					<strong>Contact Details </strong>
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					<input name="Button" type="button" class="button" onclick="MM_goToURL('parent','Manage.aspx?PatientID=<%=PatientID%>');return document.MM_returnValue"
						value="Back to Patient Profile">
				</td>
			</tr>
			<tr>
				<td width="194" nowrap>
					<strong>Patient Name </strong>
				</td>
				<td>
					<asp:Label ID="lblPatientName" runat="server" />
				</td>
				<td width="100">
					<strong>Date Entered</strong>
				</td>
				<td>
					<asp:Label ID="lblContactDateEntered" runat="server" />
				</td>
			</tr>
			<tr>
				<td width="194" nowrap>
					<strong>Category</strong>
				</td>
				<td width="315">
					<asp:Label ID="lblAptTypeDesc" runat="server" />
				</td>
				<td width="100">
					<strong>Entered By</strong>
				</td>
				<td width="125">
					<asp:Label ID="lblEnteredBy" runat="server" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
			CssClass="button" />
		<obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
			ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14" Submit="false"
			AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="true">
			<buttons>
				<obout:Toggle Name="Bold" />
				<obout:Toggle Name="Italic" />
				<obout:Toggle Name="Underline" />
				<obout:Toggle Name="StrikeThrough" />
				<obout:HorizontalSeparator />
				<obout:Method Name="ClearStyles" />
				<obout:HorizontalSeparator />
				<obout:Select Name="FontName" />
				<obout:Select Name="FontSize" />
				<obout:HorizontalSeparator />
				<obout:VerticalSeparator />
				<obout:Method Name="Undo" />
				<obout:Method Name="Redo" />
				<obout:HorizontalSeparator />
				<obout:Method Name="PasteWord" />
				<obout:HorizontalSeparator />
				<obout:Method Name="JustifyLeft" />
				<obout:Method Name="JustifyCenter" />
				<obout:Method Name="JustifyRight" />
				<obout:Method Name="JustifyFull" />
				<obout:HorizontalSeparator />
				<obout:Method Name="SpellCheck" />
				<obout:HorizontalSeparator />
				<obout:Method Name="IncreaseHeight" />
				<obout:Method Name="DecreaseHeight" />
				<obout:TextIndicator />
			</buttons>
		</obout:Editor>	</div>
</asp:Content>
