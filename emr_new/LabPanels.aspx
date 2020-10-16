<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabPanels.aspx.cs" MasterPageFile="~/Site.master" Inherits="LabPanels" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" >
        <tr>
            <td>
                <table width="90%">
                    <tr>
                        <td width="20%"><strong>Panel Name:</strong></td>
                        <td><asp:TextBox ID="txtpanelName" runat="server" CssClass="FormFieldWhite" Width="250px"></asp:TextBox></td>

                    </tr>
                     <tr>
                        <td><strong>Panel Description:</strong></td>
                        <td><textarea ID="txtDescription" runat="server" cols="50" rows="7" class="FormFieldWhite"></textarea></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" />
                            &nbsp; <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />
                        </td>

                    </tr>
                </table>

            </td>
        </tr>
        

        
        
    </table>
</asp:Content>
