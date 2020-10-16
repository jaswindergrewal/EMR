﻿<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="LabAdd.aspx.cs" Inherits="LabAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
	TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
	TagPrefix="obout" %>
<%@ Register Namespace="CustomPopups" TagPrefix="custom" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
		#apDiv1
		{
			position: absolute;
			width: 183px;
			height: 468px;
			z-index: 27;
			left: 628px;
			top: 126px;
		}
		
		
		#apDiv2
		{
			position: absolute;
			width: 183px;
			height: 468px;
			z-index: 27;
			left: 822px;
			top: 127px;
		}
	</style>

     <script type="text/javascript">
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
					<span class="style1"><strong>Appointment - Lab Request</strong></span>
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
						  <asp:Button ID="btnAptConsol" CssClass="button" Text="Back to appointment" runat="server" OnClick="btnAptConsol_Click"/></div>
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
					<asp:RequiredFieldValidator ID="valStart" runat="server" ControlToValidate="txtRangeStart"
						ErrorMessage="Range start required" ForeColor="Red" Display="Dynamic" />
					&nbsp;&nbsp;&nbsp;&nbsp;<strong>End Range </strong>
					<asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText" Width="80" />
					<cc1:CalendarExtender ID="extEnd" runat="server" TargetControlID="txtRangeEnd" />
					&nbsp;Fasting: <asp:RadioButtonList ID="rdoFasting" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Text="Yes" Value="Yes" />
						<asp:ListItem Text="No" Value="No" />
					</asp:RadioButtonList>
					<asp:RequiredFieldValidator ID="valFasting" runat="server" ControlToValidate="rdoFasting" ForeColor="Red" Display="Dynamic" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validateDate();"
			CssClass="button" />
		  <obout:Editor ID="ed" runat="server" Height="300px" Width="600" >
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
	<div id="apDiv2">
		<table width="100%" border="1" cellpadding="5" cellspacing="0">
			<tr>
				<td>
					<p>
						<strong>Follow Up (4 wk)</strong></p>
					<p>
						Testosterone,<br>
						Progesterone,<br>
						DHEA<br>
						Pregnenolone,<br>
						Estrogen,<br>
						VIT-D<br>
						CMP<br>
						CBC / ferritin<br>
						<br>
					</p>
				</td>
			</tr>
			<tr>
				<td>
					<p>
						<strong>3/4 Month Lab</strong></p>
					<p>
						Lipids<br>
						Thyroid<br>
						HgbA1c<br>
						Nutrition panel<br>
						Estradiol<br>
						Testosterone<br>
						Progesterone<br>
						DHEA<br>
						Pregnenolone<br>
						<br>
					</p>
				</td>
			</tr>
			<tr>
				<td>
					<p>
						<strong>7 month</strong></p>
					<p>
						Lipids<br>
						Thyroid<br>
						Estradiol<br>
						Testosterone<br>
						Progesterone<br>
						DHEA<br>
						CMP<br>
						IGF-1<br>
						<br>
					</p>
				</td>
			</tr>
			<tr>
				<td>
					<p>
						<strong>11 month</strong></p>
					<p>
						IMMUNE PANEL<br>
						Mental function testing<br>
						Sex Hormone Testing<br>
						DEXA<br>
						Nutritional panel</p>
				</td>
			</tr>
		</table>
	</div>
	<div id="apDiv1">
		<table width="100%" border="1" cellpadding="5" cellspacing="0">
			<tr>
				<td bgcolor="#FFFFCC">
					<p>
						<strong>Mandatory in House</strong></p>
					<p>
						<font color="#FF0000">Testosterone<br>
							Progesterone<br>
							Estradiol<br>
							Thyroid<br>
							Pregnenolone<br>
							DHT<br>
							IODINE STUDY</font></p>
				</td>
			</tr>
			<tr>
				<td>
					<p>
						<strong>Nutrient Testing</strong></p>
					<p>
						Vitamins<br>
						Minerals<br>
						Amino Acids<br>
						Antioxidants<br>
						Fatty Acids<br>
						Metabolites<br>
						Carbohydrates Metab</p>
				</td>
			</tr>
		</table>
		<p>
			&nbsp;</p>
	</div>
</asp:Content>
