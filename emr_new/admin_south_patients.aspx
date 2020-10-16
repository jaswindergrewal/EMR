<%@ Page Language="C#" Title="South Clinic Patients" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_south_patients.aspx.cs" Inherits="admin_south_patients" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    
        <p class="PageTitle">List of South clinic patients</p>
        <p><font size="2"><a href="patient_add.aspx">Create New Patient Contact</a></font></p>

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <table width="98%" border="0" cellpadding="6" cellspacing="0" class="border">
               

                <tr>
                    <td align="left">
                        <asp:DataList ID="grdSouthPatients" runat="server" RepeatColumns="4" Width="96%">
                            <ItemTemplate>
                                <a href="Manage.aspx?PatientID=<%# Eval("PatientID")%>" class="regText"><%#Eval("LastName")%>, <%# Eval("FirstName")%></a>
                            </ItemTemplate>
                        </asp:DataList>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Btn_Previous" CommandName="Previous"
                            runat="server" OnCommand="ChangePage"
                            Text="Previous"  CssClass="button"/>
                        <asp:Button ID="Btn_Next" runat="server" CommandName="Next"
                            OnCommand="ChangePage" Text="Next" CssClass="button"/>

                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp of
                        <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>

                    </td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
