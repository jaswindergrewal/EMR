<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="ProblemList.aspx.cs" Inherits="ProblemList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        function ConfirmDelete() {
            var message = confirm('Are you sure you want to delete this item?');
            if (message != 0) {
                return true;
            }
            else {
                return false;
            }
        }
        function DuplicateRecord() {
            alert("Duplicate record can't be inserted");
        }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
        <tr>
            <td colspan="9">
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                    class="border">
                    <tr bgcolor="#D6B781">
                        <td width="300">
                            <strong>Diagnoses</strong>
                        </td>
                        <td>
                            <strong>Date Entered </strong>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Priority</strong>
                            </div>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Sev</strong>
                            </div>
                        </td>
                        <td>
                            <p>
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptProblems" runat="server" OnItemCommand="rptProblems_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td width="300">
                                    <%# Eval("LineColor") %>
                                    <a href="problem_diag_edit.aspx?ProbDiagID=<%# Eval("ProbDiagID")%>&patientid=<%= PatientID %>">
                                        <%# Eval("Diag_Title") %>
										[<%# Eval("ICD9_Code") %>]</a>
                                    <%# (int)Eval("BeingAddressed_YN")==1 ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>

                                            [<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProbDiagID") %>'>Delete</asp:LinkButton>]
                                  
									</font>
                                </td>
                                <td nowrap="nowrap">
                                    <%# Eval("LineColor") %>
                                    <%# Eval("DateEntered") %>
                                    <%# (int)Eval("Active_YN")==0 ?" - "+ Eval("inactive_date") : "" %>
									</font>
                                </td>
                                <td>
                                    <%# Eval("LineColor") %>
                                    <%# Eval("Priority_num") %></font></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# Eval("LineColor") %>
                                        <%# Eval("Severity_num") %></font>
                                    </div>
                                </td>
                                <td nowrap="nowrap">
                                    <div align="right">
                                        <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (int)Eval("Active_YN")==1?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                        <asp:ImageButton ID="imgActive1" runat="server" Visible='<%#  (int)Eval("Active_YN")==0?true:false%>' ImageUrl="~/images/reactivate.png" AlternateText="Reactivate this issue" CommandName="Active" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                        <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  (int)Eval("BeingAddressed_YN")==0 && (int)Eval("Active_YN") == 1?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("ProbDiagID")%>' />


                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="5" nowrap="nowrap" bgcolor="#D6B781">
                            <asp:DropDownList ID="ddlDiagnoses" runat="server" />
                            &nbsp;&nbsp;Priority
							<asp:DropDownList CssClass="FormField" ID="Priority" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;Severity
							<asp:DropDownList CssClass="FormField" ID="Severity" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnProblems" runat="server" CssClass="button" Text="+ Diagnosis"
                                OnClick="btnProblems_Click" />
                        </td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                    class="border">
                    <tr bgcolor="#D6B781">
                        <td width="300">
                            <strong>Symptoms</strong>
                        </td>
                        <td>
                            <strong>Date Entered </strong>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Priority</strong>
                            </div>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Sev</strong>
                            </div>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Trend</strong>
                            </div>
                        </td>
                        <td>
                            <p>
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptSymptoms" runat="server" OnItemCommand="rptSymptoms_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td width="300" nowrap="nowrap">
                                    <%#(int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                    <a href="problem_symp_edit_Short.aspx?probsymptid=<%# Eval("SymptomID") %>&patientid=<%# Eval("PatientID") %>&format=short">
                                        <%#Eval("SymptomName") %></a>
                                    <%# (int)Eval("BeingAddressed_YN")==1 ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>
									[<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SymptomID") %>'>Delete</asp:LinkButton>]
                                   </font>
                                </td>
                                <td nowrap="nowrap">
                                    <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                    <%# Eval("DateEntered") %>
                                    <%# (int)Eval("Active_YN")==0 ?" - "+ Eval("Inactive_Date") : "" %>
									</font>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                        <%# Eval("priority_num") %></font>
                                    </div>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                        <%# Eval("severity_num") %></font>
                                    </div>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                        <%# Eval("Dir") %></font>
                                    </div>
                                </td>
                                <td nowrap="nowrap">
                                    <p align="right">
                                        <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (int)Eval("Active_YN")==1?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("SymptomID")%>' />
                                        <asp:ImageButton ID="imgActive1" runat="server" Visible='<%#  (int)Eval("Active_YN")==0?true:false%>' ImageUrl="~/images/reactivate.png" AlternateText="Reactivate this issue" CommandName="Active" CommandArgument='<%# Eval("SymptomID")%>' />
                                        <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  (int)Eval("BeingAddressed_YN")==0 && (int)Eval("Active_YN") == 1?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("SymptomID")%>' />

                                    </p>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="6" nowrap="nowrap" bgcolor="#D6B781">
                            <asp:DropDownList ID="ddlSymptom" runat="server" />
                            &nbsp;&nbsp;Priority
							<asp:DropDownList name="Priority_sym" CssClass="FormField" ID="Priority_sym" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;Severity
							<asp:DropDownList name="Severity_sym" CssClass="FormField" ID="Severity_sym" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSympt" runat="server" CssClass="button" Text="+ Symptom" OnClick="btnSympt_Click" />
                        </td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                    class="border">
                    <tr bgcolor="#D6B781">
                        <td width="300">
                            <strong>Diagnoses handled by 3rd party </strong>
                        </td>
                        <td>
                            <strong>Date Entered </strong>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Priority</strong>
                            </div>
                        </td>
                        <td>
                            <div align="center">
                                <strong>Sev</strong>
                            </div>
                        </td>
                        <td>
                            <p>
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptMisc" runat="server" OnItemCommand="rptMisc_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td width="300" nowrap="nowrap">
                                    <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                    <a href='problem_miscdiag_edit.aspx?ProbDiagID=<%# Eval("ProbDiagID") %>&patientid=<%= PatientID %>'>
                                        <%# Eval("Diag_Title") %></a>
                                    <%# (bool)Eval("BeingAddressed_YN") ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>
									[<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProbDiagID") %>'>Delete</asp:LinkButton>]
                                           
									</font></font>
                                </td>
                                <td nowrap="nowrap">
                                    <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                    <%# Eval("DateEntered") %>
                                    <%# (bool)Eval("Active_YN") ==false? " - " + Eval("inactive_date") : "" %>
									</font></font>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                        <%# Eval("Priority_num") %></font></font>
                                    </div>
                                </td>
                                <td>
                                    <div align="center">
                                        <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                        <%# Eval("Severity_num") %></font></font>
                                    </div>
                                </td>
                                </td>
								<td nowrap="nowrap">
                                    <div align="right">
                                        <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (bool)Eval("Active_YN")?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("ProbDiagID")%>' />

                                        <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  !(bool)Eval("BeingAddressed_YN") && (bool)Eval("Active_YN") ?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="5" nowrap="nowrap" bgcolor="#D6B781">
                            <asp:DropDownList ID="ddlMisc" runat="server" />
                            &nbsp;Priority
							<asp:DropDownList name="Misc_prio" CssClass="FormField" ID="Misc_prio" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;Severity
							<asp:DropDownList name="MiscSev" CssClass="FormField" ID="MiscSev" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnMisc" runat="server" CssClass="button" Text="+ 3rd Party Diagnosis"
                                OnClick="btnMisc_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
