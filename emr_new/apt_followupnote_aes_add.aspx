<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="apt_followupnote_aes_add.aspx.cs" Inherits="apt_followupnote_aes_add" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
	TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
	TagPrefix="obout" %>
<%@ Register Namespace="CustomPopups" TagPrefix="custom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td bgcolor="#D6B781"><span class="style1"><strong>Appointment – Aesthetic Follow Up Note Entry </strong></span></td>
            <td bgcolor="#D6B781">
                <div align="right">
                  <%--  <input name="Button" type="button" class="button" onclick="MM_goToURL('parent','manage.aspx?patientid=<%=(Patientname.Fields.Item("PatientID").Value)%>    ');return document.MM_returnValue" value="Back to Patient Details">--%>
                </div>
            </td>
        </tr>
       <%-- <tr bgcolor="#D6B781" class="regText">
            <td width="81%" bgcolor="#D6B781"><b>Patient Name:</b> <%=(Patientname.Fields.Item("FirstName").Value)%>&nbsp;<%=(Patientname.Fields.Item("LastName").Value)%></td>
            <td width="19%" bgcolor="#D6B781">
                <div align="right">
                    <% If Request.QueryString("aptid") <> "" Then %>
                    <input name="Button" type="button" class="button" onclick="MM_goToURL('parent','apt_console.asp?aptid=<%=(Request.QueryString("aptid"))%>    ');return document.MM_returnValue" value="Back to Apt Console">
                    <% Else %>
                    <input name="BackButton" type="button" class="button" id="BackButton" onclick="tmt_winHistory('self','-1')" value="Back">
                    <% End If %>
                </div>
            </td>
        </tr>--%>
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
					<cc1:calendarextender ID="estStart" runat="server" TargetControlID="txtRangeStart" />
					&nbsp;&nbsp;&nbsp;&nbsp;<strong>End Range </strong>
					<asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText" Width="80" />
					<cc1:calendarextender ID="extEnd" runat="server" TargetControlID="txtRangeEnd" />
					
					<br />
					<asp:RequiredFieldValidator ID="reqStart" runat="server" ControlToValidate="txtRangeStart"
						ForeColor="Red" ErrorMessage="You must enter a range start" Display="Dynamic" />
					
				</td>
			</tr>
		</table>
    <br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClientClick="return validateDate();"
			CssClass="button" />
    <br />
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
</asp:Content>
