<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="apt_followupnote_aes_add_Short.aspx.cs" Inherits="apt_followupnote_aes_add_Short" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
	TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
	TagPrefix="obout" %>

        <script type="text/javascript">
            //Function added by jaswinder to validate dates 
            //12 th aug 2013
            function validateDate() {
                var startDate = new Date(document.getElementById('<%=txtRangeStart.ClientID%>').value);
            var endDate = new Date(document.getElementById('<%=txtRangeEnd.ClientID%>').value);
            var bool = false;

            var today = new Date();

            var dd, mm, yyyy;
            dd = today.getDate();
            mm = today.getMonth() + 1;
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
            else if (document.getElementById('<%=txtRangeEnd.ClientID%>').value == '') {
                alert('Please enter range end date !');
                document.getElementById('<%=txtRangeEnd.ClientID%>').focus();
        }

        else if (endDate < startDate) {
            alert('End date cant be smaller than start date !');
            document.getElementById('<%=txtRangeEnd.ClientID%>').focus();
        }

        else if (_content == '') {
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <%-- <form name="fHtmlEditor" method="POST" action="<%=MM_editAction%>">--%>
<table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
         <tr bgcolor="#D6B781" class="regText">
           <td  bgcolor="#D6B781"><span class="style1"><strong>Appointment – Aesthetic Follow Up Note Entry </strong></span></td>
           <td  bgcolor="#D6B781"><div align="right">
            
               <asp:Button ID="btnPatientDetails" runat="server" CssClass="button" Text="Back to Patient Details" />
           </div></td>
         </tr>
         <tr bgcolor="#D6B781" class="regText">
      <td width="81%"  bgcolor="#D6B781"><b>Patient Name:</b>
         
          <asp:Label ID="lblFirstName" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblLastName" runat="server"></asp:Label>
      </td>
      <td width="19%"  bgcolor="#D6B781"><div align="right">
         
        <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back"  />
       

      </div></td>
    </tr> 
</table>
<br>

    <Script Language="JavaScript" src="ScriptLibrary/datepicker.js"></Script>
<span id="calendar"></span>
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="borderText">
      <tr bgcolor="#D6B781">
        <td><strong>Follow Up Date</strong> (Enter the range of dates for this request to be done- [mm/dd/yyyy] )</td>
      </tr>
      <tr>
        <td>
             <strong>Start Range</strong><span class="Validation_StarMark_Color">*</span>
                    <asp:TextBox ID="txtRangeStart" runat="server" CssClass="borderText readOnly" Width="80"  />
                    <cc1:calendarextender ID="estStart" runat="server" TargetControlID="txtRangeStart" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<strong>End Range</strong><span class="Validation_StarMark_Color">*</span>
                    <asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText readOnly" Width="80" />
                    <cc1:calendarextender ID="extEnd" runat="server" TargetControlID="txtRangeEnd" />
                    <br />
                    <asp:RequiredFieldValidator ID="reqStart" runat="server" ControlToValidate="txtRangeStart"
                        ForeColor="Red" ErrorMessage="You must enter a range start" Display="Dynamic" ValidationGroup="FollowUpsNote" />
        </td>
      </tr>
    </table>
    <br>
    <table width="600" height="40%" border="0" cellpadding="0" cellspacing="0" class="border">
  <tr valign="top">
    <td><table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr valign="top">
          <td valign="top"><div id=editbar >
              <table width="100%" border="0" cellpadding="0" cellspacing="0" align="left">
                <tr>
                  <td><table border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td><table border="0">
                            <tr valign="baseline">
                              <td nowrap>
                                  <%--<input name="Button" type="button" class="button" onClick="OnFormSubmit()" value="Submit">                                --%>
                                  <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="FollowUpsNote" OnClientClick="return validateDate();" OnClick="btnSubmit_Click"/>
                                </td>
                            </tr>
                        </table></td>
                     
                      </tr>
                  </table></td>
                </tr>
                </table>
          </div></td>
        </tr>
    </table></td>
  </tr>
  <tr valign="top" align="left">
    <td valign="top"><table width="100%" border="0" >
        <tr valign="top">
          <td width="100%" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr valign="top">
                <td bgcolor="#FFFFFF">
                 
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


                </td>
              </tr>
          </table></td>
          <!-- COLORPICKER_GOES_HERE -->
        </tr>
    </table></td>
  </tr>
</table>

<%--<p>
      <span class="regText">
      <input name="EnteredBy" type="hidden" id="EnteredBy" value="<%=Session("UserID")%>">
      <input name="aptid" type="hidden" id="aptid" value="<%= Request.QueryString("aptid") %>">
      <input name="PatientID" type="hidden" id="PatientID" value="<%= Request.QueryString("PatientID") %>">
      </span>
      <input name="FollowUpType" type="hidden" id="FollowUpType" value="5">
      <input type="hidden" name="MM_insert" value="fHtmlEditor">
</p>--%>
  <p><strong> <img class='clsCursor' src="editor_images/new.gif" width="1" height="1" border="0""><img class='clsCursor' src="editor_images/save.gif" width="1" height="1" border="0" ;></strong></p>
 <%-- </form>--%>
</asp:Content>

