<%@ Page Language="C#" AutoEventWireup="true" CodeFile="special_attn_flag.aspx.cs" MasterPageFile="~/sub.master" Inherits="special_attn_flag" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <%-- function added by jaswinder on 14th aug 2013 to validate HTML editor--%>
    <script type="text/javascript">
        function validateData() {
          var editorObject = $find("<%= ed.ClientID %>");
          var _content = editorObject.get_content();
          if (_content == '') {
              alert('Please enter the content');
             
              return false;
          }
          else {
              if (!confirm("This Document is about to be submitted\nAre you sure you have finished editing?")) {
                  return false;
              }
              else { return true;}
          }
        }

        
        function onKeyPress()
        {
            alert(oboutGetEditor('editor').getContent().length);
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <table width="100%" height="210" border="1" cellpadding="2" cellspacing="0" class="border">
        <tr>
            <td>
                <p><b>Add Special Attention Note </b></p>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validateData();"
                    CssClass="button" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                    CssClass="button" OnClick="btnCancel_Click" />

                <obout:Editor ID="ed" runat="server" Height="200px" Width="600" onkeypress="onKeyPress();" >
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
