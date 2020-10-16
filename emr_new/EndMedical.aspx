<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="EndMedical.aspx.cs" Inherits="EndMedical" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="border">
        <tr>
            <td>Please select a date that the coverage ended and whether autoship is to be cancellled.
            </td>
        </tr>
        <tr>
            <td>
                Date : <asp:TextBox ID="txtDate" runat="server" CssClass="FormFieldWhite"/>
                <cc1:CalendarExtender ID="calDate" runat="server" TargetControlID="txtDate" />
                <asp:RequiredFieldValidator ID="reqDate" runat="server" ControlToValidate="txtDate"
                    ForeColor="Red" Display="Dynamic" ErrorMessage="You must enter a date to end the program" />
            </td>
        </tr>
        <tr>
            <td>
                Reason : <asp:TextBox ID="txtReason" runat="server" CssClass="FormFieldWhite" />
             
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cboAutoship" runat="server" Checked="false" Text="Cancel Auto Ship"
                    CssClass="regText" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnEnd" runat="server" Text="Continue" CssClass="button" OnClick="btnEnd_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
    <cc1:ModalPopupExtender ID="modWelcome" BackgroundCssClass="ModalPopupBG" runat="server"
        CancelControlID="btnCancel1" TargetControlID="Dummy" PopupControlID="pnlWelcome" />
    <asp:Panel ID="pnlWelcome" runat="server" CssClass="modalPopup" Style="position: absolute; left: 7px; top: 9px; width: 600px; height: 400px; z-index: 1">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div4">
                <div class="TitlebarLeft" id="Div5" runat="server">
                    Close Account Wiazrd
                </div>
            </div>
            <div class="popup_Body">
                <p style="font-size: large">
                    Welcome to the Close Accout Wizrd
                </p>
                <p style="font-size: medium">
                    This wizrd will giude you through the process of closing an account.
                </p>
                <p style="font-size: medium">
                    The first step is in Sharepoint.
                </p>
                <ul>
                    <li>Add the patient to the Dropped Patients list in Sharepoint. </li>
                </ul>
                <p style="font-size: large">
                    Press the Next button when you are done.
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="btnNext1" runat="server" Text="Next" CssClass="button" OnClick="btnNext1_Click" />
                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="button" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="Dummy1" runat="server" Style="visibility: hidden;" />
    <cc1:ModalPopupExtender ID="modQB" BackgroundCssClass="ModalPopupBG" runat="server"
        CancelControlID="btnCancel2" TargetControlID="Dummy1" PopupControlID="pnlQB" />
    <asp:Panel ID="pnlQB" runat="server" CssClass="modalPopup" Width="600px">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft" id="Div2" runat="server">
                    Close Account Wizard
                </div>
            </div>
            <div class="popup_Body">
                <p style="font-size: large">
                    The next step is in Quick books. Go to QuickBooks and make the following changes:
                </p>
                <ul>
                    <li>Add a NOTE in the CUSTOMER INFORMATION section: <i>date – pt dropped program – initials</i></li>
                    <li>If MMF needs to be removed, create a credit memo.</li>
                    <li>ADDITIONAL INFO > RENEWAL MONTH: set to ‘cxld’ </li>
                    <li>JOB INFO > JOB STATUS: ‘closed’</li>
                    <li>JOB INFO> END DATE: set date </li>
                    <li>
                        <asp:Label ID="lblQBActive" runat="server" Text="" /></li>
                </ul>
                <p style="font-size: large">
                    Press the Next button when you are done.
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="btnNext2" runat="server" Text="Next" CssClass="button" OnClick="btnNext2_Click" />
                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CssClass="button" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="Dummy2" runat="server" Style="visibility: hidden;" />
    <cc1:ModalPopupExtender ID="modEMR" BackgroundCssClass="ModalPopupBG" runat="server"
        CancelControlID="btnCancel3" TargetControlID="Dummy2" PopupControlID="pnlEMR" />
    <asp:Panel ID="pnlEMR" runat="server" CssClass="modalPopup" Width="600px">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div3">
                <div class="TitlebarLeft" id="Div6" runat="server">
                    Close Account Wizrd
                </div>
            </div>
            <div class="popup_Body">
                <p style="font-size: large">
                    The wizard will now make the following changes to EMR:
                </p>
                <ul>
                    <li>Enter a CONTACT RECORD – indicating customer has dropped with your initials
                    </li>
                    <li>Close all follow up flags </li>
                    <li>
                        <asp:Label ID="lblInactive" runat="server" /></li>
                    <li>Close any open appointments</li>
                </ul>
                <p style="font-size: large">
                    Press the Next button when you are ready.
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="btnNext3" runat="server" Text="Next" CssClass="button" OnClick="btnNext3_Click" />
                <asp:Button ID="btnCancel3" runat="server" Text="Cancel" CssClass="button" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="Dummy3" runat="server" Style="visibility: hidden;" />
    <cc1:ModalPopupExtender ID="modLast" BackgroundCssClass="ModalPopupBG" runat="server"
        CancelControlID="btnCancel4" TargetControlID="Dummy3" PopupControlID="pnlLast" />
    <asp:Panel ID="pnlLast" runat="server" CssClass="modalPopup" Width="600px">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="Div7">
                <div class="TitlebarLeft" id="Div8" runat="server">
                    Close Account Wizard
                </div>
            </div>
            <div class="popup_Body">
                <p style="font-size: large">
                    The final step is to make the following changes to the chart:
                </p>
                <ul>
                    <li>Pull chart </li>
                    <li>Put an x through the initial and date with a sharpie </li>
                    <li>File with inactive charts</li>
                </ul>
                <p style="font-size: large">
                    Press the Finish button when you are done.
                </p>
            </div>
            <div class="popup_Buttons">
                <asp:Button ID="btnNext4" runat="server" Text="Finish" CssClass="button" OnClick="btnNext4_Click" />
                <asp:Button ID="btnCancel4" runat="server" Text="Cancel" CssClass="button" Enabled="false" />
            </div>
        </div>
    </asp:Panel>
    <div>
        <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
     
    </div>
</asp:Content>
