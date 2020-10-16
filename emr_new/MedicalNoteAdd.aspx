<%@ Page Title="Add Medical Note" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="MedicalNoteAdd.aspx.cs" Inherits="MedicalNoteAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
	TagPrefix="cc2" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<div>
		<input type="hidden" id="inpPatientID" runat="server" />
		<table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td bgcolor="#D6B781">
					<span class="style1"><strong>Appointment - Medical Note Entry </strong></span>
				</td>
				<td bgcolor="#D6B781">
					<div align="right">
						<input name="Button" type="button" class="button" onclick="MM_goToURL('parent','manage.aspx?patientid=<%=PatientID%>');return document.MM_returnValue"
							value="Back to Patient Details" />
					</div>
				</td>
			</tr>
			<tr bgcolor="#D6B781" class="regText">
				<td width="81%" bgcolor="#D6B781">
					<b>Patient Name:</b>
					<asp:Label ID="lblPatientName" runat="server" />
				</td>
				<td width="19%" bgcolor="#D6B781">
					<div align="right">
						<input name="Button" type="button" class="button" onclick="MM_goToURL('parent','apt_console.aspx?aptid=<%=ApptID%>');return document.MM_returnValue"
							value="Back to Apt Console" />
					</div>
				</td>
			</tr>
		</table>
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
			CssClass="button" />
		<obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
			ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14" Submit="false"
			AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="false">
			<Buttons>
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
				<obout:Method Name="Preview" />
				<obout:HorizontalSeparator />
				<obout:Method Name="IncreaseHeight" />
				<obout:Method Name="DecreaseHeight" />
				<obout:TextIndicator />
			</Buttons>
		</obout:Editor>
	</div>
</asp:Content>
