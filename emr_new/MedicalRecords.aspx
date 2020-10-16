<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="MedicalRecords.aspx.cs" Inherits="MedicalRecords" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
	TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <script type="text/javascript">
         //Function added by jaswinder to validate dates 
         //12 th Sep 2013
         function validateDate() {
             var startDate = new Date(document.getElementById('<%=txtRangeStart.ClientID%>').value);
              var endDate= new Date(document.getElementById('<%=txtRangeEnd.ClientID%>').value);
              var bool = false;
        
              var today = new Date();

              var dd, mm, yyyy;
              dd = today.getDate();
              mm = today.getMonth()+1;
              yyyy = today.getFullYear();
              var CurrentDate = mm + "/" + dd + "/" + yyyy;
              var currdate1 = new Date(CurrentDate);

              var editorObject = $find("<%= ed.ClientID %>");
            var _content = editorObject.get_content();

            if (document.getElementById('<%=txtRangeStart.ClientID%>').value == '') {
                alert('Please enter range start date !');
                document.getElementById('<%=txtRangeStart.ClientID%>').focus();
            }

       
            else if (startDate < currdate1) {
   
                document.getElementById('<%=txtRangeStart.ClientID%>').focus();
                alert('Date cannot be less than current date!');
         

            }
            else if(document.getElementById('<%=txtRangeEnd.ClientID%>').value == '') {
                alert('Please enter range end date !');
                document.getElementById('<%=txtRangeEnd.ClientID%>').focus();
            }
            
            else if(endDate < startDate) {
                alert('End date cant be smaller than start date !');
                document.getElementById('<%=txtRangeEnd.ClientID%>').focus();
        }
           
        else if (_content == '')
        {
            alert('Please enter the content');
            return false;
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
		<table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
			<tr bgcolor="#D6B781" class="regText">
				<td bgcolor="#D6B781">
					<span class="style1"><strong>Medical Records Request Entry </strong>
					</span>
				</td>
				<td bgcolor="#D6B781">
					<div align="right">
						<input name="Button" type="button" class="button" onclick="MM_goToURL('self','PatientInfo.aspx?patientid=<%=PatientID%>');return document.MM_returnValue"
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
						&nbsp;</div>
				</td>
			</tr>
		</table>
		<br />
		<table width="600" border="0" cellpadding="6" cellspacing="0" class="borderText">
			<tr bgcolor="#D6B781">
				<td>
					<strong>Follow Up Date </strong>(Enter the range of dates for this request to be
					done- [mm/dd/yyyy] )
				</td>
			</tr>
			<tr>
				<td>
					<strong>Start Range</strong>
					<asp:TextBox ID="txtRangeStart" runat="server" CssClass="borderText" Width="80" />
					<cc1:CalendarExtender ID="estStart" runat="server" TargetControlID="txtRangeStart" />
					&nbsp;&nbsp;&nbsp;&nbsp;<strong>End Range </strong>
					<asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText" Width="80" />
					<cc1:CalendarExtender ID="extEnd" runat="server" TargetControlID="txtRangeEnd" />
					<br />
					<asp:RequiredFieldValidator ID="reqStart" runat="server" ControlToValidate="txtRangeStart"
						ForeColor="Red" ErrorMessage="You must enter a range start" Display="Dynamic" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validateDate();" OnClick="btnSubmit_Click"
			CssClass="button" />
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
