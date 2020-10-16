<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="intake_form_allergies.aspx.cs" Inherits="intake_form_allergies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p><span class="style1">Allergies</span></p>

    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td>Are you allergic to any Drugs or Medications?            </td>
                        <td width="100">
                            <asp:RadioButtonList ID="rdldrug_or_med_allergy_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>Please Specify : 
                <asp:TextBox ID="txtdrug_or_med_allergy" runat="server" size="50" CssClass="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Are you allergic to any foods? </td>
                        <td>
                            <asp:RadioButtonList ID="rdlfood_allergy_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>Please Specify :
                 <asp:TextBox ID="txtfood_allergy" runat="server" size="50" CssClass="FormField" MaxLength="50"></asp:TextBox>
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



    <p>&nbsp;</p>
    <p>&nbsp;</p>
</asp:Content>
