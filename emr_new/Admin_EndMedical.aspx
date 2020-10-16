<%@ Page Title="Mark Patients End Medical" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Admin_EndMedical.aspx.cs" Inherits="Admin_EndMedical" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="PageTitle">
            <td>
                <p>Record End Medical</p>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Clinic</strong><br />
                <asp:DropDownList ID="ddlClinic" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlClinic_SelectedIndexChanged">
                </asp:DropDownList>
             </td>
        </tr>
        <tr>
            <td>

                <asp:GridView ID="grdEndMedical" runat="server" AutoGenerateEditButton="false" BorderColor="#DEBA84"
                    BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="true" DataKeyNames="PatientID,FullName,ShippingStreet,City,State,Zip"
                    AllowPaging="true" PageSize="25" CellPadding="6"
                    CellSpacing="6" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="Bottom"
                    GridLines="None" OnRowDataBound="grdEndMedical_DataBound" HeaderStyle-CssClass="border"
                    OnSelectedIndexChanging="grdEndMedical_SelectedIndexChanging" OnPageIndexChanging="grdEndMedical_PageIndexChanging">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ControlStyle-CssClass="button" SelectText="End Medical"
                            ShowCancelButton="false" ShowDeleteButton="false" ShowEditButton="false" ShowInsertButton="false"
                            ShowSelectButton="true" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
    <cc1:ModalPopupExtender ID="modGetDate" BackgroundCssClass="ModalPopupBG" runat="server"
        CancelControlID="btnCancelEnd" TargetControlID="Dummy" PopupControlID="pnlGetDate"
        Y="200" />
    <asp:Panel ID="pnlGetDate" runat="server" CssClass="modalPopup" Width="600px">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div4">
                <div class="TitlebarLeft" id="Div5" runat="server">
                    Please select a date that the coverage ended.
                </div>
            </div>
            <div class="popup_Body">
                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" BackColor="Gray" />
                <cc1:CalendarExtender ID="calDate" runat="server" TargetControlID="txtDate" />
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="btnEnd" runat="server" Text="End Medical" CssClass="button" OnClick="btnEnd_Click" OnClientClick="return ConfirmDelete();" />
                <asp:Button ID="btnCancelEnd" runat="server" Text="Cancel" CssClass="button" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
