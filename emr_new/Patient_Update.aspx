<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="Patient_Update.aspx.cs" Inherits="Patient_Update" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
	
    <script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="Scripts/PatientAdd.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Button ID="dummy2" runat="server" Style="display:none;" />
        <cc1:ModalPopupExtender ID="modXeroCreation" BackgroundCssClass="ModalPopupBG" runat="server"
            CancelControlID="" TargetControlID="dummy2" PopupControlID="pnlXeroCreation" />
        <asp:Panel ID="pnlXeroCreation" runat="server" CssClass="modalPopup" Width="600px" style="display:none">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="Div3">
                    <div class="TitlebarLeft" id="Div6" runat="server">
                        No Xero Match
                    </div>
                </div>
                <div class="popup_Body">
                    <p>
                        You are about to create a new profile <br /> in Xero. Click 'Yes' to Proceed, 'No' to only update the EMR.

                    </p>
                </div>
                <div class="popup_Buttons">
                    <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes"
                        CssClass="button" />&nbsp;
					<asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="No"
                        CssClass="button" />
                </div>
            </div>
        </asp:Panel>
    <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
        <tr bgcolor="#D6B781">
            <td bgcolor="#D6B781">
                <b>PatientId:</b>&nbsp;
                <asp:Label ID="txtPatientId" runat="server"  />

            </td>
            <td colspan="2">

                    Xero PatiendId:
                    <asp:TextBox ID="txtXeroPatientId" runat="server" CssClass="FormField "  Width="300px" />
               
            </td>

            <td>
                <div align="right">
                     <asp:Button ID="btnSaveXeroPatientId" runat="server" CssClass="button" Text="Save" OnClick="btnSaveXeroPatientId_Click" />
                </div>
            </td>
        </tr>
        <tr bgcolor="#D6B781">
            <td colspan="3" bgcolor="#D6B781">
                <b>Peronal Information</b>
            </td>

            <td>
                <div align="right">
                    China PatiendId:
                    <asp:TextBox ID="txtChinapatientid" runat="server" CssClass="FormField " MaxLength="50" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <b><font color="#666666">First Name</font><span class="Validation_StarMark_Color">*</span></b>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="FormField firstName" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
            </td>
            <td height="24">
                <font color="#666666"><b>Last Name</b></font><span class="Validation_StarMark_Color">*</span>
            </td>
            <td height="24">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="FormField lastName" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                <b><font color="#666666">Nick Name</font></b>
            </td>
            <td>
                <asp:TextBox ID="txtNickName" runat="server" CssClass="FormField" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
            </td>
            <td height="24">
                <b><font color="#666666">Middle Initial</font></b>
            </td>
            <td height="24">
                <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="FormField middleInitial" onkeypress="return Restrictspecialchar(event)" MaxLength="25" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Email Address<span class="Validation_StarMark_Color">*</span></b></font>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="FormField emailClass" MaxLength="50" />
            </td>
            <td height="24">
                <font color="#666666"><b>Gender</b></font>
            </td>
            <td height="24">
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="FormField">
                    <asp:ListItem Text="Male" Value="M" />
                    <asp:ListItem Text="Female" Value="F" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Event</b></font>
            </td>
            <td>
                <asp:DropDownList ID="ddlMktg" runat="server" CssClass="FormField" />
            </td>
            <td>
                <font color="#666666"><b>Patient Type</b></font>
            </td>
            <td>
                <font color="#666666"><b>
					<asp:CheckBox ID="cbMedical" runat="server" Text="Medical" />
					<asp:CheckBox ID="cbAesthetics" runat="server" Text="Aesthetics" /><br />
					<asp:CheckBox ID="cbAutoship" runat="server" Text="Autoship" />
					<asp:CheckBox ID="cbRetail" runat="server" Text="Retail only" /><br />
					<asp:Label ID="lblAffName" runat="server" CssClass="regText" Text="<b>Affiliate Name:</b>" />
					<asp:DropDownList ID="ddlAffiliate" runat="server" CssClass="FormField"></asp:DropDownList>
					<br />
					<asp:CheckBox ID="cboAffiliate" runat="server" Text="This patient is an affiliate." />
				</b></font>
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Birthday</b></font>
            </td>
            <td>
                <asp:TextBox ID="txtBirthday" runat="server" CssClass="FormField birthday" />&nbsp;(ex: mm/dd/yyyy)
                <obout:Calendar ID="calBirthday" runat="server" DatePickerMode="true" TextBoxId="txtBirthday"
                    DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif" ShowYearSelector="true" YearSelectorType="DropDownList" ShowMonthSelector="true" MonthSelectorType="DropDownList" TitleText="" DateMin="1/1/1900"
                    AutoPostBack="false" />

            </td>
            <td>
                <strong><font color="#666666">Patient Advocate</font></strong><span class="Validation_StarMark_Color">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlConcierge" CssClass="FormField concierge" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Renewal Date</b></font>
            </td>
            <td>

                <asp:TextBox ID="txtRenewalDate" runat="server" CssClass="FormField birthday" ClientIDMode="Static" />(mm/dd/yyyy)
                              <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtRenewalDate"
                                  DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
                                  AutoPostBack="false" ShowYearSelector="true" YearSelectorType="DropDownList" ShowMonthSelector="true" MonthSelectorType="DropDownList" TitleText="" DateMin="1/1/1900" />

            </td>
            <td width="150">
                <font color="#666666"><b>Authorised Person</b></font>
            </td>
            <td>

                <asp:TextBox ID="txtAuthorisedPerson" runat="server" CssClass="FormField" />
            </td>
        </tr>

        <tr>
            <td>
                <font color="#666666"><b>Management Program</b></font>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="FormField concierge" ID="ddlManagementProgram" />

            </td>
            <td width="150"><font color="#666666"><b></b></font><span style="color: red"></span>
            </td>
            <td></td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td>
                <b>Shipping Info</b>
            </td>
            <td bgcolor="#D6B781">&nbsp;
                
                    <asp:CheckBox ID="chkCallBeforeShip" runat="server" ClientIDMode="Static" />
                Call Before Ship
            </td>
            <td>
                <b>Billing Info </b>
                <label id="lblchkbilling" runat="server" onclick="showHideDropDowns();">
                    <asp:CheckBox ID="chkCopyAddress" runat="server" ClientIDMode="Static" /></label>
                Tick the box to copy the Shipping Address to billing adress.
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Address</b></font>
            </td>
            <td>
                <asp:TextBox ID="txtShipAddress" runat="server" CssClass="FormField" MaxLength="150" ClientIDMode="Static" />
            </td>
            <td height="24">
                <asp:TextBox ID="txtBillAddress" runat="server" CssClass="FormField" MaxLength="150" ClientIDMode="Static" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>City, State Zip </b></font>
            </td>
            <td>
                <asp:TextBox ID="txtShipCity" runat="server" CssClass="FormField" MaxLength="50" ClientIDMode="Static" />
            </td>
            <td height="24">
                <asp:TextBox ID="txtBillCity" runat="server" CssClass="FormField" MaxLength="50" ClientIDMode="Static" />
                <br />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlShipState" runat="server" Style="margin-bottom: 0px" CssClass="FormField" ClientIDMode="Static">
                    <asp:ListItem Value="">Please select</asp:ListItem>
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtShipZip" runat="server" CssClass="FormField" Columns="7" MaxLength="7" ClientIDMode="Static" />
            </td>
            <td height="24">
                <asp:DropDownList ID="ddlBillState" runat="server" CssClass="FormField" ClientIDMode="Static">
                    <asp:ListItem Value="">Please select</asp:ListItem>
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBillZip" runat="server" CssClass="FormField" Columns="7" MaxLength="7" ClientIDMode="Static" />
            </td>
        </tr>

        <tr>
            <td>
                <font color="#666666"><b>Country</b></font>
            </td>
            <td>
                <asp:TextBox ID="txtShipCountry" runat="server" CssClass="FormField" MaxLength="250" ClientIDMode="Static" />
            </td>
            <td height="24">
                <asp:TextBox ID="txtbillCountry" runat="server" CssClass="FormField" MaxLength="250" ClientIDMode="Static" />
                <br />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td>
                <strong>Contact Info </strong>
            </td>
            <td></td>
            <td height="24" colspan="2"></td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Home Phone </b></font>
            </td>
            <td>
                <asp:TextBox ID="txtHomePhone" runat="server" CssClass="FormField" MaxLength="30" />
            </td>
            <td height="24">
                <font color="#666666"><b>Work Phone</b></font>
            </td>
            <td height="24">
                <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="FormField" MaxLength="30" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Call Back # Only</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboHome_cbo" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Call Back # Only</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboWork_cbo" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Leave Detailed Info</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboHome_detailed" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Leave Detailed Info</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboWork_detailed" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Do not leave message</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboHome_NoMessage" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Do not leave message</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboWork_NoMessage" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="3" colspan="4">
                <hr size="1" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Cell Phone</b></font>
            </td>
            <td>
                <asp:TextBox ID="txtCellPhone" runat="server" CssClass="FormField" MaxLength="30" />
            </td>
            <td height="24">
                <font color="#666666"><b>Fax Phone</b></font>
            </td>
            <td height="24">
                <asp:TextBox ID="txtFaxPhone" runat="server" CssClass="FormField" MaxLength="20" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Call Back # Only</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboCell_cbo" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Leave Detailed Info</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboFax_detailed" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Leave Detailed Info</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboCell_detailed" runat="server" />
            </td>
            <td height="24">Statement of Charges
            </td>
            <td height="24">
                <font color="#666666"><b>
				<asp:RadioButtonList ID="rdoSOC" runat="server" RepeatDirection="Horizontal" >
				<asp:ListItem Text="Yes" Value="Yes" />
				<asp:ListItem Text="No" Value="No" />
				<asp:ListItem Text="None" Value="None" />
				</asp:RadioButtonList>
				</b></font>
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Do not leave message</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboCell_NoMessage" runat="server" />
            </td>
            <td height="24"><font color="#666666">Labs Mailed</font></td>
            <td height="24">
                <asp:CheckBox runat="server" ID="cbLabMailed" /></td>
        </tr>
        <tr>
            <td height="3" colspan="4">
                <hr size="1" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Email Detailed Info</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboEmail_detailed" runat="server" />
            </td>
            <td height="24">
                <font color="#666666"><b>Clinic</b></font><span class="Validation_StarMark_Color">*</span>
            </td>
            <td height="24">
                <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormField clinic">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Signed Medicare Opt Out</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboMedicare_opt" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Opt Out Date</font></strong>
            </td>
            <td height="24">
                <asp:TextBox ID="txtOptOutDate" runat="server" CssClass="FormField" />
                <cc1:CalendarExtender ID="CalMedoutDate" runat="server" TargetControlID="txtOptOutDate" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Eating Plan Received</font></strong>
            </td>
            <td>
                <asp:CheckBox ID="cboEatingplan" runat="server" />
            </td>
            <td height="24">
                <strong><font color="#666666">Medicare Part B</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboMedicareB" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td height="24" colspan="4">
                <strong>Emergency Contact Info </strong>
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Prefered Pharm</font></strong>
            </td>
            <td>
                <asp:TextBox ID="txtPreferredPharm" runat="server" CssClass="FormField" MaxLength="50" />
            </td>
            <td height="24">
                <strong><font color="#666666">HIPAA Signed</font></strong>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboHippa_signed" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">LMC Physician</font></strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlLMCPhysician" runat="server" CssClass="FormField" />
            </td>
            <td height="24">
                <strong><font color="#666666">Signed Date</font></strong>
            </td>
            <td height="24">
                <asp:TextBox ID="txtHIPPASignedDate" runat="server" CssClass="FormField" />
                <cc1:CalendarExtender ID="CalExthpsdate" runat="server" TargetControlID="txtHippaSignedDate" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><font color="#666666">Primary Care Phys</font></strong>
            </td>
            <td>
                <asp:TextBox ID="txtPCP" runat="server" CssClass="FormField" MaxLength="200" />
            </td>
            <td height="24">
                <font color="#666666"><b>Cancel/No<br />
					Signed</b></font>
            </td>
            <td height="24">
                <asp:CheckBox ID="cboNoShow" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>First, Last </b></font>
            </td>
            <td>
                <asp:TextBox ID="txtEmerFirst" runat="server" CssClass="FormField" MaxLength="20" />
                <asp:TextBox ID="txtEmerLast" runat="server" CssClass="FormField" MaxLength="20" />
            </td>
            <td height="24">
                <font color="#666666"><b>Phone</b></font>
            </td>
            <td height="24">
                <asp:TextBox ID="txtEmerPhone" runat="server" CssClass="FormField" />
            </td>
        </tr>
        <tr>
            <td>
                <font color="#666666"><b>Contact Preference</b></font>
            </td>
            <td>
                <asp:TextBox ID="txtContactPref" runat="server" CssClass="FormField" MaxLength="50" />
            </td>
            <td height="24">
                <font color="#666666"><b>Relationship</b></font>
            </td>
            <td height="24">
                <asp:TextBox ID="txtEmerRelat" runat="server" CssClass="FormField" MaxLength="50" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td height="24" colspan="4">
                <strong>Active/Inactive</strong>
            </td>
        </tr>
        <tr>
            <td>
                <span class="style1">Name Alerts</span>
            </td>
            <td>
                <asp:CheckBox ID="cboNamealert" runat="server" />
            </td>
            <td height="24">&nbsp;
            </td>
            <td height="24">&nbsp;
            </td>
        </tr>
        <tr>
            <td width="213">
                <font color="#666666">Inactive (<strong>check</strong> to make inactive)</font>
            </td>
            <td width="300">
                <asp:CheckBox ID="cboInactive" runat="server" />
            </td>
            <td width="18" height="24">&nbsp;
            </td>
            <td width="19" height="24">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" OnClientClick="return AddPatientDetails();" />&nbsp;
				<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />


            </td>
        </tr>
    </table>

     

    <div>


        <script src="Scripts/jquery.filter_input.js" type="text/javascript">
        </script>

        <script type="text/javascript">
            $(function () {
                $('#<%= txtHomePhone.ClientID %>').mask("999-999-9999");
                $('#<%= txtBirthday.ClientID %>').mask("99/99/9999");
                $('#<%= txtWorkPhone.ClientID %>').mask("999-999-9999");
                $('#<%= txtCellPhone.ClientID %>').mask("999-999-9999");
                $('#<%= txtEmerPhone.ClientID %>').mask("999-999-9999");
                $('#<%= txtShipZip.ClientID %>').filter_input({ regex: '[0-9]' });
                $('#<%= txtBillZip.ClientID %>').filter_input({ regex: '[0-9]' });
                $('#<%= txtChinapatientid.ClientID %>').filter_input({ regex: '[0-9]' });
            });
        </script>

    </div>
</asp:Content>
