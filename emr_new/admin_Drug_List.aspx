<%@ Page Title="Edit Drug List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_Drug_List.aspx.cs" Inherits="admin_Drug_List" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td class="PageTitle" align="center">Edit Drug List
            </td>
        </tr>
        <tr>
            <td class="PageTitle" align="center">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divMain" style="background-color: Transparent; padding-left: 2px; padding-right: 2px; width: 98%">

        <asp:CheckBox ID="chkReviewed" runat="server" Text="Show only items needing review"
            CssClass="regText" AutoPostBack="true" />
        <obout:Grid ID="Grid1" runat="server" ShowLoadingMessage="false" AllowPaging="true" Serialize="false" DataSourceID="odsDrugList" EmbedFilterInSortExpression="true" EnableRecordHover="true"
            PageSize="25" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="false"
            AllowPageSizeSelection="true" CellPadding="0" CellSpacing="0" OnDeleteCommand="DeleteDrug"
            CallbackMode="true" AllowGrouping="false" AllowColumnResizing="true" AllowFiltering="true" Width="73%"
            OnUpdateCommand="UpdateRecord" FolderStyle="grid_styles/Style_7" Style="padding-left: 2px; padding-right: 2px;">
            <Columns>
                <obout:Column DataField="DrugID" Visible="false" />
                <obout:Column DataField="DrugName" SortExpression="DrugName" HeaderText="Drug Name" ItemStyle-Wrap="true" Width="50%" Wrap="true">
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                        <obout:FilterOption IsDefault="false" Type="StartsWith" />
                        <obout:FilterOption IsDefault="false" Type="EndsWith" />
                    </FilterOptions>
                </obout:Column>
                <obout:CheckBoxColumn DataField="Viewable_yn" HeaderText="Viewable" SortExpression="Viewable_yn"
                    ShowFilterCriterias="false" AllowFilter="false" Width="8%" />
                <obout:Column DataField="Gender" HeaderText="Gender" ShowFilterCriterias="false" AllowFilter="false" SortExpression="Gender" Width="8%" EditTemplateId="tplEditGender">
                </obout:Column>


                <obout:CheckBoxColumn DataField="Supplement_yn" HeaderText="Supplement" SortExpression="Supplement_yn"
                    ShowFilterCriterias="false" AllowFilter="false" Visible="false" />
                <obout:CheckBoxColumn DataField="Reviewed" HeaderText="Reviewed" Width="8%" ShowFilterCriterias="false" SortExpression="Reviewed"
                    AllowFilter="false" runat="server">
                </obout:CheckBoxColumn>
                <obout:Column DataField="ProductID" HeaderText="Auto Ship Product"
                    ShowFilterCriterias="false" AllowFilter="false" Visible="false" Width="8%">
                </obout:Column>

                <obout:Column ID="Column1" HeaderText="EDIT" AllowEdit="true" AllowDelete="true"
                    runat="server" />
            </Columns>

            <Templates>
                <obout:GridTemplate runat="server" ID="tplEditGender"
                    ControlID="ddlGender" ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList runat="server" ID="ddlGender" AppendDataBoundItems="true" Width="55px" CssClass="FormField">
                            <asp:ListItem Text="M" Value="M"></asp:ListItem>
                            <asp:ListItem Text="F" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </Template>
                </obout:GridTemplate>

            </Templates>

            <PagingSettings ShowRecordsCount="false" />

            <ClientSideEvents OnBeforeClientInsert="ValidateDrugs" OnBeforeClientUpdate="ValidateDrugs" OnClientUpdate="MessageAfterUpdateRecords" OnBeforeClientDelete="ConfirmDelete" />

        </obout:Grid>

        <asp:ObjectDataSource runat="server" ID="odsDrugList" SelectMethod="SelectAllDrugList" SortParameterName="sortExpression" SelectCountMethod="SelectAllDrugListCount" TypeName="Emrdev.ServiceLayer.AdminDrugListService" EnablePaging="true">
            <SelectParameters>
                <asp:Parameter Name="sortExpression" Type="String" />
                <asp:ControlParameter Name="reviewed" ControlID="chkReviewed" PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
