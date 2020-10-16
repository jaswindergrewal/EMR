<%@ Page Title="Intake Form Habits" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_habits.aspx.cs" Inherits="intake_form_habits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span style="font-size: 14px; font-weight: bold;">Health Habits </span>
        <br>
        Which substances do you consume:
    </p>
    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr style="bgcolor: #CCCCCC" class="regText">
                        <td style="width: 100px"><strong>Substance</strong></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Alcohol Type: </td>
                        <td>
                            <asp:TextBox ID="txtAlcohol_type" runat="server" class="FormField" MaxLength="50"></asp:TextBox>


                        </td>
                        <td>Amount: 
              
                            <asp:TextBox ID="txtAlcohol_amount" runat="server" class="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>How often: 
            
                            <asp:TextBox ID="txtAlcohol_frequency" runat="server" class="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Drugs Type:</td>
                        <td>

                            <asp:TextBox ID="txtDrugs_type" runat="server" class="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>Amount:
            
                            <asp:TextBox ID="txtDrugs_amount" runat="server" class="FormField" MaxLength="50"></asp:TextBox>

                        </td>
                        <td>How often:
            
                            <asp:TextBox ID="txtDrugs_frequency" runat="server" class="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Cigarettes</td>
                        <td>

                            <asp:TextBox ID="txtCig_packs_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            packs / day</td>
                        <td>

                            <asp:TextBox ID="txtCig_years" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            years </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Chewing Tabacco </td>
                        <td>

                            <asp:TextBox ID="txtChew_amount_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            amount / day</td>
                        <td>

                            <asp:TextBox ID="txtChew_years" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            years </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <table style="width: 900px; bgcolor: #FFFFFF" border="0" cellpadding="6" cellspacing="0">
                    <tr class="border">
                        <td><strong>Are you interested in quitting any of these? 
                            <asp:RadioButtonList ID="rdbQuitYn" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Y</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                            </asp:RadioButtonList></strong>


                        </td>
                    </tr>
                </table>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td style="width: 100px">Caffeine</td>
                        <td>

                            <asp:TextBox ID="txtCaffeine_serv_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            servings / day </td>
                    </tr>
                    <tr class="regText">
                        <td>Nutrasweet</td>
                        <td>

                            <asp:TextBox ID="txtNutrasweet_serv_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            servings / day </td>
                    </tr>
                    <tr class="regText">
                        <td>Saccharin</td>
                        <td>

                            <asp:TextBox ID="txtSaccharin_serv_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            servings / day </td>
                    </tr>
                    <tr class="regText">
                        <td>MSG</td>
                        <td>

                            <asp:TextBox ID="txtMsg_serv_per_day" runat="server" class="FormField" MaxLength="3"></asp:TextBox>
                            servings / day </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="900" border="0" cellpadding="10" cellspacing="0">
        <tr>
            <td>
                <div align="center">

                    <asp:Button ID="ButtonSubmit" runat="server" CssClass="button" Text="Next Page ->" OnClick="ButtonSubmit_Click" />

                </div>
            </td>
        </tr>
    </table>
    <div>
        <script src="Scripts/jquery-1.7.2.js" type="text/javascript">
        </script>
        <script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript">
        </script>
        <script src="Scripts/jquery.filter_input.js" type="text/javascript">
        </script>
        <script type="text/javascript">
            $(function () {
                $('#<%= txtCig_packs_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtCig_years.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtChew_amount_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtChew_years.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtCaffeine_serv_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtNutrasweet_serv_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtSaccharin_serv_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });
                $('#<%= txtMsg_serv_per_day.ClientID %>').filter_input({ regex: '[-0-9]' });

            });
        </script>
    </div>
</asp:Content>

