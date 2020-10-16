<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AutoShipBundle.aspx.cs" Inherits="AutoShipBundle" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <asp:UpdatePanel ID="ManageRightsPanel" runat="server">
                    <ContentTemplate>
	<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="PageTitle">
			<td>
				Bundled Product Details
			</td>
		</tr>
        <tr>
            <td>Bundle Name: <asp:DropDownList ID="drpBundleList" runat="server" AutoPostBack="true" DataSourceID="BundleSource" DataTextField="ProductName" DataValueField="ProductID" OnSelectedIndexChanged="drpBundleList_SelectedIndexChanged"></asp:DropDownList> </td>
        </tr>
        <tr>
            <td>Product Name: <asp:DropDownList ID="drpProducts" runat="server" DataSourceID="ProductSource" DataTextField="ProductName" DataValueField="ProductID"></asp:DropDownList> </td>
        </tr>
        <tr>
            <td><asp:Button id="btnAddProduct" text="Add Products" runat="server" OnClick="btnAddProduct_Click"/></td>
        </tr>
		<tr>
			<td>
				<obout:Grid ID="grdBundleProduct" runat="server" AutoGenerateColumns="false" 
					 AllowPaging="false"  CellPadding="6" CellSpacing="6"
					CallbackMode="true" Serialize="true" AllowRecordSelection="false" AllowGrouping="false"
					AllowColumnResizing="false" AllowAddingRecords="false" AllowFiltering="true" Width="100%"
					FolderStyle="grid_styles/Style_7" >
					<Columns>

                                <obout:Column DataField="ProductID" HeaderText="Product ID" ReadOnly="True" SortExpression="ProductID" />
                                <obout:Column DataField="Sku" HeaderText="Sku" SortExpression="Sku" Width="100" HeaderAlign="center" />
                                <obout:Column DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                                <obout:Column DataField="AutoshipPrice" HeaderText="Retail Price" SortExpression="AutoshipPrice"
                                    DataFormatString="{0:C}" />
                               
                            </Columns>
					</obout:Grid>
			</td>
		</tr>
	</table>

    <asp:SqlDataSource runat="server" ID="BundleSource" SelectCommand="SELECT [ProductID], [ProductName] FROM [AutoshipProducts] WHERE Bundle=1 ORDER BY [ProductName]"
        ConnectionString="<%$ ConnectionStrings:db %>"  />

    <asp:SqlDataSource runat="server" ID="ProductSource" SelectCommand="SELECT [ProductID], [ProductName] FROM [AutoshipProducts] WHERE (Bundle IS NULL OR Bundle=0 ) and ProductName!=''  ORDER BY [ProductName]"
        ConnectionString="<%$ ConnectionStrings:db %>"  />
                        </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

