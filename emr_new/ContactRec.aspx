<%@ Page Title="Add Contact Record" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="ContactRec.aspx.cs" Inherits="_ContactRec" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <input type="hidden" id="inpPatientID" runat="server" />
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td width="81%" bgcolor="#D6B781">
                    <b>Patient Name:</b>
                    <asp:Label ID="lblPatientName" runat="server" />
                </td>
                <td width="19%" bgcolor="#D6B781">&nbsp;
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
                    <asp:DropDownList ID="AptType" CssClass="FormField" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
            CssClass="button"  OnClientClick="return validateData();"/>
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

    </div>
</asp:Content>
