<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="OneTime.aspx.cs" Inherits="Autoship_OneTime" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
      <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/OneTime.js"></script>
    <script src="../Scripts/Common.js"></script>
 
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlCreate" runat="server" CssClass="regText">
        <asp:Button ID="btnPlaceOrder" runat="server" CssClass="button" Text="Place Order"
            OnClick="btnPlaceOrder_Click" />
        &nbsp; <b>Ship Date:</b>
        <asp:TextBox ID="txtShipDate" runat="server" ClientIDMode="Static" />
        <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtShipDate"
            DatePickerImagePath="~/images/date_picker1.gif" />
        <br />
        <br />
        <asp:Label ID="lblPatientID" runat="server" ClientIDMode="Static" Style="display: none" />
        <obout:Grid ID="grdOneTime" runat="server" AllowAddingRecords="true" AllowFiltering="false"
            AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
            AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_7"
            EnableTypeValidation="false" OnRebind="grdOneTime_Rebind" OnInsertCommand="grdOneTime_InsertUpdate"
            OnUpdateCommand="grdOneTime_InsertUpdate" OnDeleteCommand="grdOneTime_Delete">
            <Columns>
                <obout:Column DataField="DiscountID" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlDiscount" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ProductID" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlProductName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ProductName" HeaderText="Product Name">
                </obout:Column>
                <obout:Column DataField="Quantity" HeaderText="Quantity">
                    <TemplateSettings RowEditTemplateControlId="txtQuantity" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:CheckBoxColumn DataField="Affiliate" HeaderText="Affiliate Item">
                    <TemplateSettings RowEditTemplateControlId="chkAffiliate" RowEditTemplateControlPropertyName="checked" />
                </obout:CheckBoxColumn>
                <obout:Column DataField="PatientID" Visible="false" />
                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" AllowDelete="true"
                    Width="125" runat="server" />
                <obout:Column DataField="OneTimeSaleID" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="lblOneTimeSaleID" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
            </Columns>
            <ClientSideEvents OnBeforeClientAdd="grdOneTime_BeforeClientAdd" OnClientInsert="grdOneTime_ClientAdd" OnBeforeClientDelete="ConfirmDelete" OnBeforeClientInsert="checkQuantityBlank" />
            <LocalizationSettings LoadingText="No products added" />
            <TemplateSettings RowEditTemplateId="tplRowEdit" />
            <Templates>
                <obout:GridTemplate runat="server" ID="tplRowEdit" ControlID="" ControlPropertyName="">
                    <Template>
                        <asp:Label ID="lblOneTimeSaleID" runat="server" ClientIDMode="Static" />
                        <table class="rowEditTable" cellpadding="6" cellspacing="6" width="100%">
                            <tr>
                                <td valign="top" style="width: 100px;" align="right">Product Name
                                </td>
                                <td valign="top">

                                    <asp:DropDownList ID="ddlProductName" runat="server" ClientIDMode="Static"
                                        TabIndex="13" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Quantity
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="2" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Discount
                                </td>
                                <td valign="top">

                                    <asp:DropDownList ID="ddlDiscount" runat="server" ClientIDMode="Static" TabIndex="13" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Affiliate
                                </td>
                                <td valign="top">
                                    <asp:CheckBox ID="chkAffiliate" runat="server" ClientIDMode="Static" />
                                </td>
                            </tr>
                        </table>
                    </Template>
                </obout:GridTemplate>
            </Templates>
        </obout:Grid>
    </asp:Panel>
    <asp:Panel ID="pnlComplete" runat="server" Visible="false" CssClass="PageTitle">
        Order has been added.
    </asp:Panel>

</asp:Content>
