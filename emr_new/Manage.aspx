<%@ Page Title="Patient Details" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeFile="Manage.aspx.cs" Inherits="Manage" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="LMC" TagName="PatientInfo" Src="controls/PatientInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script src="Scripts/AddPhoto.js" type="text/javascript"></script>
    <script src="Scripts/Manage.js" type="text/javascript"></script>
    <link href="css/Customalert.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="HDPatientID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HDPatientIdByAptfollowUps" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HDStaffID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HDQBCount" runat="server" />
    <asp:HiddenField ID="HDSpecialAttention" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidAffiliate" runat="server" ClientIDMode="Static" />
 
    <table id="MainTable" class="border" width="1230px">
        <tr valign="top">
            <td width="200px" height="700px">
                <asp:Panel ID="pnlMenu" runat="server" Width="200px">
                    <div id="accordion" style="width: 200px; " >
                        <h3 style="margin-top: 5px;">Photo</h3>
                        <div class="DivBorder" id="ImageDiv">

                            <li class="liFont" style="list-style-type: none;">
                                <asp:Image ID="imgPhoto" runat="server" Width="198" Height="147" ImageUrl="" /></li>
                            <li class="liFont" style="flex-align: center; list-style-type: none;">
                                <input id="ImageButton" type="button" value="Take New Photo" class="button" style="width: 198px;" /></li>
                        </div>
                        <h3 style="margin-top: 5px;">Tickets</h3>
                        <div id="TicketsDiv" class="DivBorder">
                        </div>
                        <h3 style="margin-top: 5px;">Patient Info</h3>
                        <div class="DivBorder">
                            <ul class="ulClass">
                                <li class="liFont"><a id="lnkPersonalInfo" class="a_class margin_left-30">Personal Info</a></li>
                                <li class="liFont"><a id="lnkScanUpload" class="a_class margin_left-30">Scans/Uploads</a></li>
                                <li class="liFont"><a id="lnkSpecialAttention" class="a_class margin_left-30">Special Attention</a></li>
                                <li class="liFont"><a id="lnkAppointment" class="a_class margin_left-30">Appointments</a></li>
                                <li class="liFont"><a id="lnkStatusLog" class="a_class margin_left-30">Calender Status </a></li>
                                <li class="liFont"><a id="lnkPrintOvu" class="a_class margin_left-30">Print OVU Form</a></li>
                                <li class="liFont"><a id="lnkContactRecord" class="a_class margin_left-30">Contact Records</a></li>
                                <li class="liFont"><a id="lnkPrescrption" class="a_class margin_left-30">Prescriptions</a></li>
                                <li class="liFont"><a id="lnkLab" class="a_class margin_left-30">Labs</a></li>
                                <li class="liFont"><a id="lnkLabReport" class="a_class margin_left-30">Lab Report</a></li>
                                <li class="liFont"><a id="lnkShareFile" class="a_class margin_left-30">ShareFile</a></li>
                                <li class="liFont"><a id="lnkSummary" class="a_class margin_left-30">Summary Report</a></li>
                                <li class="liFont"><a id="lnkCriticalTask" class="a_class margin_left-30">Critical Tasks</a></li>
                                <li class="liFont"><a id="lnkProblemList" class="a_class margin_left-30">Problems List</a></li>
                                <li class="liFont"><a id="lnkFollowups" class="a_class margin_left-30">Follow ups</a></li>
                                <li class="liFont"><a id="lnkClinicVisits" class="a_class margin_left-30">Clinic Visits (old)</a></li>
                                <li class="liFont"><a id="lnkVitals" class="a_class margin_left-30">Vitals</a></li>
                                <li class="liFont"><a id="lnkOpenInvoice" class="a_class margin_left-30">Open Invoices</a></li>
                                <li class="liFont"><a id="lnkAllergies" class="a_class margin_left-30">Allergies</a></li>
                                <%--<li class="liFont"><a id="lnktest" class="a_class margin_left-30">test </a></li>--%>
                            </ul>
                        </div>
                        <h3 style="margin-top: 5px;">Autoship</h3>
                        <div class="DivBorder">
                            <ul class="ulClass">
                                <li class="liFont"><a id="lnkOrder" class="a_class margin_left-30">Orders</a></li>
                                <li class="liFont"><a id="lnkManagement" class="a_class margin_left-30">Management Page</a></li>
                                <li class="liFont"><a id="lnkDailyShipment" class="a_class margin_left-30">Daily Shipments</a></li>
                                <li class="liFont"><a id="lnkOpenOrders" class="a_class margin_left-30">Open Orders</a></li>
                            </ul>
                        </div>
                        <h3 style="margin-top: 5px;">Aesthetics</h3>
                        <div class="DivBorder">
                            <ul class="ulClass">
                                <li class="liFont"><a id="lnkAestheticNotes" class="a_class margin_left-30">Aesthetic Notes</a></li>
                                <li class="liFont"><a id="lnkAestheticFollowUp" class="a_class margin_left-30">Aesthetic Follow Up</a></li>
                            </ul>
                        </div>
                        <h3 style="margin-top: 5px;">Admin</h3>
                        <div class="DivBorder" style="margin-bottom: 0px;">
                            <ul class="ulClass">
                                <li class="liFont"><a id="lnkCloseAccount" class="a_class margin_left-30">Close Account</a></li>
                                <li class="liFont"><a id="lnkMatchQuickBook" class="a_class margin_left-30">Match QuickBooks</a></li>
                                <li class="liFont"><a id="lnkRenewalExecption" class="a_class margin_left-30">Renewal Exception</a></li>
                            </ul>
                        </div>
                    </div>
                </asp:Panel>
            </td>
            <td width="1080px" height="100px" style="border-style: none;">
                <asp:Panel ID="pnlStuff" runat="server" Width="1080px">
                    <LMC:PatientInfo ID="PatInfo" runat="server" />
                    <iframe frameborder="0" id="PageContents" runat="server" width="1070px" height="1600px"
                        src="" style="overflow: auto;" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PnlPopup" runat="server" Style="display: none">
        <asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
        <cc1:ModalPopupExtender ID="modReceived" BackgroundCssClass="ModalPopupBG" runat="server"
            CancelControlID="" TargetControlID="Dummy" PopupControlID="pnlReceived" />
        <asp:Panel ID="pnlReceived" runat="server" CssClass="modalPopup" Width="600px">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="Div4">
                    <div class="TitlebarLeft" id="Div5" runat="server">
                        Error in patient search
                    </div>
                </div>
                <div class="popup_Body">
                    <p style="font-size: large">
                        You have not selected a patient.
								<p style="font-size: large">
                                    To select a patient enter a portion of the first name OR the last name in the search
									box and <strong>wait for the drop down.</strong>
                                </p>
                        <p style="font-size: large">
                            Then click on the appropriate patient.
                        </p>
                </div>
                <div class="popup_Buttons">
                    <asp:Button ID="btnCancelDoc" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancelDoc_Click" />
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>

    <div id="loading-div-background">
        <div id="loading-div" class="ui-corner-all">
            <img src="images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>

  
</asp:Content>
