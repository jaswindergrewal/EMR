<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="upload_file.aspx.cs" Inherits="upload_file" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td colspan="2">
                <p>
                    <b>Upload New Patient Document or Scanned Record</b>
                </p>
            </td>
        </tr>
        <tr>
            <td>File Name<span class="Validation_StarMark_Color">*</span>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtFilename" Columns="50" MaxLength="50" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a file name."
                    SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFilename" />
            </td>
        </tr>
        <tr>
            <td>Category
            </td>
            <td>
                <asp:CheckBoxList ID="chkTag" runat="Server" DataTextField="Name" DataValueField="Id"  RepeatColumns="4" RepeatLayout="Flow"></asp:CheckBoxList>
			
               <%-- <asp:DropDownList CssClass="FormField" ID="ddlCategory" runat="server">
                    <asp:ListItem Value="lab_reports" Text="Lab Reports" />
                    <asp:ListItem Value="radiology" Text="Radiology" />
                    <asp:ListItem Value="pcp_reports" Text="PCP Reports" />
                    <asp:ListItem Value="specialist_reports" Text="Specialist Reports" />
                    <asp:ListItem Value="lmc_consent_forms" Text="LMC Consent Forms" />
                    <asp:ListItem Value="lmc_intake_forms" Text="LMC Intake Forms" />
                    <asp:ListItem Value="ovu_forms" Text="OVU Forms" />
                    <asp:ListItem Value="letters" Text="Letters" />
                    <asp:ListItem Value="referrals" Text="Referrals" />
                    <asp:ListItem Value="patient_corr" Text="Patient Correspondence" />
                    <asp:ListItem Value="misc_reports" Selected="true" Text="Misc Reports" />
                    <asp:ListItem Value="Aesthetic_Documents" Text="Aesthetic Documents" />
                    <asp:ListItem Value="dexa" Text="DEXA Scans" />
                    <asp:ListItem Value="cc_auth" Text="Credit Card Auth" />
                    <asp:ListItem Value="SOC" Text="SOC" />
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td>File To Upload<span class="Validation_StarMark_Color">*</span>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FormField"  ClientIDMode="Static"/>
                <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload1"
                    SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please select a file."></asp:RequiredFieldValidator>
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
                <asp:HiddenField ID="hdnfilepath" runat="server" ClientIDMode="Static"/>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Upload" OnClick="btnSubmit_Click" OnClientClick="document.getElementById('hdnfilepath').value=document.getElementById('FileUpload1').value"/>
        </tr>
    </table>
    <p>
        <b></b>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>
