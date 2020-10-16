<%@ Page Title="Pending Blood Draws" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_pending_blooddraws.aspx.cs" Inherits="admin_pending_blooddraws" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.SuperForm" Assembly="obout_SuperForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="Scripts/admin_add_pandingfollowups.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">
        Pending Blood Draws<br />
        <strong>Clinic</strong>
        <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormFieldWhite" OnSelectedIndexChanged="ddlClinic_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
      
    </p>
    <div class="persist" style="background-color: Transparent; padding-left: 0px;">
        <obout:Grid ID="grdBlood" runat="server" ShowLoadingMessage="False" AllowPaging="true"
            PageSize="25" AllowSorting="true" AutoGenerateColumns="False" AllowAddingRecords="False"
            AllowPageSizeSelection="true" Serialize="true" Width="110px" CellPadding="0"
            CellSpacing="0" CallbackMode="true" CSSFolderImages="resources/custom-styles/persist" OnRebind="grdBlood_Rebind">
            <Columns>
                <obout:Column DataField="DateEntered" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                <obout:Column DataField="value" HeaderText="Range Start"  DataFormatString="{0:MM/dd/yyyy}"/>
                <obout:Column DataField="EmployeeName" HeaderText="Entered By" />
				<obout:Column DataField="Clinic"  HeaderText="Clinic" Width="130" />
                <obout:Column DataField="PatientName" HeaderText="PatientName" />
                <obout:Column DataField="FirstCall" Visible="true" HeaderText="First Call" Width="100" />
                <obout:Column DataField="FirstNotes" Visible="false" />
                <obout:Column DataField="SecondCall" Visible="true" HeaderText="Second Call" Width="110" />
                <obout:Column DataField="SecondNotes" Visible="false" />
                <obout:Column DataField="FinalCall" Visible="true" HeaderText="Final Call" Width="100" />
                <obout:Column DataField="FinalNotes" Visible="false" />
                <obout:Column DataField="LetterSent" Visible="true" HeaderText="Letter Sent" Width="120" />
                <obout:Column DataField="LetterNotes" Visible="false" />
                <obout:Column DataField="ID" Visible="false" />
                <obout:Column DataField="PatientID" Visible="false" />
            </Columns>
            <ClientSideEvents OnClientDblClick="onDoubleClick" OnBeforeClientEdit="grdBlood_ClientEdit"
                ExposeSender="True" />
            <CssSettings CSSFolderImages="resources/custom-styles/persist"></CssSettings>
            <TemplateSettings RowEditTemplateId="tplRowEdit" />
            <PagingSettings ShowRecordsCount="False" />
            <Templates>
                <obout:GridTemplate runat="server" ID="tplRowEdit" ControlID="" ControlPropertyName="">
                    <Template>
                        <table cellpadding="6" cellspacing="6">
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="cboFirstCall" runat="server" Text="First Call" Font-Bold="true"
                                        ClientIDMode="Static" /><br />
                                    <b>Notes:</b><br />
                                    <asp:TextBox ID="txtFirstNote" runat="server" TextMode="MultiLine" Rows="3" Columns="30"
                                        ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="cboSecondCall" runat="server" Text="Second Call" Font-Bold="true"
                                        ClientIDMode="Static" /><br />
                                    <b>Notes:</b><br />
                                    <asp:TextBox ID="txtSecondNote" runat="server" TextMode="MultiLine" Rows="3" Columns="30"
                                        ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="cboFinalCall" runat="server" Text="Final Call" Font-Bold="true"
                                        ClientIDMode="Static" /><br />
                                    <b>Notes:</b><br />
                                    <asp:TextBox ID="txtFinalNote" runat="server" TextMode="MultiLine" Rows="3" Columns="30"
                                        ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="cboFinalLetter" runat="server" Text="Letter sent" Font-Bold="true"
                                        ClientIDMode="Static" /><br />
                                    <b>Notes:</b><br />
                                    <asp:TextBox ID="txtFinalLetter" runat="server" TextMode="MultiLine" Rows="3" Columns="30"
                                        ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <obout:OboutButton ID="btnRecord" runat="server" Text="Submit" Width="85" UseSubmitBehavior="false"
                                        ClientIDMode="Static" OnClientClick="grdBlood_ClientClick(); return true;" />
									<asp:LinkButton runat="server" ID="lnkFup" ClientIDMode="Static" >Open Follow Up</asp:LinkButton>
										
                                </td>
                            </tr>
                        </table>
                    </Template>
                </obout:GridTemplate>
            </Templates>
        </obout:Grid>
    </div>
    <p>
        <input name="Submit" type="button" class="button" onclick="MM_goToURL('parent','admin_main.aspx');return document.MM_returnValue"
            value="Back to Admin Page" />
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>
