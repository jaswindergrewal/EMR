<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarAppointment.aspx.cs" Inherits="Database_Default"
    EnableEventValidation="false" MasterPageFile="Site.master" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="Cal" TagPrefix="Longevity" Src="~/controls/OneCal.ascx" %>
<%@ Register TagName="TDD" TagPrefix="Longevity" Src="~/controls/TimeDropDown.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />
    <style>.CompletionListCssClass
{
   list-style: none outside none;
    z-index: 1000000 !important ;
    border: 1px solid buttonshadow;
  cursor: default;
  padding: 0px;
  margin: 0px;
   
   
}</style>

    <script type="text/javascript">


        function CheckProvider() {
            var providerID = document.getElementById("ProvID");
            alert(providerID.value);
        }

        function HideModal() {

            $find('modalPopupBehavior').hide();
        }


        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            $find("mpe1").hide();
            return false;
        }


        //Function added by jaswinder to validate data befor creating/updating appointment
        //12 th aug 2013
        function ValidateData() {
            var MainContent_EventDetail = $('table#MainContent_EventDetail');
            var txtPatient = $('[id$=txtPatient]', MainContent_EventDetail);
            var txtStartDate = $('[id$=txtStartDate]', MainContent_EventDetail);
            var txtEndDate = $('[id$=txtEndDate]', MainContent_EventDetail);
            var startDate = new Date($('[id$=txtStartDate]', MainContent_EventDetail).val());
            var endDate = new Date($('[id$=txtEndDate]', MainContent_EventDetail).val());
            var today = new Date();
            var txtEndTime = $('[id$=txtEndTime_MainDrop_ComboBoxTextBox]', MainContent_EventDetail);
            var txtStartTime = $('[id$=txtStartTime_MainDrop_ComboBoxTextBox]', MainContent_EventDetail);
            var txtEmail = $('[id$=txtEmail]', MainContent_EventDetail);
            //var Timeexp = /^((([1-9])|(1[02]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2}))/;
            var Timeexp = /^([0]?[1-9]|[1][0-2]):([0-5][0-9]|[1-9]) [APap][Mm]$/;
            var Timeexp2 = /^(0?[1-9]|1[012])(:[0-5]\d)[APap][mM]$/;
            var dateObjStart = new Date(txtStartDate.val() + ' ' + txtStartTime.val());
            var dateObjEnd = new Date(txtEndDate.val() + ' ' + txtEndTime.val());

            var dd, mm, yyyy;
            dd = today.getDate();
            mm = today.getMonth() + 1;
            yyyy = today.getFullYear();
            var CurrentDate = mm + "/" + dd + "/" + yyyy;
            var currdate1 = new Date(CurrentDate);
            if (txtPatient.val() == '') {
                alert('Please enter patient!');
                txtPatient.focus();
                return false;
            }
            else if (txtStartDate.val() == '') {
                alert('Please enter Start Date!');
                txtStartDate.focus();
                return false;
            }
            else if (txtStartTime.val() == "") {
                alert('Please enter start time!');
                txtStartTime.focus();
                return false;
            }
            else if (!Timeexp.test(txtStartTime.val())) {
                if (!Timeexp2.test(txtStartTime.val())) {
                    alert('Please enter valid start time!');
                    txtStartTime.focus();
                    return false;
                }
            }
            else if (!Timeexp2.test(txtStartTime.val())) {
                if (!Timeexp.test(txtStartTime.val())) {
                    alert('Please enter valid start time!');
                    txtStartTime.focus();
                    return false;
                }
            }
            else if (txtEndTime.val() == "") {
                alert('Please enter end time!');
                txtEndTime.focus();
                return false;
            }
            else if (!Timeexp.test(txtEndTime.val())) {
                if (!Timeexp2.test(txtEndTime.val())) {
                    alert('Please enter valid end time!');
                    txtStartTime.focus();
                    return false;
                }
            }
            else if (!Timeexp2.test(txtEndTime.val())) {
                if (!Timeexp.test(txtEndTime.val())) {
                    alert('Please enter valid end time!');
                    txtStartTime.focus();
                    return false;
                }
            }

            else if (startDate < currdate1) {
                alert('Start date cannot be less than current date!');
                txtStartDate.focus();
                return false;
            }

            else if (txtEndDate == '') {
                alert('Please enter End Date!');
                txtEndDate.focus();
                return false;
            }

            else if (dateObjEnd <= dateObjStart) {
                alert('Appointment end datetime should be greater then start datetime !');
                txtEndDate.focus();
                return false;
            }

            else if (txtEmail.val() != "") {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                if (!filter.test(txtEmail.val())) {
                    alert('Please enter valid email');
                    txtEmail.focus();
                    return false;
                }
            }
            else {
                return true;
            }



        }

        //function to show loader on on click of checkbox listitem
        function onProviderChecked(chkbox) {
            var count = $('.provClass :checkbox:checked').length
            if (count > 5) {

                alert('You cannot add more than five providers');
                $(chkbox).removeAttr('checked');
            }
            $('[id$=loadingdivbackground]').show();
            setTimeout("$('[id$=loadingdivbackground]').hide();", 1500);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div>  
        <asp:UpdatePanel ID="updAll" runat="server">
            <ContentTemplate>
            
                <table id="Table1" class="Appt" style="width:100%">
                    <tr>
                        <th class="Appt"  style="width:95%">Appointments for Today
                        </th>
                        <th class="Appt" style="width:5%">Days to display
                        </th>
                    </tr>
                    <tr>
                        <td class="Appt" style="width:95%">Time Division:
							<asp:RadioButton ID="RadioButton1" runat="server" Text="30 Min" GroupName="time"
                                OnCheckedChanged="OnSelChange" />
                            <asp:RadioButton ID="RadioButton2" runat="server" Text="15 Min" GroupName="time"
                                OnCheckedChanged="OnSelChange" />
                            <asp:RadioButton ID="RadioButton3" runat="server" Text="10 Min" GroupName="time"
                                OnCheckedChanged="OnSelChange" />
                            <asp:RadioButton ID="RadioButton4" runat="server" Text="5 Min " GroupName="time"
                                OnCheckedChanged="OnSelChange" />
                        </td>
                        <td class="Appt" nowrap="nowrap">
                            <asp:RadioButton ID="radio7Days" runat="server" Text="7 Days" GroupName="days" AutoPostBack="true" />
                            <asp:RadioButton ID="radio1Day" runat="server" Text="1 day" GroupName="days" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Appt" valign="top">
                            <asp:UpdatePanel ID="UpdatePanelCalendar" runat="server" RenderMode="Inline" ChildrenAsTriggers="false"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblEventID" runat="server" Style="display: none" />
                                    <table cellspacing="0" cellpadding="0" style="width:100%">
                                        <tr id="CalRow" runat="server">
                                            <td width="148px" valign="top">
                                                <asp:UpdatePanel ID="updateCalOnly" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <DayPilot:DayPilotNavigator ID="DayPilotNavigator1" runat="server" ShowMonths="3"
                                                            CssClassPrefix="navigator_silver_" VisibleRangeChangedHandling="CallBack" OnTimeRangeSelected="DayPilotNavigator1_TimeRangeSelected"
                                                            TimeRangeSelectedHandling="PostBack" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
                                                <input name="ButtonSked" type="button" class="button" onclick="window.open('./CombinedSchedule.aspx');"
                                                    value="Print Schedule" style="width:140px;"/>
                                                <br />
                                                <br />
                                                <input name="ButtonFollowUp" type="button" class="button" onclick="window.open('./FollowUps.aspx');"
                                                    value="Calendar Follow Ups" style="width:140px;" /><br />
                                                <br />
                                                <asp:Button ID="ButtonQuickAdd" runat="server" CssClass="button" OnClientClick="alert('A window will now open to add a patient.  When you are finished, close the window to continue.');window.open('../PatientSearch.aspx');"
                                                    Text="Add Patient" style="width:140px;" />
                                                <br />
                                                <br />
                                                <asp:Button ID="ButtonShowAppts" runat="server" CssClass="button" Text="Patient Appointments"
                                                    OnClientClick="window.open('./PatientAppointments.aspx');" style="width:140px;" />
                                                <br />
                                                <br />
                                                <input name="ButtonRecur" id="ButtonRecur" runat="server" type="button" class="button"
                                                    value="Recurring Appts" onclick="window.open('./Recurring.aspx');" style="width:140px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td id="TableCell1" valign="top" class="Appt">
                            <asp:Label ID="Label1" Text="Providers" Font-Bold="true" runat="server" /><br />
                            <asp:CheckBoxList ID="ProvidersCBox" runat="server" AutoPostBack="true" RepeatLayout="Flow" CssClass="provClass" Width="200px">
                            </asp:CheckBoxList>
                            <br />
                            <br />
                            <br />
                            <strong>Appointment Type</strong><br />
                            <asp:DropDownList ID="ApptTypeDropDown" runat="server" AutoPostBack="true" DataSourceID="AppointmentTypeSourceAll"
                                DataTextField="TypeName" DataValueField="id" OnSelectedIndexChanged="OnSelChange" Width="200px">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <br />
                            <br />
                            <strong>Status</strong><br />
                            <asp:DropDownList ID="StatusDropDown" runat="server" AutoPostBack="true" DataSourceID="StatusSourceAll"
                                DataTextField="StatusName" DataValueField="id" Width="200px">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
                <DayPilot:DayPilotMenu ID="DayPilotMenu1" runat="server">
                    <DayPilot:MenuItem Text="Edit..." Action="PostBack" Command="Open"></DayPilot:MenuItem>
                    <DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
                    <DayPilot:MenuItem Text="Delete" Action="Callback" Command="Delete"></DayPilot:MenuItem>
                </DayPilot:DayPilotMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="1050px" Height="600px"
            Style="overflow: auto;">
            <div style="display: none;">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1" />
                <cc1:TabContainer ID="xxx" runat="server">
                    <cc1:TabPanel ID="yyyy" runat="server">
                    </cc1:TabPanel>
                </cc1:TabContainer>
                <cc1:ComboBox ID="zzz" runat="server" />
                <Longevity:TDD ID="qqq" runat="server" />
            </div>
            <asp:UpdatePanel ID="UpdatePanelDetail" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                CssClass="modalBackground">

                <ContentTemplate>
                    <asp:FormView ID="EventDetail" runat="server" DefaultMode="Edit" DataSourceID="ObjectDataSourceDetail"
                        DataKeyNames="EventID" AutoGenerateEditButton="True" OnItemCommand="EventDetail_ItemCommand"
                        OnItemUpdating="EventDetail_ItemUpdating" AutoGenerateInsertButton="True" AutoGenerateRows="False"
                        GridLines="None" Caption="Edit Appointment" OnDataBound="EventDetail_DataBound"
                        OnPreRender="EventDetail_PreRender" RenderOuterTable="true" Visible="false" OnModeChanging="EventDetail_ModeChanging">
                        <EditItemTemplate>
                            <tr>
                                <td class="border" width="450" valign="top">
                                    <table border="0" cellpadding="4" cellspacing="0" class="border">
                                        <caption>
                                            Appointment Details</caption>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Patient</b></font>
                                            </td>
                                            <td align="left">
                                                 
                                                <asp:TextBox runat="server" ID="txtPatient" Text='<%# Eval("Patient") %>' CausesValidation="false"
                                                    TabIndex="1" AutoPostBack="true" OnTextChanged="txtPatient_TextChanged" Width="250" CssClass="FormField" />
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="3"
                                                    ServiceMethod="GetPatientList" ServicePath="~/PatientNamesServ.asmx" TargetControlID="txtPatient" CompletionListCssClass="CompletionListCssClass" />
                                                <%--<asp:Button ID="btnLookup" runat="server" Text="Lookup patient" OnClick="btnLookup_Click"
													CssClass="button" Style="display: none" />--%>
                                                <br />
                                                <asp:Label ID="lblInactive" runat="server" Text="This patient is inactive" ForeColor="Red"
                                                    Font-Bold="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Start</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="txtStartDate" Columns="8" Text='<%# Eval("ApptStart") %>'
                                                    CausesValidation="true" AutoPostBack="false" TabIndex="2" CssClass="FormField" />&nbsp;
												<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" />
                                                <Longevity:TDD ID="txtStartTime" runat="server" />
                                                *
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>End</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="txtEndDate" Columns="8" Text='<%# Eval("ApptEnd") %>'
                                                    AutoPostBack="false" TabIndex="4" CssClass="FormField" />&nbsp;
												<cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndDate" />
                                                <Longevity:TDD runat="server" ID="txtEndTime" />
                                                *
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Clinic</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlClinic" runat="server" DataTextField="ClinicName" DataValueField="ClinicName"
                                                    AutoPostBack="true" TabIndex="5"  CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>All-Day</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox runat="server" ID="cbAllDay" Checked='<%# Eval("AllDay") %>' AutoPostBack="false"
                                                    TabIndex="6" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Appointment Type</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ApptTypeDropDown" runat="server" DataTextField="TypeName" DataValueField="id"
                                                    AutoPostBack="true" TabIndex="7" OnSelectedIndexChanged="ApptTypeDropDown_SelectedIndexChanged" CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <%--<tr>
                                            <td align="right">
                                                <font color="#666666"><b>Locked Appointment </b></font>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox runat="server" ID="cbLockedAppointment" Checked='<%# Eval("LockedAppointment") %>' AutoPostBack="false"
                                                    TabIndex="8" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ForeColor="#666666" Font-Bold="true" ID="lblFollow" runat="server" Text="Open follow ups" />
                                            </td>
                                            <td align="left" valign="top">
                                                <div style="border-style: groove; overflow: auto; width: 180px; height: 70px; border-color: #E8E8E8; border-width: thin">
                                                    <asp:Label ID="lblNoFollowUps" runat="server" Text="No open follow-ups." Visible="false" />
                                                    <asp:CheckBoxList ID="ddFollowups" runat="server" DataTextField="TypeName" DataValueField="id"
                                                        TabIndex="7" OnSelectedIndexChanged="ddFollowups_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:CheckBoxList>
                                                </div>
                                                <asp:Button ID="btnFollowup" runat="server" Text="Print Followup(s)" Visible="false"
                                                    CssClass="button" />
                                                <asp:Button ID="btnDetach" runat="server" Text="Detach" Visible="false" CssClass="button"
                                                    OnClick="btnDetach_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Provider</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ProviderDropDown" runat="server" DataSourceID="ProvidersSource"
                                                    DataTextField="ProviderName" DataValueField="id" AutoPostBack="false" TabIndex="8"
                                                    ClientIDMode="Static" CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>HA Rep</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpHARep" runat="server" 
                                                     AutoPostBack="false" TabIndex="8" DataTextField="EmployeeName" DataValueField="EmployeeID"
                                                    ClientIDMode="Static" CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Results</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ResultsDropDown" runat="server" DataSourceID="ResultsSource"
                                                    DataTextField="ResultName" DataValueField="ID" AutoPostBack="false" 
                                                    TabIndex="9" CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Status</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="StatusDropDown" runat="server" DataSourceID="StatusSourceOnly"
                                                    DataTextField="StatusName" DataValueField="ID" AutoPostBack="false" TabIndex="10" CssClass="FormField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <font color="#666666"><b>Email on Change</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox runat="server" ID="cbEmailOnChange" Checked='<%# Eval("EmailOnChange") %>'
                                                    CausesValidation="true" AutoPostBack="false" TabIndex="11" />
                                                <asp:TextBox runat="server" ID="txtEmail" Columns="15" Text='<%# Eval("Email") %>'
                                                    AutoPostBack="false" TabIndex="12" CssClass="FormField" MaxLength="100" /><br />
                                                <asp:Label ID="lblEmailWarn" runat="server" Text="Email for Internal Use Only" ForeColor="Red" />
                                                <asp:CustomValidator runat="server" ID="EmailCheck" OnServerValidate="CheckEmail"
                                                    EnableClientScript="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">Labs checked in?
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox runat="server" ID="cbLabsCheckedIn" Checked='<%# Eval("LabsCheckedIn") %>'
                                                    CausesValidation="true" AutoPostBack="false" TabIndex="11" />
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <font color="#666666"><b>Notes</b></font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="NotesBox" Rows="3" Columns="20"
                                                    Text='<%# Eval("Notes") %>' AutoPostBack="false" TabIndex="13" CssClass="FormField" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <input type="hidden" id="ProvID" value='<%= Eval("ProviderID") %>' runat="server" />
                                                <asp:Button runat="server" CssClass="button" ID="btnUpdate" OnClick="EventDetail_ItemUpdating"
                                                    Text="Update" CausesValidation="true" TabIndex="14" OnClientClick="return ValidateData();" />
                                                <asp:Button runat="server" ID="btnCancel" CssClass="button" OnClientClick="HideModal();"
                                                    Text="Cancel" CausesValidation="true" TabIndex="15" />
                                                <asp:Button runat="server" ID="btnEmail" Text="Confirmation will be sent" OnClick="btnEmail_Click"
                                                    CssClass="button" CausesValidation="false" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="val_txtPatient" runat="server" ControlToValidate="txtPatient"
                                                    ErrorMessage="Required." EnableClientScript="false" />
                                                <asp:CustomValidator ID="CheckPatient" runat="server" OnServerValidate="CheckPatient"
                                                    ErrorMessage="Patient not found" ControlToValidate="txtPatient" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate"
                                                    ErrorMessage="Start date is required." EnableClientScript="false" />
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtStartDate"
                                                    Type="Date" Operator="DataTypeCheck" ErrorMessage="Start date must be a valid date."
                                                    EnableClientScript="false" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate"
                                                    ErrorMessage="End date is required." EnableClientScript="false" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEndDate"
                                                    Type="Date" Operator="DataTypeCheck" ErrorMessage="End date must be a valid date."
                                                    EnableClientScript="false" />
                                                <asp:RegularExpressionValidator ID="RegEx" runat="server" ControlToValidate="txtEmail"
                                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                                                    ErrorMessage="Not a valid email address" EnableClientScript="false" />
                                                <asp:CustomValidator ID="val_Email" runat="server" ControlToValidate="txtEmail" OnServerValidate="CheckValidEmail"
                                                    ErrorMessage="Email address must be internal" /><br />
                                                <asp:CustomValidator ID="val_dates" runat="server" ControlToValidate="txtEndDate"
                                                    OnServerValidate="CheckDates" ErrorMessage="Start time must precede end time" />
                                                <asp:TextBox runat="server" ID="txtEventID" Text='<%# Eval("EventID") %>' ControlStyle-BackColor="White"
                                                    ControlStyle-ForeColor="White" ControlStyle-BorderStyle="None" Height="1" Width="1" />
                                                <asp:TextBox runat="server" ID="txtPatientID" Text='<%# Eval("PatientID") %>' ControlStyle-BackColor="White"
                                                    ControlStyle-ForeColor="White" ControlStyle-BorderStyle="None" Height="1" Width="1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="Appt" valign="top" width="600">
                                    <cc1:TabContainer ID="DetailsTab" runat="server" CssClass="lmc_tab">
                                        <cc1:TabPanel ID="DetailsPanel" runat="server" HeaderText="PatientDetails" CssClass="TabPanel">
                                            <ContentTemplate>
                                                <table width="100%" border="0" cellpadding="4" cellspacing="0" class="border">
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <b>Billing Info</b>
                                                        </td>
                                                        <td width="175" nowrap="nowrap">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2">
                                                            <b>Shipping Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Address</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="billingStreet" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2" id="shippingAddress" runat="server">
                                                            <font color="#666666"></font>
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>City, State Zip </b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="billingCityStateZip" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2" id="shippingCityStateZip" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666">&nbsp;</font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2">
                                                            <font color="#666666">&nbsp;</font>
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Home Phone </b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="homePhone" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td width="110" nowrap="nowrap">
                                                            <font color="#666666"><b>Work Phone</b></font>
                                                        </td>
                                                        <td width="246" id="workPhone" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Cell Phone</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="cellPhone" runat="server"></td>
                                                        <td colspan="3">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" id="patientEmail" runat="server" colspan="4"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Fax Phone</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="faxPhone" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <font color="#666666"><strong>Clinic</strong></font>
                                                        </td>
                                                        <td id="clinic" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666">&nbsp;</font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Birthday</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="birthday" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td id="active" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <strong><font color="#666674">Cancel/NS Form Signed</font></strong>
                                                        </td>
                                                        <td nowrap="nowrap" id="cancelNSFormSigned" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td id="nameAlert" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <strong><font color="#666674">HIPAA Form Signed</font></strong>
                                                        </td>
                                                        <td nowrap="nowrap" id="hipaaSigned" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666">&nbsp;</font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <font color="#666666"><b>Gender</b></font>
                                                        </td>
                                                        <td id="gender" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <strong><font color="#666666">Patient Advocate</font></strong>
                                                        </td>
                                                        <td nowrap="nowrap" id="Concierge" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <strong><font color="#666666">Labs Mailed</font></strong>
                                                        </td>
                                                        <td nowrap="nowrap" id="LabMailed" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <span class="style2"><strong>Primary Care Phys</strong></span>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="pcp" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Emergency Contact </b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="emergencyContact" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <span class="style2"><strong>LMC Physician</strong></span>
                                                        </td>
                                                        <td id="lmcProvider" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Phone</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="emergencyPhone" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Contact Preference</b></font>
                                                        </td>
                                                        <td id="contactPreference" runat="server"></td>
                                                    </tr>
                                                    <tr class="regText">
                                                        <td nowrap="nowrap">
                                                            <font color="#666666"><b>Relationship</b></font>
                                                        </td>
                                                        <td width="175" nowrap="nowrap" id="emergencyRelationship" runat="server"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td colspan="2">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="FollowUpsPanel" runat="server" HeaderText="Follow Ups" CssClass="TabPanel"
                                            Width="600px">
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="rptFollowUp" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                                                CellPadding="3" CellSpacing="3" CssClass="border" OnRowDataBound="rptFollowUp_RowDataBound"
                                                                Height="600px" Width="550px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Range">
                                                                        <ItemTemplate>
                                                                            <%# Eval("Range_Start") %><br />
                                                                            <%# Eval("Range_End") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DateEntered" HeaderText="Entered" DataFormatString="{0:d}" />
                                                                    <asp:BoundField DataField="Complete" HeaderText="Complete?" />
                                                                    <asp:TemplateField HeaderText="Category">
                                                                        <ItemTemplate>
                                                                            <a href="PrintFollowup.aspx?FollowUpID=<%# Eval("FollowUpID")	%>" target="_blank">
                                                                                <%# Eval("FollowUp_Type_Desc")	%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Notes">
                                                                        <ItemTemplate>
                                                                            <%# Eval("FollowUp_Body")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </td>
                            </tr>
                            <asp:ObjectDataSource ID="AppointmentTypeSourceOnly" runat="server" TypeName="Calendar.AppointmentTypes"
                                SelectMethod="getApptTypeListOnly">
                                <SelectParameters>
                                    <asp:Parameter Name="ProviderID" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </EditItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSourceDetail" runat="server" TypeName="Calendar.ProviderCal"
                        SelectMethod="GetApptByID">
                        <SelectParameters>
                            <asp:Parameter Name="id" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ProvidersSource" runat="server" TypeName="Calendar.Providers"
                        SelectMethod="getProviderList"></asp:ObjectDataSource>
                  
                    <asp:ObjectDataSource ID="StatusSourceAll" runat="server" TypeName="Calendar.Status"
                        SelectMethod="getStatusList"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="StatusSourceOnly" runat="server" TypeName="Calendar.Status"
                        SelectMethod="getStatusListOnly"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="AppointmentTypeSourceAll" runat="server" TypeName="Calendar.AppointmentTypes"
                        SelectMethod="getApptTypeList"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ResultsSource" runat="server" TypeName="Calendar.Results"
                        SelectMethod="getResultsList"></asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="ButtonDummy" runat="server" Style="display: none" />
        <cc1:ModalPopupExtender ID="ModalPopup" runat="server" TargetControlID="ButtonDummy"
            PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" BehaviorID="modalPopupBehavior" ClientIDMode="Static"/>
        <asp:Button ID="dummy2" runat="server" Style="visibility: hidden;" />
        <cc1:ModalPopupExtender ID="modExpiring" BackgroundCssClass="ModalPopupBG" runat="server"
            CancelControlID="" TargetControlID="dummy2" PopupControlID="pnlExpiring" />
        <asp:Panel ID="pnlExpiring" runat="server" CssClass="modalPopup" Width="600px">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="Div3">
                    <div class="TitlebarLeft" id="Div6" runat="server">
                        Plan Expiring
                    </div>
                </div>
                <div class="popup_Body">
                    <p>
                        Do you wish to change the provider for this patient?
                    </p>
                </div>
                <div class="popup_Buttons">
                    <asp:Button ID="btnChangeProv" runat="server" OnClick="ChangeProvder" Text="Yes"
                        CssClass="button" />&nbsp;
					<asp:Button ID="btnDontChangeProv" runat="server" OnClick="ChangeProvder" Text="No"
                        CssClass="button" />
                </div>
            </div>
        </asp:Panel>


           <asp:Button ID="dummyPatientAlert" runat="server" Style="display: none" />
						<cc1:ModalPopupExtender ID="ModalPatientAlert" BackgroundCssClass="ModalPopupBG" BehaviorID="mpe1"  runat="server"
							CancelControlID="" TargetControlID="dummyPatientAlert" PopupControlID="Panel1"
							Y="200" />
						<asp:Panel ID="Panel1" CssClass="modalPopup" Width="700px" runat="server" 
							Style="position: relative; top: 200px;">
							<div class="popup_Container">
								<div class="popup_Titlebar">
									<div class="TitlebarLeft">
										Message
									</div>
								</div>
								<div class="popup_Body">
									Renewal date for patient is expired.
								</div>
								<div class="popup_Buttons">
									
									<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" OnClientClick="return HideModalPopup()"  runat="server" />
								</div>
							</div>
						</asp:Panel>

          <asp:Button ID="dummyPatientAlert1" runat="server" Style="display: none" />
						<cc1:ModalPopupExtender ID="ModalPatientAlert1" BackgroundCssClass="ModalPopupBG" BehaviorID="mpe"  runat="server"
							CancelControlID="" TargetControlID="dummyPatientAlert1" PopupControlID="Panel2"
							Y="200" />
						<asp:Panel ID="Panel2" CssClass="modalPopup" Width="700px" runat="server" 
							Style="position: relative; top: 200px;">
							<div class="popup_Container">
								<div class="popup_Titlebar">
									<div class="TitlebarLeft">
										Message
									</div>
								</div>
								<div class="popup_Body">
									No renewal date entered.
								</div>
								<div class="popup_Buttons">
									
									<asp:Button ID="Button2" Text="Cancel" CssClass="button" OnClientClick="return HideModalPopup()" runat="server" />
								</div>
							</div>
						</asp:Panel>

    </div>

    <div id="loadingdivbackground" runat="server" class="DivCalendarBlock">
        <div id="loading-div" class="ui-corner-all">
            <img src="../images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>
</asp:Content>
