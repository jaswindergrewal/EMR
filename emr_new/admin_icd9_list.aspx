<%@ Page Language="C#" Title="ICD9 Code" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="admin_icd9_list.aspx.cs" Inherits="admin_icd9_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/admin_icd9_add.js" type="text/JavaScript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="DiagnosisList">

        <tr>
            <td><strong>ICD9 Code</strong></td>
            <td><strong>Diagnosis Name</strong></td>
            <td><strong>Viewable</strong></td>

        </tr>
    </table>
    <table id="ProcessInfo">
        <tr>
            <td>
                <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button" />
                <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp <span id="pagingtext">of</span>
                <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>
            </td>
        </tr>

    </table>
    <asp:HiddenField ID='hdnPageSize' runat="server" ClientIDMode="Static" />
</asp:Content>
