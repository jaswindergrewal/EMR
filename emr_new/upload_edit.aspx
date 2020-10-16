<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="upload_edit.aspx.cs" Inherits="upload_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td colspan="2">
                <p>
                    <b>Edit the Title or Category for an upload</b>
                </p>
            </td>
        </tr>
        <tr>
            <td>File Name<span class="Validation_StarMark_Color">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtFileName" runat="server" CssClass="FormField" size="50" MaxLength="50" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a file name."
                    SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFilename" />
            </td>
        </tr>
        <tr>
            <td>Category
            </td>
            <td><asp:CheckBoxList ID="chkTag" runat="Server" DataTextField="Name" DataValueField="Id"  RepeatColumns="4" RepeatLayout="Flow"></asp:CheckBoxList>
			
               <%-- <asp:DropDownList ID="ddCategory" CssClass="FormField" runat="server">
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
                </asp:DropDownList>--%>
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="button" OnClick="btnSubmit_Click" CausesValidation="true" />&nbsp;
				<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false" />
            </td>
            <td>
                <asp:HiddenField ID="hdnPatientId" runat="server" />
                <asp:HiddenField ID="hdnFileName" runat="server" />
            </td>
        </tr>
    </table>
    <div>
    </div>
</asp:Content>
