<%@ Page Title="Autoship Home" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
	CodeFile="ShortDefault.aspx.cs" Inherits="_ShortManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<title>Autoship Home</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<p class="PageTitle">
		Manage Autoship</p>
	<cc1:TabContainer ID="Autoship" runat="server" Width="1000px" ActiveTabIndex="0"
		CssClass="lmc_tab">
		<cc1:TabPanel HeaderText="Generate Shipments" runat="server" ID="GenerateOrders"
			CssClass="TabPanel">
			<ContentTemplate>
				<table width="100%">
					<caption>
						<h4>
							Generate Shipments</h4>
					</caption>
					<tr>
						<td align="right">
							Date:
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
						<th>
							Shipment Preview
						</th>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="OrderViewGrid" runat="server" DataSourceID="OrderPreviewSource"
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
									<asp:BoundField DataField="AutoshipDiscounts" HeaderText="Discounts" />
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
						<th>
							Shipment Items
						</th>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="OrderItemsPreviewGrid" runat="server" DataSourceID="OrderItemPreviewSource"
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
				<asp:SqlDataSource runat="server" ID="OrderPreviewSource" SelectCommand="Orders_GetBatch"
					ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="StoredProcedure">
				</asp:SqlDataSource>
				<asp:SqlDataSource runat="server" ID="OrderItemPreviewSource" SelectCommand="OrderItems_GetOrder"
					ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="StoredProcedure">
					<SelectParameters>
						<asp:Parameter Name="OrderID" DefaultValue="0" />
					</SelectParameters>
				</asp:SqlDataSource>
			</ContentTemplate>
		</cc1:TabPanel>
		<cc1:TabPanel HeaderText="Create QuickBooks Invoices" runat="server" ID="CreateInvoices"
			CssClass="TabPanel">
			<ContentTemplate>
				<table width="700px">
					<caption>
						<h4>
							Create Invoices</h4>
					</caption>
					<tr>
						<td align="right">
							Date shipments were generated:
						</td>
						<td>
							<asp:TextBox ID="txtCreateDate" runat="server" Text='<%# DateTime.Today.ToString("MM/dd/yyyy") %>' />
							<cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtCreateDate" />
							<asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtCreateDate"
								Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
								EnableClientScript="true" />
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center">
							<asp:Button ID="btnCreateInvoices" runat="server" OnClick="btnCreateInvoices_OnClick"
								Text="Create" CssClass="button" />&nbsp;
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
								<h4>
									Shipment Status</h4>
							</caption>
							<tr>
								<td>
									Beginning
									<asp:TextBox ID="txtBegin" runat="server" />
									<cc1:CalendarExtender ID="xx" runat="server" TargetControlID="txtBegin" />
									and Ending
									<asp:TextBox ID="txtEnd" runat="server" />
									<cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEnd" />
									<asp:Button ID="btnCloseOrders" runat="server" OnClick="btnCloseOrders_Click" Text="Go"
										CssClass="button" /><br />
									<asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Start date must come before end date."
										ControlToValidate="txtBegin" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEnd"
										Display="Dynamic" ForeColor="Red" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="CloseOrdersGrid" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
										BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID,Date Prepared,Name,Address,City,State,Zip"
										OnSelectedIndexChanged="CloseOrdersGrid_SelectedIndexChanged" EmptyDataText="No Open Orders."
										OnRowCommand="CloseOrdersGrid_RowCommand" OnRowDeleting="CloseOrdersGrid_RowDeleting"
										OnRowUpdating="CloseOrdersGrid_RowUpdating" Visible="false" OnRowEditing="CloseOrdersGrid_Editing"
										OnRowCancelingEdit="CloseOrdersGrid_RowCancelingEdit">
										<Columns>
											<asp:CommandField SelectText="Complete" DeleteText="Delete" ShowDeleteButton="true"
												ShowSelectButton="true" EditText="Edit Note" ShowEditButton="true" ButtonType="Button"
												ShowCancelButton="true" ControlStyle-CssClass="button" ItemStyle-Wrap="false" />
											<asp:BoundField DataField="OrderID" HeaderText="Order #" InsertVisible="False" ReadOnly="True"
												SortExpression="OrderID" />
											<asp:BoundField DataField="Date Prepared" HeaderText="Date Prep" DataFormatString="{0:MM/dd/yyyy}"
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
						<asp:SqlDataSource ID="OpenOrders" runat="server" SelectCommand="Orders_GetOpen"
							ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="StoredProcedure" />
					</ContentTemplate>
				</asp:UpdatePanel>
			</ContentTemplate>
		</cc1:TabPanel>
		<cc1:TabPanel ID="Reports" runat="server" HeaderText="Reports">
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
						</div>
						<div id="VisDiv" runat="server" visible="false">
							<h4 id="ReportHeader" runat="server">
							</h4>
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
		<cc1:TabPanel HeaderText="Manage Products" runat="server" ID="ManageProducts" CssClass="TabPanel">
			<ContentTemplate>
				<asp:UpdatePanel ID="ProductsPanel" runat="server">
					<ContentTemplate>
						<h4>
							Manage Products</h4>
						<br />
						<asp:GridView ID="ProductsGrid" runat="server" AutoGenerateEditButton="True" BackColor="#DEBA84"
							BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
							DataKeyNames="ProductID" DataSourceID="ProductsSource" PagerSettings-Mode="NumericFirstLast"
							AllowPaging="True" PageSize="15" CellPadding="2" CellSpacing="2" OnRowUpdating="ProductsGrid_RowUpdating">
							<Columns>
								<asp:BoundField DataField="ProductID" HeaderText="Product ID" ReadOnly="True" SortExpression="ProductID" />
								<asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
								<asp:BoundField DataField="AutoshipPrice" HeaderText="Retail Price" SortExpression="AutoshipPrice"
									DataFormatString="{0:C}" />
								<asp:CheckBoxField DataField="Active" HeaderText="Active" />
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
						<table>
							<caption>
								<h3>
									Add a Product</h3>
							</caption>
							<tr>
								<td align="right">
									<strong>Product Name</strong>
								</td>
								<td>
									<asp:TextBox ID="txtProductName" runat="server" />
								</td>
							</tr>
							<tr>
								<td align="right">
									<strong>Retail Price</strong>
								</td>
								<td>
									<asp:TextBox runat="server" ID="txtAutoShipPrice" />
									<asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtAutoShipPrice"
										Type="Currency" Operator="DataTypeCheck" ErrorMessage="Auto ship price not valid."
										EnableClientScript="true" />
								</td>
							</tr>
							<tr>
								<td align="right">
									<asp:Button runat="server" ID="btnAddProduct" OnClick="btnAddProduct_Click" Text="Add" />
								</td>
							</tr>
						</table>
					</ContentTemplate>
				</asp:UpdatePanel>
			</ContentTemplate>
		</cc1:TabPanel>
		<cc1:TabPanel ID="ManageRights" HeaderText="Manage Access" runat="server">
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
	<asp:SqlDataSource runat="server" ID="ProductsSource" SelectCommand="SELECT [ProductID], [ProductName], STR([AutoshipPrice],10,2)as AutoShipPrice,Active,Viewable,Reviewed FROM [AutoshipProducts] ORDER BY [ProductName]"
		ConnectionString="<%$ ConnectionStrings:db %>" UpdateCommand="UPDATE AutoshipProducts SET ProductName = @ProductName, AutoshipPrice = @AutoshipPrice, Active=@Active WHERE (ProductID = @ProductID)" />
	<asp:SqlDataSource runat="server" ID="DiscountSource" SelectCommand="Select DiscountID,DiscountName from Autoship_Discounts order by DiscountID"
		ConnectionString="<%$ ConnectionStrings:db %>" />
</asp:Content>
