<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_icd9_update.aspx.cs" Inherits="admin_icd9_update" %>


<asp:content id="Content1" contentplaceholderid="HeadContent" runat="Server">
     <script src="Scripts/admin_icd9_add.js" type="text/JavaScript"></script>
</asp:content>

<asp:content id="Content2" contentplaceholderid="MainContent" runat="Server">
    <table width="400" border="0" cellpadding="5" class="borderText">
        <tr>
            <td><strong>ICD9 Code</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtICDCode"  ClientIDMode="Static"></asp:TextBox></td>
                
        </tr>
        <tr>
            <td><strong>Diagnosis</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="FormField" ID="txtDiagnosis"  ClientIDMode="Static"></asp:TextBox>
               </td>
        </tr>
        <tr>
            <td><strong>Viewable</strong></td>
            <td>
                
                <asp:CheckBox ID="viewable_yn" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input name="button" type="button" class="button" id="button" value="Update" onclick="Validate('Edit')" />
                &nbsp;
                <input name="cancel" type="submit" class="button" id="cancel" onclick="MM_goToURL('parent', 'admin_icd9_list.aspx'); return document.MM_returnValue" value="Cancel"></td>
        </tr>
    </table>
    <asp:HiddenField id='hdnDiagnosisID' runat="server" ClientIDMode="Static" />
</asp:content>
