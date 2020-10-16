<%@ Page Language="C#" Title="Protocol Add" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="protocols_protocol_add.aspx.cs" Inherits="protocols_protocol_add" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- function added by jaswinder on 16th aug 2013 to validate HTML editor--%>
    <script type="text/javascript">
        function validateData() {
            var editorObject = $find("<%= ed.ClientID %>");
            var _content = editorObject.get_content();

            if ($('[id$=txtTitle]').val() == "")
            {
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

    <table width="637px"  border="0" cellpadding="2" cellspacing="0" class="border">
        <tr>
            <td>
                <p><b>Protocol Add</b></p>
            </td>
        </tr>
        <tr style="border-bottom:1px; border-bottom-color:black ; vertical-align:top;">
            <td >
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validateData();"
                    CssClass="button" OnClick="btnSubmit_Click" /> &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                    CssClass="button" OnClick="btnCancel_Click"  />
            </td>
        </tr>
        <tr>
            <td valign="middle"><strong>Title :&nbsp;</strong><asp:TextBox ID="txtTitle" runat="server" CssClass="FormField"></asp:TextBox></td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <obout:Editor ID="ed" runat="server" Height="200px" Width="630">
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
