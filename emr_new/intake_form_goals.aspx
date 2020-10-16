<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="intake_form_goals.aspx.cs" Inherits="intake_form_goals" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span class="style1">Your Goals</span>
        <br>
        What do you hope to achieve in your participation in the Nonage Longevity Program?
    </p>

    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td>
                            <asp:CheckBoxList runat="server" ID="chkGoals" RepeatColumns="3" RepeatDirection="Horizontal" Width="900" >
                                <asp:ListItem Text="Balance Hormones " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Start Hormones " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Energy " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Feel Better " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Feel Stronger  " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Sleep " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Get Rid of Hot Flashes " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Get off Prescriptions " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Weight Loss" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Stabalize PMS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Stop Hair Loss" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Sense of Well Being " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Enhance Immune System " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Less Pain" Value="1" onclick="javascript: if(this.checked) { document.getElementById('tdlesspain').style.display = 'inline';} else { document.getElementById('tdlesspain').style.display = 'none'; document.getElementById('txtLessPain').value='';  };"></asp:ListItem>
                                <asp:ListItem Text="Improve Libido " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Sex Life " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Muscle " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Memory " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Bladder Control   " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Skin " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Better Stamina " Value="1"></asp:ListItem>
                                <asp:ListItem Text="General Wellness " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Reduce Stress  " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Improve Metabolism " Value="1"></asp:ListItem>
                              
                            </asp:CheckBoxList>

                        </td>
                    </tr>
                </table>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="border" id="tdlesspain" style="display:none;">
                        <td width="100">Less pain where</td>
                        <td>
                            <textarea name="LessPain" cols="50" rows="3" class="border" id="txtLessPain" runat="server" clientidmode="Static" maxlength="50"></textarea></td>
                    </tr>
                    <tr class="border">
                        <td width="100">other symptoms</td>
                        <td>
                            <textarea name="other_Goals" cols="50" rows="3" class="border" id="txtother_Goals" runat="server" maxlength="50"></textarea></td>
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
