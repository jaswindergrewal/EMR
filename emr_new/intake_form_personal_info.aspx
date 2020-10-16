<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_personal_info.aspx.cs" Inherits="intake_form_personal_info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p><span class="style1">Confidential Health History</span></p>


    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td width="150">First Name</td>
                        <td width="300">
                            <asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td width="300">&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Last Name</td>
                        <td width="300">
                            <asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                        <td>Marital Status</td>
                        <td width="300">
                            <asp:DropDownList runat="server" ID="ddlmarital_status" CssClass="border" >
                                <asp:ListItem Value="NA" Text="- Select -" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                                <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                                <asp:ListItem Value="Divorced" Text="Divorced"></asp:ListItem>
                                <asp:ListItem Value="Significant Other" Text="Significant Other"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Birth Date</td>
                        <td width="300">
                            <asp:Label ID="lblBirthDay" runat="server"></asp:Label></td>

                        <td>Level of Education</td>
                        <td width="300">
                            <asp:DropDownList runat="server" ID="ddlEducation" CssClass="border">
                                <asp:ListItem Value="NA" Text="- Select -" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="High School" Text="High School"></asp:ListItem>
                                <asp:ListItem Value="Bachelors" Text="Bachelors"></asp:ListItem>
                                <asp:ListItem Value="Masters" Text="Masters"></asp:ListItem>
                                <asp:ListItem Value="Doctorate" Text="Doctorate"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Current Occupation </td>
                        <td width="300">
                            <asp:TextBox ID="txtcurrent_occupation" runat="server" CssClass="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                        <td width="150">&nbsp;</td>
                        <td height="26">&nbsp;</td>
                    </tr>
                </table>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td width="250">Is your occupation enjoyable?</td>
                        <td width="638">
                            <asp:RadioButtonList ID="rdloccupation_enjoy_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Is it stressful?</td>
                        <td>
                            <asp:RadioButtonList ID="rdloccupation_stress_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Is it fulfilling?</td>
                        <td>
                            <asp:RadioButtonList ID="rdloccupation_fulfill_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Hazardous Materials Exposure?</td>
                        <td>
                            <asp:RadioButtonList ID="rdloccupation_hazardous_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Are you Retired? </td>
                        <td>
                            <asp:RadioButtonList ID="rdlretired_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr class="regText">
                        <td>If retired, what was your main occupation?</td>
                        <td>
                            <asp:TextBox ID="txtretired_occupation" runat="server" CssClass="FormField" size="35" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>When did you retire? (mm/dd/yyyy)</td>
                        <td>
                            <asp:TextBox ID="txtretired_date_of" runat="server" CssClass="FormField"></asp:TextBox>

                            <cc1:CalendarExtender ID="CtrRetiredate" runat="server" TargetControlID="txtretired_date_of" />

                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Are you happy / content in retirement?</td>
                        <td>
                            <asp:RadioButtonList ID="rdlretired_happy_YN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
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
