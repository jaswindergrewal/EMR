<%@ Page Title="Add New Patient" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Patient_Add.aspx.cs" Inherits="Patient_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/PatientAdd.js" type="text/javascript"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div style="position: absolute; left: 123px; width: 1001px;">
        <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="3" bgcolor="#D6B781">
                    <p>
                        <b>Create a New Patient Form</b>
                    </p>
                </td>
                <td>
                    <div align="right">
                    </div>
                </td>
            </tr>
            <tr>
                <td width="135">
                    <b><font color="#666666">First Name</font></b><span style="color: red">*</span>
                </td>
                <td width="239">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="FormField firstName" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
                </td>
                <td width="150" nowrap="nowrap">
                    <font color="#666666"><b>Last Name</b></font><span style="color: red">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="FormField lastName" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td width="135">
                    <b><font color="#666666">Nick Name</font></b>
                </td>
                <td width="239">
                    <asp:TextBox ID="txtNickName" runat="server" CssClass="FormField" onkeypress="return Restrictspecialchar(event)" MaxLength="50" />
                </td>
                <td width="150" nowrap="nowrap">
                    <font color="#666666"><b>Middle Initial</b></font>
                </td>
                <td>
                    <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="FormField middleInitial" onkeypress="return Restrictspecialchar(event)" MaxLength="25" />
                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Email Address</b></font><span style="color: red">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="FormField emailClass" MaxLength="50" />
                </td>
                <td width="150">
                    <font color="#666666"><b>Gender</b></font>
                </td>
                <td>
                    <asp:DropDownList runat="server" CssClass="border" ID="txtGender">
                        <asp:ListItem Value="M" Text="Male" />
                        <asp:ListItem Value="F" Text="Female" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Event</b></font>
                </td>
                <td>

                    <asp:DropDownList runat="server" CssClass="FormField" ID="ddlMark_source" />
                </td>
                <td width="150">
                    <font color="#666666"><b>Patient Type</b></font>
                </td>
                <td>
                    <font color="#666666"><b>
                        <asp:CheckBox runat="server" ID="cbMedical" Text="Medical" />
                        <asp:CheckBox runat="server" ID="cbAesthetics" Text="Aesthetics" />
                        <asp:CheckBox runat="server" ID="cbAutoship" Text="Autoship" />
                        <asp:CheckBox runat="server" ID="cbRetail" Text="Retail only" /><br />
                        <asp:CheckBox runat="server" ID="cbAffiliate" Text="Affiliate" />
                        Name:
							
                            <asp:DropDownList ID="ddlAffiliate" runat="server" CssClass="FormField" />
                        <br />
                        <asp:CheckBox ID="cboAffiliate" runat="server" Text="This patient is an affiliate." /></b></font>
                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Birthday</b></font>
                </td>
                <td>
                   
                    <asp:TextBox ID="txtBirthday" runat="server" CssClass="FormField birthday" ClientIDMode="Static" />(mm/dd/yyyy)
                              <obout:Calendar ID="calBirthday" runat="server" DatePickerMode="true" TextBoxId="txtBirthday"
                                  DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif"
                                  AutoPostBack="false" ShowYearSelector="true" YearSelectorType="DropDownList" ShowMonthSelector="true" MonthSelectorType="DropDownList" TitleText="" DateMin="1/1/1900" />
                    
                </td>
                <td width="150"><font color="#666666"><b>Patient Advocate</b></font><span style="color: red">*</span>
                </td>
                <td>

                    <asp:DropDownList runat="server" CssClass="FormField concierge" ID="ddlConcierge" />
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

                   <asp:TextBox ID="txtAuthorisedPerson" runat="server" CssClass="FormField"  />
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
                <td>

                    
                </td>            </tr>
        </table>
        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="tblAddress ">
            <tr bgcolor="#D6B781">
                <td>
                    <b>Shipping Info</b>
                </td>
                <td></td>
                <td>
                    <b>Billing Info </b>

                    <label id="lblchkbilling" runat="server" onclick="showHideDropDowns();">
                        <asp:CheckBox ID="chkCopyAddress" runat="server" ClientIDMode="Static" /></label>
                    Tick the box to copy the Shipping Address to billing adress.
                </td>
            </tr>
            <tr class="border">
                <td>
                    <font color="#666666"><b>Address</b></font>
                </td>
                <td>
                    <asp:TextBox ID="txtShipAddress" runat="server" CssClass="FormField" MaxLength="150" ClientIDMode="Static" />
                </td>
                <td>
                    <asp:TextBox ID="txtBillAddress" runat="server" CssClass="FormField" MaxLength="150" ClientIDMode="Static" />
                </td>
            </tr>
            <tr class="border">
                <td>
                    <font color="#666666"><b>City, State Zip </b></font>
                </td>
                <td>
                    <asp:TextBox ID="txtShipCity" runat="server" CssClass="FormField" MaxLength="50" ClientIDMode="Static" />

                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtBillCity" runat="server" CssClass="FormField" MaxLength="50" ClientIDMode="Static" />
                    <br />
                </td>
            </tr>
            <tr class="border">
                <td>
                </td>
                <td>
                    <asp:DropDownList runat="server" CssClass="border" ID="ddlShipState" ClientIDMode="Static">
                        <asp:ListItem Value="">Please select</asp:ListItem>
                        <asp:ListItem Value="AL">Alabama</asp:ListItem>
                        <asp:ListItem Value="AK">Alaska</asp:ListItem>
                        <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                        <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                        <asp:ListItem Value="CA">California</asp:ListItem>
                        <asp:ListItem Value="CO">Colorado</asp:ListItem>
                        <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                        <asp:ListItem Value="DE">Delaware</asp:ListItem>
                        <asp:ListItem Value="DC">District Of Columbia</asp:ListItem>
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
                    <asp:TextBox ID="txtShipZip" runat="server" CssClass="FormField" MaxLength="7" ClientIDMode="Static" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlBillState" runat="server" CssClass="border" ClientIDMode="Static">
                        <asp:ListItem Value="">Please select</asp:ListItem>
                        <asp:ListItem Value="AL">Alabama</asp:ListItem>
                        <asp:ListItem Value="AK">Alaska</asp:ListItem>
                        <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                        <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                        <asp:ListItem Value="CA">California</asp:ListItem>
                        <asp:ListItem Value="CO">Colorado</asp:ListItem>
                        <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                        <asp:ListItem Value="DE">Delaware</asp:ListItem>
                        <asp:ListItem Value="DC">District Of Columbia</asp:ListItem>
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
                    <asp:TextBox ID="txtBillZip" runat="server" CssClass="FormField" MaxLength="7" ClientIDMode="Static" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td>
                    <strong>Contact Info </strong>
                </td>
                <td>
                </td>
                <td height="24" colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Home Phone </b></font>
                </td>
                <td>
                    <asp:TextBox ID="txtHomephone" runat="server" CssClass="FormField" MaxLength="30" ClientIDMode="Static"/>
                </td>
                <td height="24">
                    <font color="#666666"><b>Work Phone</b></font>
                </td>
                <td height="24">
                    <asp:TextBox ID="txtWorkphone" runat="server" CssClass="FormField" MaxLength="30" ClientIDMode="Static"/>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Call Back # Only</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbHome_cbo" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Call Back # Only</font></strong>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbWork_cbo" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Leave Detailed Info</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbHome_detailed" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Leave Detailed Info</font></strong>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbWork_detailed" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Do not leave message</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbHome_NoMessage" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Do not leave message</font></strong>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbWork_NoMessage" />
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
                    <asp:TextBox ID="txtCellphone" runat="server" CssClass="FormField" MaxLength="30" ClientIDMode="Static"/>
                </td>
                <td height="24">
                    <font color="#666666"><b>Fax Phone</b></font>
                </td>
                <td height="24">
                    <asp:TextBox ID="txtFaxphone" runat="server" CssClass="FormField" MaxLength="30" ClientIDMode="Static"/>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Call Back # Only</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbCell_cbo" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Leave Detailed Info</font></strong>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbFax_detailed" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Leave Detailed Info</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbCell_detailed" />
                </td>
                <td height="24">
                    <font color="#666666"><b>Eating Planned Received</b></font>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbEatingPlan" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Do not leave message</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbCell_NoMessage" />
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
                    <asp:CheckBox runat="server" ID="cbEmail_detailed" />
                </td>
                <td height="24">
                    <font color="#666666"><b>Clinic</b></font><span style="color: red">*</span>
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
                    <asp:CheckBox runat="server" ID="cbMedicare_opt" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Opt Out Date</font></strong>
                </td>
                <td height="24">
                    <asp:TextBox ID="txtMedicareOptoutDate" runat="server" CssClass="FormField" />
                    <cc1:CalendarExtender ID="CalMedoutDate" runat="server" TargetControlID="txtMedicareOptoutDate" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong><font color="#666666">Medicare Part B</font></strong>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbMedicareB" />
                </td>
                <td height="24"></td>
                <td height="24"></td>
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
                    <asp:TextBox ID="txtPreferedPharm" runat="server" CssClass="FormField" MaxLength="50" />
                </td>
                <td height="24">
                    <strong><font color="#666666">HIPPA Signed</font></strong>
                </td>
                <td height="24">
                    <asp:CheckBox runat="server" ID="cbHippa_signed_yn" />
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style1"><strong>Primary Care Phys</strong></span>
                </td>
                <td>
                    <asp:TextBox ID="txtPCP" runat="server" CssClass="FormField" MaxLength="200" />
                </td>
                <td height="24">
                    <strong><font color="#666666">Signed Date</font></strong>
                </td>
                <td height="24">
                    <asp:TextBox ID="txtHippaSignedDate" runat="server" CssClass="FormField" />
                    <cc1:CalendarExtender ID="CalExthpsdate" runat="server" TargetControlID="txtHippaSignedDate" />
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
                    <span class="style1"><strong>LMC Physician</strong></span>
                </td>
                <td height="24">

                    <asp:DropDownList ID="ddlMLCPhysician" runat="server" CssClass="FormField" />
                </td>
            </tr>
            <tr>
                <td>
                    <font color="#666666"><b>Contact Preference</b></font>
                </td>
                <td>
                    <asp:TextBox ID="txtContact_pref" runat="server" CssClass="FormField" MaxLength="50" />
                </td>
                <td height="24">
                    <font color="#666666"><b>Phone</b></font>
                </td>
                <td height="24">
                    <asp:TextBox ID="txtEmerPhone" runat="server" CssClass="FormField" MaxLength="50" ClientIDMode="Static"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
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
        <p>
            <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClientClick="return AddPatientDetails();"
                OnClick="btnAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"
                CausesValidation="false" />
            <asp:Label ID="lblUnique" runat="server" ForeColor="Red" Text="This patient is already in the system.  This includes inactive patients."
                Visible="false" />
            <br />

           
            <script type="text/javascript">
                //Apply mask edit to textboxes
                $(function () {
                    $('#txtHomephone').mask("999-999-9999");
                    $('#txtWorkphone').mask("999-999-9999");
                    $('#txtCellphone').mask("999-999-9999");
                    $('#txtEmerPhone').mask("999-999-9999");
                    $('#txtShipZip').filter_input({ regex: '[0-9]' });
                    $('#txtBillZip').filter_input({ regex: '[0-9]' });
                    $('#txtBirthday').mask("99/99/9999");
                    $('#txtFaxphone').mask("999-999-9999");
                });
                </script>
           
        </p>
    </div>

</asp:Content>
