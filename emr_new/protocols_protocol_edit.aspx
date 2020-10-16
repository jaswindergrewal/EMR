<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="protocols_protocol_edit.aspx.cs" Inherits="protocols_protocol_edit" %>


<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- function added by jaswinder on 20th aug 2013 to validate HTML editor--%>
    <script type="text/javascript">
        function validateData() {
            var editorObject = $find("<%= ed.ClientID %>");
            var _content = editorObject.get_content();

            if ($('[id$=txtTitle]').val() == "") {
                alert("Please enter title");
                return false;
            }
            else if (_content == '') {
                alert('Please enter the content');
                return false;
            }
            else {
                if (!confirm("This Document is about to be submitted\nAre you sure you have finished editing?")) {
                    return false;
                }
                else { return true; }
            }
        }

    </script>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <table style="width:673px; height:229px; z-index:27; left: 10px; top: 128px; " class="border">
        <tr>
             <td height="37"  valign="top"><span class="PageTitle">Protocol Update</span><span class="regText"> </span><div align="right"></div></td>
        <td valign="top"><div align="right"><span class="regText">[<a href="protocols_protocol_list.aspx">Protocol Home</a>] </span></div></td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validateData();"
                    CssClass="button" OnClick="btnSubmit_Click" /> &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                    CssClass="button"  OnClick="btnCancel_Click" />
            </td>
        </tr>
       
      <tr>
        <td width="16%" height="25" valign="top"><strong>Title</strong>:</td>
        <td width="82%" height="25" colspan="2" valign="top"><asp:TextBox ID="txtTitle"  runat="server" CssClass="FormField"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="25" valign="top"><strong>Written By</strong>: </td>
        <td height="25" colspan="2" valign="top"><%= lstProtocol.EnteredBy%></td>
      </tr>
      <tr>
        <td height="25" valign="top"><strong>Date Written</strong>:</td>
        <td height="25" colspan="2" valign="top"><%= lstProtocol.DateEntered%></td>
      </tr>
      <tr>
        <td height="25" valign="top"><strong>Last Updated</strong>: </td>
        <td height="25" colspan="2" valign="top"><%= lstProtocol.Lastupdated%></td>
      </tr>
        <tr><td></td></tr>
        <tr>
            <td colspan="2">
                <obout:Editor ID="ed" runat="server" Height="200px" Width="100%">
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


