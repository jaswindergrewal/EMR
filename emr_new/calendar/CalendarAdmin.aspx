<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="CalendarAdmin.aspx.cs" Inherits="calendar_CalendarAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="width: 98%; margin-left: 5px; margin-right: 5px;">
        <p class="PageTitle">
            Admin Calendar
        </p>
        <asp:Label ID="Label1" runat="server" Text="Select the database to manage" />
        <asp:DropDownList runat="server" ID="ddTables" OnSelectedIndexChanged="ddTables_SelectIndexChanged"
            AutoPostBack="true" />
        <asp:GridView ID="Admin" runat="server" DataSourceID="ProvidersSource" AutoGenerateEditButton="True" Width="100%"
            AutoGenerateInsertButton="true" AllowPaging="false" AutoGenerateRows="false"
            AutoGenerateColumns="true" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
            BorderWidth="1px" Caption="Manage Providers" CellPadding="3" CellSpacing="2"
            Visible="false" OnRowEditing="Admin_RowEditing" OnRowCancelingEdit="Admin_RowCaneclling"
            OnRowUpdating="Admin_RowUpdating" OnSorting="OnSort" OnRowDataBound="Admin_RowDataBound">
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
        <asp:ObjectDataSource ID="ProvidersSource" runat="server" SelectMethod="dummy" TypeName="dummy"></asp:ObjectDataSource>
      <asp:GridView ID="ApptTypeGrid" runat="server" DataSourceID="AptTypeSource" AutoGenerateEditButton="True" Width="100%"
            AutoGenerateInsertButton="true" AllowPaging="false" AutoGenerateRows="false" DataKeyNames="id"
            AutoGenerateColumns="false" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" CellSpacing="2" Visible="false" OnRowEditing="ApptTypeGrid_RowEditing"
            OnRowCancelingEdit="Admin_RowCaneclling" OnRowUpdating="Admin_RowUpdating" OnSorting="OnSort" OnRowDataBound="ApptTypeGrid_RowDataBound"
            AllowSorting="true" Caption="Manage Appointment Types">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="false" />
                <asp:BoundField DataField="TypeName" HeaderText="TypeName" SortExpression="TypeName" />
                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:TemplateField HeaderText="MailChimp" >
                    <ItemTemplate>
                        <%# Eval("MailChimpCampaignName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpCampaignList" runat="server" DataTextField="MailChimpCampaignName" DataValueField="MailChimpCampaignId"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ConfirmationEmail" HeaderText="ConfirmationEmail" SortExpression="ConfirmationEmail" />
                <asp:TemplateField HeaderText="Confirmation Text" SortExpression="ConfirmationText">
                    <ItemTemplate>
                        <%# Eval("ConfirmationText") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtConfirmationText" runat="server" TextMode="MultiLine" Text='<%# Eval("ConfirmationText") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Lead Status" >
                    <ItemTemplate>
                        <%# Eval("StatusName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpStatusList" runat="server" DataTextField="StatusName" DataValueField="StatusId"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ResultStatusId" HeaderText="ResultStatusId" SortExpression="ResultStatusId" Visible="false" />
                <asp:BoundField DataField="WufooFormKey" HeaderText="Wufoo Key" SortExpression="WufooFormKey" />
                <asp:BoundField DataField="Attachment" HeaderText="Attachment" SortExpression="Attachment" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                <asp:BoundField DataField="EmailFromAddress" HeaderText="Email Address" SortExpression="EmailFromAddress" />
                <asp:BoundField DataField="EmailFromName" HeaderText="Email Name" SortExpression="EmailFromName" />
                <asp:BoundField DataField="OVU" HeaderText="OVU Entry" SortExpression="OVU" />
                <asp:BoundField DataField="MailChimpCampaignId" HeaderText="id" SortExpression="id" Visible="false" />
                <asp:BoundField DataField="ResultStatusId" HeaderText="ResultStatusId" SortExpression="ResultStatusId" Visible="false" />
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
       
        <asp:ObjectDataSource ID="AptTypeSource" runat="server" SelectMethod="getApptTypeListOnly"
            TypeName="Calendar.AppointmentTypes" UpdateMethod="AppointmentTypeUpdate" InsertMethod="AppointmentTypeInsert"></asp:ObjectDataSource>

         <asp:GridView ID="grdResult" runat="server" DataSourceID="ResultSource" AutoGenerateEditButton="True" Width="100%"
            AutoGenerateInsertButton="true" AllowPaging="false" AutoGenerateRows="false" DataKeyNames="ID"
            AutoGenerateColumns="false" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" CellSpacing="2" Visible="false" OnRowEditing="grdResult_RowEditing"
            OnRowCancelingEdit="Admin_RowCaneclling" OnRowUpdating="Admin_RowUpdating" OnSorting="OnSort" OnRowDataBound="grdResult_RowDataBound"
            AllowSorting="true" Caption="Manage Results">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
                <asp:BoundField DataField="ResultName" HeaderText="ResultName" SortExpression="ResultName" />
                <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="IsActionRequired" HeaderText="IsActionRequired" SortExpression="IsActionRequired" />
                
                <asp:TemplateField HeaderText="Patient Status" >
                    <ItemTemplate>
                        <%# Eval("StatusName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpStatusList" runat="server" DataTextField="StatusName" DataValueField="StatusId"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ResultStatusId" HeaderText="ResultStatusId" SortExpression="ResultStatusId" Visible="false" />
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
       
        <asp:ObjectDataSource ID="ResultSource" runat="server" SelectMethod="getResultListOnly"
            TypeName="Calendar.Results" UpdateMethod="ResultsUpdate" InsertMethod="ResultsInsert"></asp:ObjectDataSource>

         <table runat="server" id="prov" visible="false">
            <tr>
                <th colspan="2" id="AddHeader" runat="server" class="headerText"></th>
            </tr>
            <tr>
                <td>Name:<span style="color: red"> *</span>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ProviderName" Text="" CssClass="TextValue" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="AddButton" Text="Add" OnClick="AddButton_OnClick" OnClientClick="return ValidateDataForCalendarAdmin();" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
        <script src="../Scripts/Scrips.js" type="text/javascript"></script>

    </div>
</asp:Content>

