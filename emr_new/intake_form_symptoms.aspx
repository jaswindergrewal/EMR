<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_symptoms.aspx.cs" Inherits="intake_form_symptoms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span class="style1">Current or Recent Symptoms </span>
        <br>
        Check any symptom that you have noticed recently.
    </p>

    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td>
                            <asp:CheckBoxList runat="server" ID="chkSymptoms" RepeatColumns="4" RepeatDirection="Horizontal" Width="900">
                                <asp:ListItem Text="Chest Pain " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Nose Bleeds " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Abdominal Pain " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Difficulty Swallowing " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Kidney Pain  " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Change in Headaches " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Bone Pain " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Excessive Thirst " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Recent Change in Bowel Habit" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Weight Loss - Unexpected" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Blood in Sputum " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Shortness of Breath " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Acid Reflux " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Loss of Appetite " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Blood in Urine " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Double Vision " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Unusual Bruising " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Rapid Heart Beat " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Fainting / Collapse  " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Swollen Ankles " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Black Tarry Stools " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Persistent Nausea " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Urgent Urination " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Dizzy / Spinning " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Prolonged Bleeding " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Unusual Cough " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Leg Pain with Walking " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Snoring Excessively " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Bright Blood in Stool " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Mood Swings " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Frequent Urination " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Eye Pain " Value="1"></asp:ListItem>
                                <asp:ListItem Text="Bloated " Value="1"></asp:ListItem>

                            </asp:CheckBoxList>

                        </td>
                    </tr>
                </table>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="border">
                        <td width="100">other symptoms</td>
                        <td>
                            <textarea name="other_symptoms" cols="50" rows="3" class="border" id="other_symptoms" runat="server"></textarea></td>
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
