<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Managertest.aspx.cs" Inherits="Managertest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                ShowHeader="true"
                Width="500px"
                OnRowDataBound="GridView1_RowDataBound" GridLines="Horizontal" Style="text-align: justify;">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderStyle="None" HeaderText="ID" ItemStyle-Font-Bold="true" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PanelID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Panels" ItemStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>

                            <%# Eval("PanelName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Selected Photos">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td valign="middle">
                                        <asp:ImageButton ID="ImageButton1" Enabled="false" CommandName="selectedimg" ImageUrl='<%# "~/Thumbnail.ashx?fileId="+Eval("FileId") %>' CommandArgument='<%# Eval("FileId") %>' runat="server"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Groups">
                        <ItemTemplate>
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="true" PageSize="5" ShowHeader="true" Width="400px" AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound"  OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="PanelName">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" MaxLength="3" ValidationGroup="add" Width="35" onkeydown="return CheckKeyCode()" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-BorderStyle="None" ItemStyle-Font-Bold="true" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupID" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-BorderStyle="None" HeaderText="ID" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImageButton1" CommandName="SelectGrid" CommandArgument='<%# Eval("GroupID") %>' Text="+" runat="server"></asp:LinkButton>
                                             <asp:LinkButton ID="LinkButton3" CommandName="CloseGrid" CommandArgument='<%# Eval("GroupID") %>' Text="-" runat="server" Visible="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="300px">
                                        <ItemTemplate>
                                            <%# Eval("GroupName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Test">
                                        <ItemTemplate>
                                            <asp:GridView ID="GridView3" runat="server" ShowHeader="true" Width="300px" AutoGenerateColumns="false" Visible="false">
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="PanelName">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" MaxLength="3" ValidationGroup="add" Width="35" onkeydown="return CheckKeyCode()" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%# Eval("TestName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Price(1 Qty)">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" ForeColor="AliceBlue" Text='<%# Eval("Price") %>' Enabled="false" CommandName="lbtnprice" CommandArgument='<%# Eval("SizeId") %>' runat="server"></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TotalPrice">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Price(1 Qty)">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" ForeColor="AliceBlue" Text='<%# Eval("Price") %>' Enabled="false" CommandName="lbtnprice" CommandArgument='<%# Eval("SizeId") %>' runat="server"></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TotalPrice">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
