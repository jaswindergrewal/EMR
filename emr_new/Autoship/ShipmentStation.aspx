<%@ Page Title="Autoship Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ShipmentStation.aspx.cs" Inherits="_Manager" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Autoship Home</title>
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/Scrips.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OnBeforeDelete(record) {

            if (confirm("Are you sure you want to delete order " + record.OrderID + " (for " + record.ShipName + ") ?") == false) {
                return false;
            }

            return true;
        }

        function CheckforSipstationMsg() {
            alert("There is some error while sending connecting to Shipstation.");
        }
      
        function SetValuesForShipStatus(Flagvalue, CommandValue) {

            var Dropdown = $('#<%=ddlselectAssign.ClientID %>');
            //$('input[name="SelectedProductId"]').val(Dropdown.val());
            document.getElementById("SelectedProductId").value = Dropdown.val();
            var rbList = $('#<%=rdoShowonly.ClientID %> input:checked').val();
            // $('input[name="SelectedRadioList"]').val(rbList);
            document.getElementById("SelectedRadioList").value = rbList;
            document.getElementById("FlagForShipped").value = Flagvalue;
            document.getElementById("CommandText").value = CommandValue;

        }

       

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p class="PageTitle">
        Manage Autoship
    </p>
        <input type="hidden" id="FlagForShipped" runat="server" clientidmode="Static" />
    <input type="hidden" id="SelectedProductId" runat="server" clientidmode="Static" />
    <input type="hidden" id="SelectedRadioList" runat="server" clientidmode="Static" />
    <input type="hidden" id="CommandText" runat="server" clientidmode="Static" />
    <cc1:TabContainer ID="Autoship" runat="server" Width="95%" ActiveTabIndex="2"
        CssClass="lmc_tab">
        <cc1:TabPanel HeaderText="Generate Shipments" runat="server" ID="GenerateOrders"
 Visible="false"            CssClass="TabPanel">
            <ContentTemplate>
                <table width="100%">
                    <caption>
                        <h4>Generate Shipments</h4>
                    </caption>
                    <tr>
                        <td align="right">Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" Text='<%# DateTime.Today.ToString("MM/dd/yyyy") %>' />
                            <cc1:CalendarExtender ID="StartExt" runat="server" TargetControlID="txtDate" />
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDate"
                                Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                                EnableClientScript="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnGenOrders" runat="server" OnClick="btnGenOrders_OnClick" Text="Generate"
                                CssClass="button" />&nbsp;
							<asp:Button ID="btnPreviewOrders" runat="server" OnClick="btnPreviewOrders_Click"
                                Text="Preview" CssClass="button" />
                        </td>
                    </tr>
                </table>
                <table width="100%" id="OrderPreviewTable" runat="server" visible="false">
                    <tr>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="View Report" CssClass="button" OnClick="btnPrint_Click" />&nbsp;
							<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />&nbsp;
							<a id="Invoices" runat="server" href="" visible="false" target="_blank">View PDF</a>
                        </td>
                    </tr>
                    <tr>
                        <th>Shipment Preview
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="OrderViewGrid" runat="server"
                                AutoGenerateSelectButton="false" AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID"
                                OnSelectedIndexChanged="OrderViewGrid_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="button"
                                        SelectText="Select" />
                                    <asp:BoundField DataField="OrderID" HeaderText="Order #" />
                                    <asp:BoundField DataField="DatePrep" HeaderText="Date Prepared" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="ShipName" HeaderText="Name" />
                                    <asp:BoundField DataField="ShipAddress1" HeaderText="Address" />
                                    <asp:BoundField DataField="ShipCity" HeaderText="City" />
                                    <asp:BoundField DataField="ShipState" HeaderText="State" />
                                    <asp:BoundField DataField="ShipZip" HeaderText="Zip" />
                                    <asp:BoundField DataField="AutoShipNote" HeaderText="Note" />
                                    <asp:BoundField DataField="ShipDate" HeaderText="Due Date" DataFormatString="{0:MM/dd/yyyy}" />
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <th>Shipment Items
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="OrderItemsPreviewGrid" runat="server"
                                AutoGenerateSelectButton="false" AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" EmptyDataText="Select an order to view items.">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Extended Price">
                                        <ItemTemplate>
                                            <asp:Label ID="ExtendedPrice" runat="server" Text='<%# ((int)Eval("Quantity") * (decimal)Eval("Price")).ToString("C") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Manage Complete Orders" runat="server" ID="CreateInvoices"
            CssClass="TabPanel" Visible="false">
            <ContentTemplate>
                <table width="100%">
                    <caption>
                        <h4>Complete Orders</h4>
                    </caption>
                    <tr>
                        <td>Beginning
							<asp:TextBox ID="txtBeginClosed" runat="server" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBeginClosed"
                                Enabled="True" />
                            and Ending
							<asp:TextBox ID="txtEndClosed" runat="server" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndClosed"
                                Enabled="True" />
                            <asp:Button ID="btnDeleteOrders" runat="server" OnClick="btnDeleteOrders_Click" Text="Go"
                                CssClass="button" /><br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Start date must come before end date."
                                ControlToValidate="txtBeginClosed" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEndClosed"
                                Display="Dynamic" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="grdDeleteOrders" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID,DatePrep,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip"
                                EmptyDataText="No Open Orders." OnDeleteCommand="grdDeleteOrders_RowDeleting"
                                Visible="False" FolderStyle="grid_styles/Style_7" OnBeforeClientDelete="OnBeforeDelete">
                                <Columns>
                                    <obout:Column ID="Column2" HeaderText="Delete" AllowDelete="True" Width="60" Index="0">
                                        <ControlStyle CssClass="button" />
                                    </obout:Column>
                                    <obout:Column DataField="OrderID" HeaderText="Order #" InsertVisible="False" ReadOnly="True"
                                        SortExpression="OrderID" Width="60" Index="1" />
                                    <obout:Column DataField="DatePrep" HeaderText="Date Prep" DataFormatString="{0:MM/dd/yyyy}"
                                        ReadOnly="True" Width="100" DataFormatString_GroupHeader="{0:MM/dd/yyyy}" Index="2" />
                                    <obout:Column DataField="ShipName" HeaderText="Name" ReadOnly="True" Index="3" Width="125">
                                        <ItemStyle Wrap="False" />
                                    </obout:Column>
                                    <obout:Column DataField="ShipAddress1" HeaderText="Address" ReadOnly="True" Index="4">
                                        <ItemStyle Wrap="False" />
                                    </obout:Column>
                                    <obout:Column DataField="ShipCity" HeaderText="City" ReadOnly="True" Index="5" Width="100" />
                                    <obout:Column DataField="ShipState" HeaderText="State" ReadOnly="True" Index="6" Width="75" />
                                    <obout:Column DataField="ShipZip" HeaderText="Zip" ReadOnly="True" Index="7" Width="75" />
                                </Columns>
                                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Shipment Status" runat="server" ID="CloseOrders" CssClass="TabPanel">
            <ContentTemplate>
                <asp:UpdatePanel ID="CloseOrderPanel" runat="server">
                    <ContentTemplate>
                        <table width="1000px">
                            <caption>
                                <h4>Shipment Status</h4>
                            </caption>
                            <tr>
                                <td class="regText">
                                    <asp:RadioButtonList ID="rdoShowonly" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Text="Show only" Value="only" Selected="True" />
                                        <asp:ListItem Text="Show all Except" Value="all" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr><td>
                                Select Product<asp:DropDownList ID="ddlselectAssign" runat="server" DataTextField="ProductName"
                                DataValueField="ProductID" ClientIDMode="Static" AutoPostBack="false" style="width:400px;" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td>Beginning
									<asp:TextBox ID="txtBegin" runat="server" />
                                    <cc1:CalendarExtender ID="xx" runat="server" TargetControlID="txtBegin" />
                                    and Ending
									<asp:TextBox ID="txtEnd" runat="server" />
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEnd" />
                                    <asp:Button ID="btnCloseOrders" runat="server" OnClick="btnCloseOrders_Click" Text="Go"
                                        CssClass="button" OnClientClick="SetValuesForShipStatus('Open','Invoiced');"/><br />
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Start date must come before end date."
                                        ControlToValidate="txtBegin" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEnd"
                                        Display="Dynamic" ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                 <td>
                                    <asp:Button ID="BtnOpen" runat="server" Text="Open" CssClass="button" OnClick="BtnOpen_Click" OnClientClick="SetValuesForShipStatus('Open','Invoiced');"/>
                                    <asp:Button ID="BtnInvoice" runat="server" CssClass="button" Text="Invoiced" OnClick="BtnInvoice_Click" OnClientClick="SetValuesForShipStatus('Invoiced','Paid');" />
                                    <asp:Button ID="BtnPaid" runat="server" CssClass="button" Text="Paid" OnClick="BtnPaid_Click" OnClientClick="SetValuesForShipStatus('Paid','Ready');"/>
                                    <asp:Button ID="BtnReady" runat="server" CssClass="button" Text="Ready" OnClick="BtnReady_Click" OnClientClick="SetValuesForShipStatus('Ready','Shipped');"/>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="CloseOrdersGrid" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID,DatePrep,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip"
                                        OnSelectedIndexChanged="CloseOrdersGrid_SelectedIndexChanged" EmptyDataText="No Open Orders."
                                        OnRowCommand="CloseOrdersGrid_RowCommand" OnRowDeleting="CloseOrdersGrid_RowDeleting"
                                        OnRowUpdating="CloseOrdersGrid_RowUpdating" Visible="false" OnRowEditing="CloseOrdersGrid_RowEditing"
                                        OnRowCancelingEdit="CloseOrdersGrid_RowCancelingEdit" >
                                        <Columns>
                                            <asp:CommandField  SelectText="Complete" DeleteText="Edit" ShowDeleteButton="true"
                                                ShowSelectButton="true" EditText="Note" ShowEditButton="true" ButtonType="Button"
                                                ShowCancelButton="true" ControlStyle-CssClass="button" ItemStyle-Wrap="false" />


                                            <asp:TemplateField HeaderText="Select">
                                                
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" Width="70px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrderID" HeaderText="Order #" InsertVisible="False" ReadOnly="True"
                                                SortExpression="OrderID" />
                                            <asp:BoundField DataField="Date_Prepared" HeaderText="Date Prep" DataFormatString="{0:MM/dd/yyyy}"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="false" ReadOnly="true" />
                                            <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-Wrap="false" ReadOnly="true" />
                                            <asp:BoundField DataField="City" HeaderText="City" ReadOnly="true" />
                                            <asp:BoundField DataField="State" HeaderText="State" ReadOnly="true" />
                                            <asp:BoundField DataField="Zip" HeaderText="Zip" ReadOnly="true" />

                                            <asp:BoundField DataField="Note" HeaderText="Note">
                                                <ItemStyle Width="200px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                        <SelectedRowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <%-- <asp:SqlDataSource ID="OpenOrders" runat="server" SelectCommand="Orders_GetOpen"
                            ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="StoredProcedure" />--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Reports" Visible="false">
            <ContentTemplate>
                <asp:UpdatePanel ID="ReportsPanel" runat="server">
                    <ContentTemplate>
                        <asp:Menu ID="ReportsMenu" runat="server" OnMenuItemClick="ReportsMenu_MenuItemClick"
                            Orientation="Horizontal" BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana"
                            Font-Size="0.8em" ForeColor="#990000" StaticSubMenuIndent="10px">
                            <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <DynamicMenuStyle BackColor="#FFFBD6" />
                            <DynamicSelectedStyle BackColor="#FFCC66" />
                            <DynamicItemTemplate>
                                <%# Eval("Text") %>
                            </DynamicItemTemplate>
                            <Items>
                                <asp:MenuItem Text="Cancelled orders" Value="1" />
                                <asp:MenuItem Text="Open Orders" Value="2" />
                                <asp:MenuItem Text="Product Demand" Value="3" />
                            </Items>
                            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <StaticSelectedStyle BackColor="#FFCC66" />
                        </asp:Menu>
                        <br />
                        <div id="ProdReportVis" runat="server" visible="false">
                            Report demand as of:
							<asp:TextBox ID="txtProdDate" runat="server" Text='<%# DateTime.Today.AddDays(30).ToString("MM/dd/yyyy") %>' />
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtProdDate"
                                Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                                EnableClientScript="true" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtProdDate" />
                            <br />
                            <asp:Button ID="btnProdGen" runat="server" Text="Create Report" CssClass="button"
                                OnClick="btnProdGen_Click" />

                            <h4 id="ProductRreportHeader" runat="server"></h4>
                            <asp:GridView ID="GrdProductDemand" runat="server"
                                AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="ProductName"
                                OnRowDataBound="GrdProductDemand_RowDataBound">
                                <Columns>

                                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                    <asp:BoundField DataField="Day1" />
                                    <asp:BoundField DataField="Day2" />
                                    <asp:BoundField DataField="Day3" />
                                    <asp:BoundField DataField="Day4" />
                                    <asp:BoundField DataField="Day5" />
                                    <asp:BoundField DataField="Day6" />
                                    <asp:BoundField DataField="Day7" />

                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </div>
                        <div id="VisDiv" runat="server" visible="false">
                            <h4 id="ReportHeader" runat="server"></h4>
                            <asp:GridView ID="ReportsGrid" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84"
                                BorderStyle="None" BorderWidth="1px">
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>


                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Manage Products" runat="server" ID="TabPanel2" CssClass="TabPanel" Visible="false">
            <ContentTemplate>
                               <asp:UpdatePanel ID="ProductsPanel" runat="server">
                    <ContentTemplate>
                        <h4>Manage Products</h4>
                        <br />
                        <%--Width="1050"--%>
                        <obout:Grid ID="ProductsGrid" runat="server" ShowLoadingMessage="false" AllowPaging="true"
                            PageSize="10" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="true"
                            AllowPageSizeSelection="true" Serialize="false" CellPadding="0" Width="1050"
                            AllowFiltering="true" AllowRecordSelection="true" CellSpacing="0" ShowTotalNumberOfPages="false"
                            OnUpdateCommand="ProductsGrid_RowUpdating" OnInsertCommand="btnAddProduct_Click"
                            FolderStyle="../grid_styles/Style_7" DataSourceID="ProductsSource">
                            <Columns>
                                <obout:Column DataField="ProductID" HeaderText="Product ID" ReadOnly="True" SortExpression="ProductID" Width="80px" Align="center" HeaderAlign="center" />
                                <obout:Column DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" Align="Center" Width="155px" HeaderAlign="center" />
                                <obout:Column DataField="AutoshipPrice" HeaderText="Retail Price" SortExpression="AutoshipPrice"
                                    DataFormatString="{0:C}" Width="80" Align="Center" HeaderAlign="center" />
                                <obout:CheckBoxColumn DataField="Active" HeaderText="Available for Auto Ship" Width="150" Align="center" HeaderAlign="center" />
                                <obout:CheckBoxColumn DataField="Viewable" HeaderText="Viewable for Prescriptions" Width="150" Align="center" HeaderAlign="center" />
                                <obout:CheckBoxColumn DataField="Reviewed" HeaderText="Reviewed" Width="150" Align="center" HeaderAlign="center" />

                                <obout:Column DataField="Weight" HeaderText="Product Weight" SortExpression="ProductWeight" Width="100" Align="center" HeaderAlign="center" />
                                <obout:Column DataField="Length" HeaderText="Product Length" SortExpression="ProductLength" Width="100" Align="center" HeaderAlign="center" />
                                <obout:Column DataField="Width" HeaderText="Product Width" SortExpression="ProductWidth" Width="100" Align="center" HeaderAlign="center" />
                                <obout:Column DataField="Height" HeaderText="Product Height" SortExpression="ProductHeight" Width="100" Align="center" HeaderAlign="center" />
                                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" AllowDelete="false"
                                    runat="server" Align="Center" Width="120" HeaderAlign="center" />
                            </Columns>
                            <ClientSideEvents OnBeforeClientInsert="ValidateManageProduct" OnBeforeClientUpdate="ValidateManageProduct" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" />
                        </obout:Grid>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="ManageRights" HeaderText="Manage Access" runat="server" Visible="false">
            <ContentTemplate>
                <asp:UpdatePanel ID="ManageRightsPanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="ManageRightsGrid" runat="server" AutoGenerateEditButton="True"
                            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                            AutoGenerateColumns="false" DataKeyNames="EmployeeID,EmployeeName,username" DataSourceID="StaffSource"
                            AllowPaging="true" PageSize="15" CellPadding="2" CellSpacing="2" OnRowUpdating="ManageRightsGrid_RowUpdating"
                            OnRowEditing="ManageRightsGrid_RowEditing" OnDataBound="ManageRightsGrid_DataBound">
                            <Columns>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" ReadOnly="true" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="true" />
                                <asp:BoundField DataField="username" HeaderText="UserName" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Access Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccess" runat="server" Text='<%# Eval("AutoshipAccess") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddAccess" runat="server">
                                            <asp:ListItem Text="Manager" Value="Manager" />
                                            <asp:ListItem Text="Records" Value="Records" />
                                            <asp:ListItem Text="ProdManager" Value="ProdManager" />
                                            <asp:ListItem Text="FrontDesk" Value="FrontDesk" />
                                            <asp:ListItem Text="Blank" Value="Blank" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                        <asp:SqlDataSource runat="server" ID="StaffSource" SelectCommand="Select EmployeeID,EmployeeName,username,AutoshipAccess from staff order by EmployeeName"
                            UpdateCommand="Update Staff set AutoshipAccess=@AutoshipAccess where EmployeeID = @EmployeeID"
                            ConnectionString="<%$ ConnectionStrings:db %>" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
     <asp:SqlDataSource runat="server" ID="ProductsSource" SelectCommand="SELECT [ProductID], [ProductName], STR([AutoshipPrice],10,2)as AutoShipPrice,Active,Viewable,Reviewed,Weight,Length,Width,Height  FROM [AutoshipProducts] ORDER BY [ProductName]" ConnectionString="<%$ ConnectionStrings:db %>" UpdateCommand="UPDATE AutoshipProducts SET ProductName = @ProductName, AutoshipPrice = @AutoshipPrice, Active=@Active WHERE (ProductID = @ProductID)" />
    <asp:SqlDataSource runat="server" ID="DiscountSource" SelectCommand="Select DiscountID,DiscountName from Autoship_Discounts order by DiscountID"
        ConnectionString="<%$ ConnectionStrings:db %>" />
</asp:Content>






