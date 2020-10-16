<%@ Page Language="C#" Title="Add ICD9 Code" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="admin_icd9_add.aspx.cs" Inherits="admin_icd9_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/admin_icd9_add.js" type="text/JavaScript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="400" border="0" cellpadding="5" class="borderText">
        <tr>
            <td><strong>ICD9 Code</strong></td>
            <td>
                <input name="icd9" type="text" class="FormField" id="txtICDCode" maxlength="50"></td>
        </tr>
        <tr>
            <td><strong>Diagnosis</strong></td>
            <td>
                <input name="diag" type="text" class="FormField" id="txtDiagnosis"  maxlength="100"></td>
        </tr>
        <tr>
            <td><strong>Viewable</strong></td>
            <td>
                <input type="checkbox" name="viewable_yn" id="viewable_yn"></td>
        </tr>
        <tr>
            <td colspan="2">
                <input name="button" type="button" class="button" id="button" value="Add" onclick="Validate('Add')">
                &nbsp;
                <input name="cancel" type="submit" class="button" id="cancel" onclick="MM_goToURL('parent', 'admin_icd9_list.aspx'); return document.MM_returnValue" value="Cancel"></td>
        </tr>
    </table>
</asp:Content>

