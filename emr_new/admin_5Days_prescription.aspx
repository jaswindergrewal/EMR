<%@ Page Language="C#" Title="Prescription List" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_5Days_prescription.aspx.cs" Inherits="admin_5Days_prescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script src="Scripts/admin_5Days_prescription.js" type="text/javascript"></script>
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="totalcount" runat="server" />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="StaffList">
    </table>
    <table id="tdButton" style="display: none;">
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
    <p>
        <br />
        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue" value="Back to Admin Page" />
    </p>
    <asp:HiddenField id='hdnPageSize' runat="server" ClientIDMode="Static" />
    
</asp:Content>

