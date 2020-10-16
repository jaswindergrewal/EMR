<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="allergies_edit.aspx.cs" Inherits="allergies_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function checkBlank()
        {
            var al=$('#<%= txtAllergies.ClientID %>');            
            if(al.val()=="")
            {
                alert("Please enter the value.");
                al.focus();
                return false;
            }
        }

   
            function textboxMultilineMaxNumber(txt,maxLen){
                try{
                    if(txt.value.length > (maxLen-1))return false;
                }catch(e){
                }
            }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtAllergies" runat="server"  TextMode="MultiLine"  CssClass="FormField" Width="582px" Height="152px" onkeypress="return textboxMultilineMaxNumber(this,100)" MaxLength="100"/>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAllergies" runat="server" class="button" Text="Save" OnClick="btnAllergies_Click" OnClientClick="return checkBlank();" />
            
                <asp:Button ID="btnCancel" runat="server" class="button" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
            <td>&nbsp;
            </td>
        </tr>
       
    </table>
</asp:Content>
