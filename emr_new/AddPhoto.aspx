<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/AddPhoto.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td colspan="2" class="PageTitle">Please choose the clinic where this photo is being taken:
            </td>
        </tr>
        <tr><td></td></tr>
        <tr><td></td></tr>
        <tr>

            <td class="PageTitle"> Clinic:</td>
            <td>
                <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormField clinic">
                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="PageTitle">File To Upload:<span class="Validation_StarMark_Color">*</span>
            </td>
            <td>
                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="FormField" />
                <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="ImageUpload"
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Upload Image" OnClick="btnSubmit_Click" OnClientClick=" return UploadImageValidate();"/></td>
        </tr>
    </table>

</asp:Content>
