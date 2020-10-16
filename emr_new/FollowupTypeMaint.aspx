<%@ Page Title="Maintain Followup Types" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FollowupTypeMaint.aspx.cs" Inherits="FollowupTypeMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validateAdd() {
            var URL = '<%=Page.ResolveUrl("~/FollowupTypeMaint.aspx/CheckDuplicateRecords")%>';
            var tableName = "apt_FollowUp_types";
            var ID = "0";
            var Name = $(".newText").val();

            if (Name == "") {
                alert("Please enter the value in description field.");
                return false;
            }
            else {
                CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
                return checkCommonVar;
            }
        }
        function ValidateEdit() {
            var URL = '<%=Page.ResolveUrl("~/FollowupTypeMaint.aspx/CheckDuplicateRecords")%>';
            var tableName = "apt_FollowUp_types";
            var ID = $(".keyFollowId").val();
            var Name = $(".editText").val();

            if (Name == "") {
                alert("Please enter the value in description field.");
                return false;
            }
            else {
                CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
                return checkCommonVar;
            }
        }
        function CommonFunctionForCheckDuplicate(url, id, name, tableName) {
            var isFlag = false;

            $.ajax({
                type: "POST",
                url: url,
                async: false,
                data: "{ID:'" + id + "', Name:'" + name + "', tableName:'" + tableName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != '') {
                        alert("The value('" + name + "') that you entered already exists in the database, please choose another.");
                        isFlag = false;
                    }
                    else {
                        isFlag = true;
                    }
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });
            if (isFlag == false) {
                checkCommonVar = false;
                return false;
            }
            else {
                checkCommonVar = true;
                return true;
            }

        }
        function ConfirmDelete() {
            var message = confirm('Are you sure you want to delete this item?');
            if (message != 0) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="95%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td bgcolor="#D6B781" class="PageTitle">Maintain Followup Types
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView runat="server" ID="grdTypes" AutoGenerateColumns="false" DataKeyNames="FollowUp_Type_ID"
                    CellPadding="4" CellSpacing="4" GridLines="None" OnRowCommand="grdTypes_RowCommand"
                    OnRowCancelingEdit="grdTypes_RowCancelingEdit" OnRowEditing="grdTypes_RowEditing" OnRowUpdating="grdTypes_RowUpdating" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdTypes_PageIndexChanging"
                     ShowFooter="true" OnRowDeleting="grdTypes_RowDeleting"  >
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="button" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="button" OnClientClick="return ConfirmDelete();" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CssClass="button" OnClientClick="return ValidateEdit();" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="button" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnInster" runat="server" CommandName="Insert" Text="Add" CssClass="button" OnClientClick="return validateAdd();" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <table style="display: none">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="keyId" runat="server" Text='<%#Eval("FollowUp_Type_ID") %>' CssClass="keyFollowId" Width="50" />
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" >
                            <ItemTemplate>
                                <%# Eval("FollowUp_Type_Desc") %>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescrip" runat="server" Text='<%# Eval("FollowUp_Type_Desc") %>' CssClass="regText editText"  ClientIDMode="Static"/>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDescripNew" runat="server" Text='' CssClass="regText newText" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Viewable">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboViewable" runat="server" Checked='<%# Eval("Viewable_YN") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboViewable" runat="server" Checked='<%# Eval("Viewable_YN") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboViewableNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consult">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboConsult" runat="server" Checked='<%# Eval("ConsultType_YN") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboConsult" runat="server" Checked='<%# Eval("ConsultType_YN") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboConsultNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FollowUp">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboFollowUp" runat="server" Checked='<%# Eval("FollowUpType_YN") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboFollowUp" runat="server" Checked='<%# Eval("FollowUpType_YN") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboFollowUpNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Patient Ticket">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboTicket" runat="server" Checked='<%# Eval("TicketType_YN") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboTicket" runat="server" Checked='<%# Eval("TicketType_YN") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboTicketNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Ticket">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboTicketStaff" runat="server" Checked='<%# Eval("StaffTicketType_YN") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboTicketStaff" runat="server" Checked='<%# Eval("StaffTicketType_YN") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboTicketStaffNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appointment">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboAppointment" runat="server" Checked='<%# Eval("Appointment") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboAppointment" runat="server" Checked='<%# Eval("Appointment") %>'
                                    Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboAppointmentNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <%# Eval("DepartmentName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:label ID="lblDept" runat="server" text=' <%# Eval("DepartmentName")%>' visible="false"></asp:label>
                                <asp:DropDownList ID="ddlEditDept" runat="server" DataTextField="DepartmentName" DataValueField="DepartmentID"  />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlNewDept" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
