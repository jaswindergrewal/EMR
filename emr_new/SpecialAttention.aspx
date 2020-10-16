<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="SpecialAttention.aspx.cs" Inherits="SpecialAttention" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td width="30%">
                <b>Special Attention Notes </b>
            </td>
            <td width="70%" bgcolor="#D6B781">
                <div align="right">
                    [<a href="special_attn_flag.aspx?PatientID=<%= PatientID %>">Add</a>]
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width:1040px !important;overflow:auto;">
                <asp:GridView ID="rptAttnetion" runat="server" AutoGenerateColumns="false" ShowHeader="false" OnRowDeleting="rptAttnetion_RowDeleting" DataKeyNames="SpecialAttentionID">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table >
                                    <tr>
                                        <td><asp:LinkButton ID="lnkDelete" runat="server" Text="[Delete]" CommandName="Delete" OnClientClick="return ConfirmDelete();" /></td>
                                        <td  style="text-wrap:normal;">
                                            <%# ((DateTime)Eval("DateEntered")).ToShortDateString() %> &nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;
                                            
                                            <%# Eval("FlagNotes").ToString()%>&nbsp;&nbsp;-By:<%# Eval("EmployeeName").ToString()%>
                                        </td>
                                    </tr>

                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </div>
            </td>
        </tr>
    </table>
      
</asp:Content>
