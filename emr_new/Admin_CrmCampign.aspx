<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin_CrmCampign.aspx.cs" Inherits="Admin_CrmCampign" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script type="text/javascript" src="Scripts/Common.js"></script>
      <script type="text/javascript" src="Scripts/CampaignType.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="centered">
        <table width="90%" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td colspan="2">
                    <b>Campaign Type </b>
                </td>

            </tr>

            <tr>
                <td colspan="2">
                    <obout:Grid ID="grdCampaignType" runat="server" AllowAddingRecords="true" AllowFiltering="false" width="100%"
                        AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                        AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5"
                        OnUpdateCommand="grdCampaignType_UpdateInsert" OnInsertCommand="grdCampaignType_UpdateInsert"
                        OnRebind="grdCampaignType_Rebind" EnableTypeValidation="false" >
                        <Columns>

                            <obout:Column DataField="CampaignID" Visible="false" width="10%"/>
                            <obout:Column DataField="CampaignType" HeaderText="Campaign Type" Width="50%">
                                <TemplateSettings RowEditTemplateControlId="CampaignType"/>
                            </obout:Column>

                            <obout:CheckBoxColumn DataField="IsActive" HeaderText="Active" Width="10%" />
                            <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" runat="server" Width="20%" />

                        </Columns>


                        <ClientSideEvents OnBeforeClientUpdate="ValiDateCampaignType" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientInsert="ValiDateCampaignType" />
                    </obout:Grid>
                </td>
            </tr>

        </table>

    </div>

</asp:Content>

