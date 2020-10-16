<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="NewTicketNoPatient.aspx.cs" Inherits="NewTicketNoPatient" EnableEventValidation="false" %>

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
    <script src="Scripts/NewTicketNoPatient.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<input type="hidden" value="<%= Session["StaffID"] %>" id="SessionTextBox" />
    <div class="centered" >
		<input type="hidden" id="inpPatientID" runat="server" />
		<br />
		<table width="90%" border="0" cellpadding="6" cellspacing="0" class="border" >
			<tr bgcolor="#D6B781">
				<td colspan="3">
					<p>
						<b>New Staff Ticket</b></p>
				</td>
			</tr>
			<tr>
				<td class="regText">
					<strong>Assign to</strong>				
                      <asp:RadioButtonList ID="rdoDept" runat="server"
                        RepeatDirection="Horizontal" AutoPostBack="false" RepeatLayout="Flow">
                        <asp:ListItem onclick="BindAssignDropDown('Employee')" Text="Employee" Value="Emp" Selected="True"   />
                        <asp:ListItem onclick="BindAssignDropDown('Department')" Text="Department" Value="Dept"  />
                    </asp:RadioButtonList>
                    <asp:DropDownList ID="ddlAssign" runat="server"  CssClass="FormField"  Width="160px"   />
                    
				</td>
				<td class="regText">
					<strong>Category </strong>
					<asp:DropDownList ID="AptType" CssClass="FormField" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="regText">
					<strong>Severity</strong>&nbsp
					<asp:RadioButtonList ID="rdoSeverity" runat="server" RepeatDirection="Horizontal" 
						RepeatLayout="Flow" >
						<asp:ListItem Text="High" Value="1" class="padding_r29" />
						<asp:ListItem Text="Normal" Value="2" Selected="True"  class="padding_r29" />
						<asp:ListItem Text="Low" Value="3"  class="padding_r29" />
					</asp:RadioButtonList>
				</td>
				<td class="regText">
					<strong>Due Date </strong>
					<asp:TextBox ID="txtDueDate" runat="server" CssClass="regText FormField" Text="" style="width: 155px;" /><cc1:CalendarExtender
						ID="calDue" runat="server" TargetControlID="txtDueDate"  />
				</td>
			</tr>
		</table>
		<br />
		<table width="90%" class="border">
			<tr>
				<td valign="top" style="width: 800px;">
					<strong><asp:Label ID="lblSubject" runat="server" Text="Subject:" CssClass="regText " /></strong>
					<asp:TextBox ID="txtSubject" runat="server" style="width:314px"  CssClass="FormField"/>
                      <input type="button" value="Submit" id="SubmitTicketID" class="button" style="float:right" />	
				</td>
			</tr>
			<tr>
				<td colspan="3">					
                  			
					<obout:Editor ID="ed" runat="server" Height="350px" Width="100%" >
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
								<custom:ImmediateImageInsert ID="btnImageInsert" runat="server" />
							</AddButtons>
						</TopToolbar>
					</obout:Editor>
				</td>
			</tr>
		</table>
	</div>	
</asp:Content>
