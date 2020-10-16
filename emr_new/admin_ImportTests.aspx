<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_ImportTests.aspx.cs" Inherits="admin_ImportTests" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="centered" >
    <table width="90%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
				<td colspan="2">
					<b>Import test to the EMR  </b>
				</td>
				
			</tr>
        <tr>
            <td>Start Date</td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# DateTime.Today.ToString("MM/dd/yyyy") %>' CssClass="FormFieldWhite" />
                <cc1:CalendarExtender ID="StartExt" runat="server" TargetControlID="txtStartDate" />
               
                &nbsp;&nbsp;
                <asp:Button ID="btnShowRecord" runat="server" CssClass="button" Text="Go"
                    OnClick="btnShowRecord_Click" />&nbsp;&nbsp;
                
                 <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtStartDate"
                    Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                    EnableClientScript="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="LabelMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label><br />
                The Data will be show for 6 month from the start date.
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnImport" runat="server" CssClass="button" Text="Import all checked"
                    OnClick="btnImport_Click" /></td>

        </tr>
        <tr>
            <td colspan="2">
                <obout:Grid ID="grdTests" runat="server" AllowAddingRecords="false" AllowFiltering="true" Width="100%"
                    AllowPageSizeSelection="true" AllowPaging="true" PageSize="100" AllowSorting="true"
                    AutoGenerateColumns="false" CallbackMode="false" FolderStyle="grid_styles/Style_7">
                    <Columns>
                        <obout:CheckBoxSelectColumn />
                        <obout:Column DataField="CleanName" HeaderText="Test Name" Width="65%" />
                        <obout:Column DataField="LastUsed" HeaderText="Last Used" Width="35%" />
                        <obout:Column DataField="TestName" Visible="false" />
                    </Columns>
                </obout:Grid>
            </td>
        </tr>

    </table>

</div>





</asp:Content>
