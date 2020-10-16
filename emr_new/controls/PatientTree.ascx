<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientTree.ascx.cs" Inherits="Controls_PatientTree" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<script runat="server">

    protected void btnSaveHotNotes_Click(object sender, EventArgs e)
    {

    }
</script>

<div id="PatientName" style="width: 798px;">
	<table width="800px" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781">
			<td class="regText" nowrap="nowrap">
				<b>Patient Name:</b>
				<asp:Label ID="lblFirstName" runat="server" Text="" />&nbsp;<asp:Label ID="lblMI"
					runat="server" />&nbsp;<asp:Label ID="lblLastName" runat="server" /><strong> &nbsp;&nbsp;&nbsp;Age:</strong>
				<asp:Label ID="lblAge" runat="server" />
				&nbsp;&nbsp;&nbsp;<strong>Clinic:&nbsp;</strong><asp:Label ID="lblClinic" runat="server" />
				&nbsp;&nbsp;&nbsp;<strong>Sex</strong>:
				<asp:Label ID="lblSex" runat="server" />
				&nbsp;&nbsp;&nbsp;<strong>Status</strong>: <span class="style2">
					<asp:Label ID="lblStatus" runat="server" />
				</span>&nbsp;&nbsp;&nbsp;<strong>Nick Name</strong>: <span class="style2">
					<asp:Label ID="lblNickName" runat="server" />
					Renewal Month:
					<asp:Label ID="lblRenewalMonth" runat="server" /></span>
			</td>
		</tr>
	</table>
	<p>
		<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PnlContent"
			ExpandControlID="pnlTitle" CollapseControlID="pnlTitle" TextLabelID="Label1"
			CollapsedText="Click to Show Notes and Alerts" ExpandedText="Click to Hide Notes and Alerts"
			Collapsed="True" SuppressPostBack="true" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
			CollapsedImage="~/images/expand.jpg">
		</cc1:CollapsiblePanelExtender>
		<asp:Panel ID="pnlTitle" runat="server" CssClass="border">
			<asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0">
			</asp:Image>
			<asp:Label ID="Label1" runat="server" Text="Show Notes and Alerts" CssClass="regText">
			</asp:Label>
		</asp:Panel>
		<asp:Panel ID="pnlContent" runat="server" CssClass="borderPanel">
			<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="notesPanel"
				DynamicLayout="true">
				<ProgressTemplate>
					<img src="~/images/indicator.gif" alt="Loading" />
					<strong>Please Wait . . .</strong></ProgressTemplate>
			</asp:UpdateProgress>
			<flan:UpdateProgressOverlayExtender ID="UpdateProgressOverlayExtender1" runat="server"
				ControlToOverlayID="notesPanel" CssClass="updateProgress" TargetControlID="UpdateProgress1" />
			<asp:UpdatePanel ID="notesPanel" runat="server">
				<ContentTemplate>
					<table cellpadding="2" cellspacing="2">
						<tr>
							<td width="262px">
								<strong>Alert</strong><br />
								<asp:TextBox ID="txtDiscount" runat="server" TextMode="MultiLine" Width="262px" CssClass="regText" Rows="6" /><br />
								<asp:Button ID="btnEditDiscount" runat="server" CssClass="button" OnClick="btnEditDiscount_Click"
									Text="Edit" />&nbsp;
								<asp:Button ID="btnSaveDiscount" runat="server" Text="Save" OnClick="btnSaveDiscount_Click"
									CssClass="button" Enabled="false" />
							</td>
							<td width="262px">
								<strong>Notes</strong><br />
								<asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="262px" CssClass="regText" Rows="6"/><br />
								<asp:Button ID="btnEditNote" runat="server" CssClass="button" OnClick="btnEditNote_Click"
									Text="Edit" />&nbsp;
								<asp:Button ID="btnSaveNote" runat="server" Text="Save" OnClick="btnSaveNote_Click"
									CssClass="button" Enabled="false" />
							</td>
                           
							<td width="262px">
								<asp:Label ID="lblOrderPending" ForeColor="Red" runat="server" Text="Order Generated, awaiting fulfillment."
									Visible="false" Width="262px" />
								<asp:Label ID="lblInactive" ForeColor="Red" runat="server" Text="This patient is inactive!" Visible="false" />
							</td>
						</tr>
					</table>
				</ContentTemplate>
			</asp:UpdatePanel>
		</asp:Panel>
	</p>
</div>
