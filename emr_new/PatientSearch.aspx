<%@ Page Title="Patient Search" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PatientSearch.aspx.cs" Inherits="PatientSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <th colspan="2" align="center">Patient Search
                    </th>
                </tr>
                <tr>
                    <td align="left" class="regText" colspan="2">Search is updated each time you move from a field.<br />
                        <br />
                        Any or all infomation can be entered. For example, you could enter just the phone number to see if there is a patient in the system using that number.</td>
                </tr>
            </table>
            <p></p>
            <table>
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="regText">First Name:</td>
                                <td>
                                    <p class="regText">

                                        <asp:TextBox ID="txtFirstName" runat="server" AutoPostBack="true" OnTextChanged="TextChanged" CssClass="FormFieldWhite" onkeypress="return Restrictspecialchar(event)"/>
                                    </p>


                                </td>
                            </tr>
                            <tr>
                                <td class="regText">Middle Initial:</td>
                                <td>
                                    <p class="regText">

                                        <asp:TextBox ID="txtMiddleInitial" runat="server" AutoPostBack="true" OnTextChanged="TextChanged" CssClass="FormFieldWhite" onkeypress="return Restrictspecialchar(event)"/>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="regText">Last Name:</td>
                                <td>
                                    <p class="regText">

                                        <asp:TextBox ID="txtLastName" runat="server" AutoPostBack="true" OnTextChanged="TextChanged" CssClass="FormFieldWhite" onkeypress="return Restrictspecialchar(event)"/>
                                    </p>

                                </td>
                            </tr>
                            <tr>
                                <td class="regText">Phone:</td>
                                <td>
                                    <p class="regText">

                                        <asp:TextBox ID="txtHomePhone" runat="server" AutoPostBack="true" OnTextChanged="TextChanged" CssClass="FormFieldWhite" />
                                        <cc1:MaskedEditExtender
                                            ID="mskTbPhone"
                                            runat="server"
                                            TargetControlID="txtHomePhone"
                                            Mask="999-999-9999"
                                            ClearMaskOnLostFocus="true"
                                            MessageValidatorTip="true"
                                            OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="None"
                                            ErrorTooltipEnabled="True" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Create" OnClick="btnAdd_Click" /></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td style="vertical-align: top">
                        <table>
                            <tr>

                                <td>
                                    <asp:ListView ID="lstResults" runat="server" OnSelectedIndexChanged="lstResults_SelectedIndexChanged"
                                        OnSelectedIndexChanging="lstResults_SelectedIndexChanging" DataKeyNames="PatientID">
                                        <LayoutTemplate>
                                            <div style="overflow: auto; height: 400px;">
                                                <table width="100%" style="border-style: solid; border-width: 1px; width: 500px;">
                                                    <tr>
                                                        <td colspan="2" class="largeheading" align="center">
                                                            <string>Matching Names</string>
                                                        </td>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Patient Details" CssClass="button" />
                                                </td>
                                                <td align="center" class="regText">

                                                    <%# (bool)Eval("Inactive")==false ?"<font color='black'>" : "<font color='red'>" %>
                                                    <%# Eval("FirstName") %>
                                                    <%# Eval("MiddleInitial")%>
                                                    <%# Eval("LastName") %><br />
                                                    <%# Eval("HomePhone") %>
                                        </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>

                        </table>
                    </td>


                </tr>

            </table>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
