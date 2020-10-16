<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="contact_record_add.aspx.cs" Inherits="contact_record_add" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script type="text/javascript">
         //Function added by jaswinder to validate dates 
        
         function validateData() {
          
             var editorObject = $find("<%= edTicket.ClientID %>");
            var _content = editorObject.get_content();

           
            
         
           
         if (_content == '')
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td width="81%" bgcolor="#D6B781"><b>Patient Name:</b><asp:Label runat="server" ID="lblPatientName"></asp:Label>
                </td>
                <td width="19%" bgcolor="#D6B781">
                    <div align="right">
                        <input name="Button" type="button" class="button" onclick="MM_goToURL('parent','manage.aspx?patientid=<%=patientId%>    ');return document.MM_returnValue" value="Back to Patient Details" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="2">
                    <p><b>Add Contact Record</b> (Medical Entry,Phone call, Email, etc)</p>
                </td>
            </tr>
            <tr>
                <td class="regText"><strong>Contact Type </strong></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlApptType" AppendDataBoundItems="true" DataValueField="AptTypeId" DataTextField="AptTypeDesc">
                        <asp:ListItem Text="Select Appt Type" Value=""></asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ID="rfvAppType" ControlToValidate="ddlApptType" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="regText"></td>
                <td></td>
            </tr>

        </table>

        <table>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnSaveNote" Text="Submit" CssClass="button" OnClick="btnSaveNote_Click" OnClientClick="return validateData();" />
                </td>
            </tr>
        </table>
        <table width="600" height="40%" border="0" cellpadding="0" cellspacing="0" class="border">

            <tr valign="top" align="left">
                <td valign="top">
                    <table width="100%" border="0">
                        <tr valign="top">
                            <td width="100%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr valign="top">
                                        <td bgcolor="#FFFFFF">
                                            <obout:Editor ID="edTicket" runat="server" Height="300px" Width="600px">
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
                                            <asp:RequiredFieldValidator ID="rfvContent" ControlToValidate="edTicket" EnableClientScript="True" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-- COLORPICKER_GOES_HERE -->
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

