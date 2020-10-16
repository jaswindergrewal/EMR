<%@ Page Title="Pending Medical Records" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_pending_medrecs.aspx.cs" Inherits="admin_pending_medrecs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">
        Pending Medical Records Requests
    </p>
    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td>
                <strong>Appointment Date </strong>
            </td>
            <td>
                <strong>Date Range for Follow Up </strong>
            </td>
            <td>
                <strong>Clinic</strong><br />

                <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormFieldWhite" OnSelectedIndexChanged="ddlClinic_SelectedIndexChanged"
                    AutoPostBack="true"></asp:DropDownList>
               <%-- <asp:DropDownList ID="ddlClinic" runat="server" OnSelectedIndexChanged="ddlClinic_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="All" Value="All" />
                    <asp:ListItem Text="Kirkland" Value="Kirkland" />
                    <asp:ListItem Text="Tacoma" Value="South" />
                    <asp:ListItem Text="Lynnwood" Value="Lynnwood" />
                </asp:DropDownList>--%>
            </td>
            <td>
                <strong>Follow Type</strong>
            </td>
            <td>
                <strong>Entered By </strong>
            </td>
            <td>
                <strong>Patient Name </strong>
            </td>
        </tr>
        <asp:Repeater ID="rptConsults" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="admin_contact_add_pendingfollowups.asp?followUp_id=<%# Eval("followup_id")%>&patientid=<%# Eval("patientid")%>">
                            <%# Eval("dateentered")%></a>
                    </td>
                    <td>
                        <a href="admin_contact_add_pendingfollowups.asp?followUp_id=<%# Eval("followup_id")%>&patientid=<%# Eval("patientid")%>">
                            [<%# Convert.ToDateTime(Eval("Range_Start")).ToString("MM/dd/yyyy")%>] - [<%# Eval("Range_End")==null?"":Convert.ToDateTime(Eval("Range_End")).ToString("MM/dd/yyyy")%>]

                        </a>
                    </td>
                    <td>
                        <%# Eval("clinic")%>
                    </td>
                    <td>
                        <%# Eval("followup_type_desc")%>
                    </td>
                    <td>
                        <%# Eval("EmployeeName")%>
                    </td>
                    <td>
                        <%# Eval("Lastname")%>,&nbsp;<%# Eval("Firstname")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <p>
        <input name="Submit" type="button" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue"
            value="Back to Admin Page">
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>
