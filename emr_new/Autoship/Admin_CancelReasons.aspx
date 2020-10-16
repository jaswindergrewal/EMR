<%@ Page Title="Maintain Auto Ship Cancel Reasons" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeFile="Admin_CancelReasons.aspx.cs" Inherits="Autoship_Admin_CancelReasons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        // function for validate the records in database during add the records.
        function validateAdd() {
            var URL = '<%=Page.ResolveUrl("~/Autoship/Admin_CancelReasons.aspx/CheckDuplicateRecords")%>';
            var tableName = "AutoshipCancelReasons";
            var ID = "0";
            var Name = $(".newText").val();

            if (Name == "") {
                alert("Please enter the value in reason field.");
                return false;
            }
            else {
                CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
                return checkCommonVar;
            }
        }

        // function for validate the records in database during update the records.
        function ValidateEdit() {
            var URL = '<%=Page.ResolveUrl("~/Autoship/Admin_CancelReasons.aspx/CheckDuplicateRecords")%>';
            var tableName = "AutoshipCancelReasons";
            var ID = $(".keyFollowId").val();
            var Name = $(".editText").val();

            if (Name == "") {
                alert("Please enter the value in reason field.");
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
    <div style="height: 20px;"></div>
    <table width="98%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td width="33%" bgcolor="#D6B781" class="PageTitle">Maintain Auto Ship Cancel Reasons
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView runat="server" ID="grdReasons" AutoGenerateColumns="false" DataKeyNames="ReasonID"
                    CellPadding="4" CellSpacing="4" GridLines="None" OnRowCommand="grdReasons_RowCommand"
                    OnRowCancelingEdit="grdReasons_RowCancelingEdit" OnRowEditing="grdReasons_RowEditing"
                    OnRowUpdating="grdReasons_RowUpdating" ShowFooter="true" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdReasons_PageIndexChanging" OnRowDeleting="grdReasons_RowDeleting">
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
                                            <asp:TextBox ID="keyId" runat="server" Text='<%#Eval("ReasonID") %>' CssClass="keyFollowId" Width="50" />
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <%# Eval("ReasonName") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("ReasonName") %>' CssClass="editText" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewName" runat="server" CssClass="newText" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboActive" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cboActive" runat="server" Checked='<%# Eval("Active") %>' Enabled="true" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cboActiveNew" runat="server" Checked='false' Enabled="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
