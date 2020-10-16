<%@ Page Title="Search Patient" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SearchResults.aspx.cs" Inherits="SearchResults" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <table width="98%" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr>
                    <td><strong>Search Patients</strong><br />
                        <br />
                        Any or all infomation can be entered. For example, you could enter just the phone number to see if there is a patient in the system using that number.<br />
                        (By default it show all active patients in all clinics.)<br />
                        <br />
                        <table>
                            <tr>

                                <td>Status:</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="FormField">
                                        <asp:ListItem Selected="True" Value="Active" Text="Active" />
                                        <asp:ListItem Value="InActive" Text="InActive" />

                                    </asp:DropDownList></td>
                                <td>Clinic:</td>
                                <td>
                                    <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormField">
                              
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>

                                <td class="regText">First Name:</td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="FormField" MaxLength="50" />

                                </td>
                                <td class="regText">Middle Initial:</td>
                                <td>
                                    <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="FormField" MaxLength="50" />

                                </td>
                            </tr>

                            <tr>
                                <td class="regText">Last Name:</td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="FormField" MaxLength="50" />

                                </td>
                                <td class="regText">Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtHomePhone" runat="server" CssClass="FormField" MaxLength="50" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="98%">

                            <asp:Repeater ID="rptSearch" runat="server" OnItemDataBound="rptSearch_ItemDataBound">
                                <HeaderTemplate>
                                    <tr bgcolor="#D6B781">
                                        <td><strong>First Name</strong></td>
                                        <td><strong>Last Name</strong></td>
                                        <td><strong>Phone Number</strong></td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="regText" width="33%">
                                            <a href='manage.aspx?PatientID=<%# Eval("PatientID") %>'>
                                                <%# Eval("FirstName") %></a>
                                        </td>
                                        <td class="regText" width="33%">
                                            <a href='manage.aspx?PatientID=<%# Eval("PatientID") %>'>
                                                <%# Eval("LastName") %></a>
                                        </td>
                                        <td class="regText" width="33%">

                                            <%# Eval("HomePhone") %>
                                        </td>

                                    </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblEmptyData" Style="color: red; text-align: center;"
                                                    Text="No record found !" runat="server" Visible="false">
                                                </asp:Label></strong>
                                        </td>
                                    </tr>

                                </FooterTemplate>
                            </asp:Repeater>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Btn_Previous" CommandName="Previous"
                            runat="server" OnCommand="ChangePage" CssClass="button"
                            Text="Previous" />
                        <asp:Button ID="Btn_Next" runat="server" CommandName="Next"
                            OnCommand="ChangePage" Text="Next" CssClass="button" />

                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle"></asp:Label>&nbsp <span id="pagingtext" runat="server">of </span>&nbsp
                        <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>

                    </td>
                </tr>
            </table>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
