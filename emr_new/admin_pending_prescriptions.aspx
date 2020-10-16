<%@ Page Title="Pending Prescription Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_pending_prescriptions.aspx.cs" Inherits="admin_pending_prescriptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">
        Pending Prescription Requests
    </p>
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">

        <tr>
            <td>
                <asp:GridView AllowPaging="true" PageSize="20" OnPageIndexChanging="rptPendingScrips_PageIndexChanging" ID="rptPendingScrips" CellPadding="5" runat="server" AutoGenerateColumns="false" Width="95%" HeaderStyle-BackColor="#D6B781" Style="margin-left: 10px;margin-right:10px">
                    <Columns>
                        <asp:TemplateField HeaderText="View/Approve" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="admin_pending_pre_fil.aspx?pre_id=<%# Eval("PrescriptionID")%>">View/Approve</a>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date Entered " DataField="DateEntered" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Patient Name" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="PresrcriptionList.aspx?patientid=<%# Eval("patientid")%>">
                                    <%# Eval("LastName")%>,
							<%# Eval("Firstname")%></a>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Clinic" DataField="clinic" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Drug" DataField="DrugName" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" />
                    </Columns>

                </asp:GridView>

            </td>
        </tr>

    </table>
    <p class="PageTitle">
        Pending Supplement Requests
    </p>
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr>
            <td>
                <asp:GridView ID="rptPendingSupps" AllowPaging="true" PageSize="20" OnPageIndexChanging="rptPendingSupps_PageIndexChanging" CellPadding="3" runat="server" EmptyDataText="No Data For Pending Suppliments" AutoGenerateColumns="false" Width="95%" HeaderStyle-BackColor="#D6B781" Style="margin-left: 10px;margin-right:10px">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="View/Approve" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="admin_pending_supp_fil.aspx?pre_id=<%# Eval("PrescriptionID")%>">View/Approve</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date Entered " DataField="DateEntered" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Patient Name" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="PresrcriptionList.aspx?patientid=<%# Eval("patientid")%>">
                                    <%# Eval("LastName")%>,
							<%# Eval("Firstname")%></a>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Clinic" DataField="clinic" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Drug" DataField="DrugName" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" />
                    </Columns>

                </asp:GridView>

            </td>
        </tr>
    </table>
    <p>
        <input name="Button" type="button" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue"
            value="Admin Page">
    </p>
    <p>
        &nbsp;
    </p>
    <!-- InstanceEndEditable -->

    <!-- InstanceEnd -->

</asp:Content>
