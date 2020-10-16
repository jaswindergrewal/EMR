<%@ Page Title="admin_lab_patientmatch_edit" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="admin_lab_patientmatch_edit.aspx.cs" Inherits="admin_lab_patientmatch_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td colspan="2">
                <b>Assign the Patient to the EMR System </b>
            </td>

        </tr>
        
        <tr>
            <td width="100px"><strong>Name</strong>:</td>
            <td>
                <asp:Label ID="lblPatientNameFirstName" runat="server"></asp:Label>
                &nbsp;
            <asp:Label ID="lblPatientNameLastName" runat="server"></asp:Label></td>
        </tr>
       
        <tr>
            <td><strong>Birthdate</strong>:</td>
            <td>
                <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><strong>Sex</strong>:</td>
            <td>
                <asp:Label ID="lblSex" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><strong>EMR Patient ID</strong>:</td>
            <td>
                <asp:DropDownList ID="ddlPatientList" runat="server" DataTextField="PatientFullNameWithInActiveStatus" DataValueField="PatientID" CssClass="width150px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" PostBackUrl="~/admin_lab_patientmatch.aspx" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="hfID" runat="server"></asp:HiddenField>





</asp:Content>

