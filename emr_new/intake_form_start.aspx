<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_start.aspx.cs" Inherits="intake_form_start" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p><span class="style1">Confidential Health History</span></p>


    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td height="108">
                <table width="100%" height="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td valign="top">
                            <p>You are about to start the confidential Health History with Longevity Medical Clinic.</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="900" border="0" cellpadding="10" cellspacing="0">
        <tr>
            <td>
                <div align="center">

                    <asp:Button CssClass="button" ID="btnNext" Text="Next Page ->" runat="server" OnClick="btnNext_Click" />

                </div>
            </td>
        </tr>
    </table>


</asp:Content>

