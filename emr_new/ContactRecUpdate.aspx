<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="ContactRecUpdate.aspx.cs" Inherits="ContactRecUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <script type="text/javascript">
         //Function added by jaswinder to validate data 
         //9th sept 2013
         function validateData() {

             var bool = false;


             var editorObject = $find("<%= ed.ClientID %>");
            var _content = editorObject.get_content();


            if (_content == '') {
                alert('Please enter the content'); return false;
            }
            else {
                bool = true;

            }

            return bool;

        }
    </script>
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
			CssClass="button" OnClientClick="return validateData();" />
			 <obout:Editor ID="ed" runat="server" Height="600px" Width="600" >
						<TopToolbar Appearance="Custom">
							<AddButtons>
								<obout:Bold />
								<obout:Italic />
								<obout:Underline />
								<obout:StrikeThrough />
								<obout:HorizontalSeparator />
								<obout:FontName />
								<obout:FontSize />
								<obout:VerticalSeparator />
								<obout:Undo />
								<obout:Redo />
								<obout:HorizontalSeparator />
								<obout:PasteWord />
								<obout:HorizontalSeparator />
								<obout:JustifyLeft />
								<obout:JustifyCenter />
								<obout:JustifyRight />
								<obout:JustifyFull />
								<obout:HorizontalSeparator />
								<obout:SpellCheck />
								
							</AddButtons>
						</TopToolbar>
					</obout:Editor>
	</div>
</asp:Content>
