<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="UploadFile.aspx.cs" Inherits="UploadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781">
			<td colspan="2">
				<p>
					<b>Upload New Patient Document or Scanned Record</b></p>
			</td>
		</tr>
		<tr>
			<td>
				File Name
			</td>
			<td>
				<asp:TextBox ID="txtFileName" runat="server" CssClass="FormField" size="50" MaxLength="50"/>
			</td>
		</tr>
		<tr>
			<td>
				Category
			</td>
			<td>
				<asp:DropDownList ID="ddCategory" CssClass="FormField" runat="server">
					<asp:ListItem Text="lab_reports">Lab Reports</asp:ListItem>
					<asp:ListItem Text="radiology">Radiology</asp:ListItem>
					<asp:ListItem Text="pcp_reports">PCP Reports</asp:ListItem>
					<asp:ListItem Text="specialist_reports">Specialist Reports</asp:ListItem>
					<asp:ListItem Text="lmc_consent_forms">LMC Consent Forms</asp:ListItem>
					<asp:ListItem Text="lmc_intake_forms">LMC Intake Forms</asp:ListItem>
					<asp:ListItem Text="ovu_forms">OVU Forms</asp:ListItem>
					<asp:ListItem Text="letters">Letters</asp:ListItem>
					<asp:ListItem Text="referrals">Referrals</asp:ListItem>
					<asp:ListItem Text="patient_corr">Patient Correspondence</asp:ListItem>
					<asp:ListItem Text="misc_reports" Selected="True">Misc Reports</asp:ListItem>
					<asp:ListItem Text="Aesthetic_Documents">Aesthetic Documents</asp:ListItem>
					<asp:ListItem Text="dexa">DEXA Scans</asp:ListItem>
					<asp:ListItem Text="cc_auth">Credit Card Auth</asp:ListItem>
					<asp:ListItem Text="SOC">SOC</asp:ListItem>
				</asp:DropDownList>
				<tr>
					<td>
						File To Upload
					</td>
					<td>
						<asp:FileUpload ID="FileUpload1" runat="server" />
						<asp:CustomValidator ID="valNoPound" runat="server" OnServerValidate="val_NoPound"
							ErrorMessage="<br/>The characters #, :, ?, and % are not allowwed in the upload name."
							ForeColor="Red" Display="Dynamic" />
					</td>
				</tr>
				<tr>
					<td>
						&nbsp;
					</td>
					<td>
						&nbsp;
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button ID="btnSubmit" runat="server" Text="Upload" CssClass="button" OnClick="btnSubmit_Click" />
						<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
					</td>
					<td>
					</td>
				</tr>
	</table>
</asp:Content>
