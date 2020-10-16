<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportList.aspx.cs" Inherits="ReportList"  %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/Common.js"></script>
    <script type="text/javascript" src="Scripts/ReportList.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

   
            <div class="centered">
                <table width="90%" border="0" cellpadding="6" cellspacing="0" class="border">
                    <tr bgcolor="#D6B781" class="regText">
                        <td colspan="2">
                            <b>Report List </b>
                        </td>

                    </tr>
                    <tr>
                        <td>
                             <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>Select Type:<asp:DropDownList AutoPostBack="true" ClientIDMode="Static" runat="server" ID="drpReportType" CssClass="FormField" OnSelectedIndexChanged="drpReportType_SelectedIndexChanged"></asp:DropDownList>
             </ContentTemplate>
    </asp:UpdatePanel> </td>       
            </tr>

                    <tr>
                        <td colspan="2">
                            <obout:Grid ID="grdReportList" runat="server" AllowAddingRecords="true" AllowFiltering="true" Width="100%" CallbackMode="false"
                                AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true" 
                                AutoGenerateColumns="false"  FolderStyle="grid_styles/Style_5" ShowFooter="true"
                                OnRebind="grdReportList_Rebind" EnableTypeValidation="false" OnInsertCommand="grdReportList_UpdateInsert"  OnUpdateCommand="grdReportList_UpdateInsert">
                                <Columns>

                                    <obout:Column DataField="Id" Visible="false" Width="5%" />
                                     <obout:Column DataField="ReportTypeId" Visible="false" Width="5%" />
                                    <obout:Column DataField="ReportName" HeaderText="Report Name" Width="50%">
                                        <TemplateSettings RowEditTemplateControlId="ReportName" />
                                    </obout:Column>

                                    <obout:CheckBoxColumn DataField="IsActive" HeaderText="Active" Width="10%" />
                                    <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" runat="server" Width="20%" />
                                     <obout:Column HeaderText="View" Width="5%" >
                                        <TemplateSettings TemplateId="templateView" />
                                    </obout:Column>

                                </Columns>
                                
                                <Templates>

                                    <obout:GridTemplate ID="templateView">
                                        <Template>
                                            <a href="#" onclick="ViewReport(<%# Container.DataItem["Id"].ToString()%>);">View</a>
                                        </Template>
                                    </obout:GridTemplate>
                                    
                                    

                                </Templates>
                                <LocalizationSettings NoRecordsText="No Record Found!" />

                                <ClientSideEvents OnClientCallbackError="onCallbackError" OnBeforeClientUpdate="ValiDateReportList" OnClientInsert="OnInsertCommand" OnClientUpdate="OnUpdateCommand" OnBeforeClientInsert="ValiDateReportList" />
                            </obout:Grid>
                        </td>
                    </tr>

                </table>

            </div>
       
</asp:Content>



