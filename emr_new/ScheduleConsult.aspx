<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="ScheduleConsult.aspx.cs" Inherits="ScheduleConsult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

	<script language="javascript" type="text/javascript">
			
	    //Function added by jaswinder to validate dates 
        //12 th aug 2013
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
					<span class="style1"><strong>Appointment - Schedule a Consult </strong></span>
				</td>
				<td bgcolor="#D6B781">
					<div align="right">
                           <asp:Button ID="btnManage" runat="server" CssClass="button" Text="Back to Patient Details" OnClick="btnManage_Click" />
						<%--<input name="Button" type="button" class="button" onclick="MM_goToURL('self','Manage.aspx?patientid=<%=PatientID%>');return document.MM_returnValue"
							value="Back to Patient Details" />--%>
                       
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

                         <asp:Button ID="btnAptConsol" CssClass="button" Text="Back to appointment" runat="server" OnClick="btnAptConsol_Click"/>
						</div>
				</td>
			</tr>
		</table>
		<br />
		<table width="600" border="0" cellpadding="6" cellspacing="0" class="borderText">
			<tr bgcolor="#D6B781">
				<td>
					<strong>Consult Date Range </strong>
				</td>
				<td>
					<strong>Consult Category </strong>
				</td>
			</tr>
			<tr>
				<td>
					<strong>Range Start</strong><span class="Validation_StarMark_Color">*</span>
					<asp:TextBox ID="txtRangeStart" runat="server" CssClass="borderText readOnly" Width="80"  />
					<cc1:CalendarExtender ID="estStart" runat="server" TargetControlID="txtRangeStart" />
					&nbsp;&nbsp;&nbsp;&nbsp;<strong>Range End</strong><span class="Validation_StarMark_Color">*</span>
						<asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText readOnly" Width="80"  />
						<cc1:CalendarExtender ID="extEnd" runat="server" TargetControlID="txtRangeEnd"   />
					<br />
					
				</td>
				<td>
					<asp:DropDownList ID="AptType" runat="server" CssClass="FormField" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validateDate();"
			CssClass="button" />
	<obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
		ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14pt" Submit="false"
		AjaxWait="true" Height="600" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="false">
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
			<obout:method name="Preview" />
			<obout:HorizontalSeparator />
			<obout:Method Name="IncreaseHeight" />
			<obout:Method Name="DecreaseHeight" />
			<obout:TextIndicator />
		</Buttons>
	</obout:Editor>
	</div>
</asp:Content>
