<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MailChipCampaigns.aspx.cs" Inherits="MailChipCampaigns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="PageTitle">
			<td>
				MailChimp Campaign
			</td>
		</tr>
        <tr>
            <td><span style="color:red">Note: Please make sure the campaign is associated with list. In case campaign is not associated with list the data for patients will not get updated.</span></td>
        </tr>
        <tr>
            <td>Campaign Name: <asp:DropDownList ID="drpCampaignList" runat="server" DataTextField="MailChimpCampaignName" DataValueField="MailChimpCampaignId" Width="250px"></asp:DropDownList> </td>
        </tr>
       <tr>
            <td> <asp:Button ID="btnSaveCampaigns" runat="server" Text="Save" onclick="btnSave_Campaigns"/></td>
        </tr>
	
	</table>
   
</asp:Content>

