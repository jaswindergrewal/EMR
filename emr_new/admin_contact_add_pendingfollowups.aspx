<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_contact_add_pendingfollowups.aspx.cs" Inherits="admin_contact_add_pendingfollowups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <script type="text/javascript">
        //Function added by jaswinder to validate data 
        //12 th aug 2013
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td bgcolor="#D6B781"><b>Patient Name:</b>  <%= PendingRequest.PatientName %></td>
            <td nowrap="nowrap" bgcolor="#D6B781">
                <div align="right">
                    <asp:Button ID="btnManage" runat="server" CssClass="button" Text="Back to Patient Details" OnClick="btnManage_Click" />
                  <%--  <input name="Button" type="button" class="button" onclick="MM_goToURL('parent','manage.aspx?patientid=<%= PatientID.ToString()%>    ');return document.MM_returnValue" value="Back to Patient Details" />--%>
                </div>
            </td>
        </tr>
    </table>
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr>
            <td>
                <strong>Follow Up Type </strong>
            </td>
            <td>
                <strong>Date Entered </strong>
            </td>
            <td>
                <strong>Date Range for Follow Up </strong>
            </td>
            <td>
                <strong>Close?</strong>
            </td>
        </tr>
        <tr>
            <td>
                <%= PendingRequest.FollowUp_Type_Desc %>
            </td>
            <td>
                <%= PendingRequest.DateEntered != null ? ((DateTime)PendingRequest.DateEntered).ToShortDateString() : ""  %>
            </td>
            <td>[<%= PendingRequest.Range_Start != null ? ((DateTime)PendingRequest.Range_Start).ToShortDateString() : " "%>]
				- [<%= PendingRequest.Range_End != null ? ((DateTime)PendingRequest.Range_End).ToShortDateString() : " "%>]
            </td>
            <td>
                <%= PendingRequest.FollowUp_Completed_YN	%>
                <%= (bool)PendingRequest.FollowUp_Completed_YN == false ? " - <a href='admin_pending_consult_close.aspx?followup_id=" + FollowUp_ID.ToString() + "&patientid=" + PatientID.ToString() + "'>[Close]</a>" :"" %>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Request Details </strong>
            </td>
            <td colspan="3">
                <%= PendingRequest.FollowUp_Body %>
            </td>
        </tr>
    </table>
    <br />
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td colspan="2">
                <p>
                    <b>Add Contact Record</b> (Medical Entry,Phone call, Email, etc)
                </p>
            </td>
        </tr>
        <tr>
            <td class="regText">
                <strong>Contact Type </strong>
            </td>
            <td>
                <asp:DropDownList ID="rptAptTypes" runat="server" />
                <span class="regText">
                    <input name="PatientID" type="hidden" id="PatientID" value="<%= Request.QueryString["PatientID"] %>" />
                    <input name="EnteredBy" type="hidden" id="EnteredBy" value="<%=Session["UserID"]%>" />
                    <input name="followUp_id" type="hidden" id="followUp_id" value="<%=Request.QueryString["followup_ID"]%>" />
                </span>
            </td>
        </tr>
    </table>
    <div id="Layer3" style="position: absolute; width: 400px; z-index: 27; left: 629px; top: 140px;">
        <table width="100%" border="0" cellpadding="3" cellspacing="0" class="border" style="position: absolute">
            <tr>
                <td bgcolor="#D6B781">
                    <strong>Contact Entries for this Consult Request </strong>
                </td>
            </tr>
            <asp:Repeater ID="rptContacts" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <strong>
                                <%# Eval("ContactDateEntered") %></strong> - <strong>
                                    <%# Eval("AptTypeDesc") %></strong><br>
                            <%# Eval("MessageBody") %>
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
            <tr>
                <td>
                    <br>
                    <p>
                        <%if (PendingRequest.Apt_ID > 0)
                          { %>

                        <input name="Button" type="button" class="button" onclick="MM_goToURL('parent','apt_console.aspx?aptid=<%= PendingRequest.Apt_ID.ToString()%>    ');return document.MM_returnValue" value="Back to Apt Console" />
                        <%} %>
                    </p>
                    <p>
                        <input name="Button" type="button" class="button" onclick="tmt_winHistory('self','-1')" value="Back" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <br />
                <p>
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                        CssClass="button" OnClientClick="return validateData();" />
                    <obout:Editor ID="ed" runat="server" Height="600px" Width="600">
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
    </table>
</asp:Content>
