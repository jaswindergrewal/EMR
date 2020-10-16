<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_reseller.aspx.cs" Inherits="admin_reseller" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>
<%@ Register Namespace="CustomPopups" TagPrefix="custom" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function onDoubleClickCurrent(iRecordIndex) {
            if (grdReseller.RecordInEditMode == null) {
                grdReseller.editRecord(iRecordIndex);
                var rec = grdReseller.Rows[iRecordIndex];
            }
            else {
                grdReseller.cancel_edit();
            }

        }
        function grdReseller_ClientAdd() {
            var btnUpdate = document.getElementById('btnUpdate');
            var pnlContactTitle = document.getElementById('pnlContactTitle');
            var pnlContact = document.getElementById('pnlContact');
            var ddlCoManagement = document.getElementById('ddlCoManagement');
            pnlContact.style.display = "none";
            pnlContactTitle.style.display = "none";
            btnUpdate.value = "Add";
            //			ddlCoManagement.options[0].selected = true;
        }

        function grdReseller_ClientEdit(record) {
            var pnlContactTitle = document.getElementById('pnlContactTitle');
            var pnlContact = document.getElementById('pnlContact');
            pnlContact.style.display = "inline";
            pnlContactTitle.style.display = "inline";
            var ContactString = record.ContactString;
            var divContacts = document.getElementById('divContacts');
            divContacts.innerHTML = ContactString;
            var btnUpdate = document.getElementById('btnUpdate');
            var btnAddContact = document.getElementById('btnAddContact');
            btnAddContact.disabled = null;
            btnUpdate.value = "Update";
        }

        function grdReseller_PopulateControls(record, type) {
            //	if (type == "add") {
            //		// default values for add
            //		record.SalesRep = -1;
            //		record.CoManageAgreement = "False";
            //		record.ContractSigned = "False";
            //	} else {
            //		// apply changes to the values for the edit controls
            //	}

            //	return record;
        }

        function Restrictspecialchar(e) {
            var key;
            key = e.which ? e.which : e.keyCode;
            if ((key > 64 && key < 91) || (key > 96 && key < 123) || key == 8 || key == 32 || (key >= 48 && key <= 57))
                return true;
            else
                return false;

        }

        //Function to validate data before insertion
        function grdReseller_Validate() {

            var txtEmail = document.getElementById('txtEmail');
            var txtBusinessName = document.getElementById('txtBusinessName');
            var txtContactFirstName = document.getElementById('txtContactFirstName');
            var txtContactLastName = document.getElementById('txtContactLastName');
            var txtFirstName = document.getElementById('txtFirstName');
            var txtLastName = document.getElementById('txtLastName');
            var ddlAttendedDinner = document.getElementById('ddlAttendedDinner');
            var ddlEvent = document.getElementById('ddlEvent');
            var ddlSource = document.getElementById('ddlSource');
            var ddlLeadStatus = document.getElementById('ddlLeadStatus');
            var ddlSalesRep = document.getElementById('ddlSalesRep');
            var ddlStatus = document.getElementById('ddlStatus');
            var ddlCoManagement = document.getElementById('ddlCoManagement');
            var ddlContractSigned = document.getElementById('ddlContractSigned');
            var errorMessage = "";


            if (txtBusinessName.value == "") {
                errorMessage += "You must enter a Business Name.";
            }
            if (txtContactFirstName.value == "") {
                errorMessage += "\r\nYou must enter a Contact First Name.";
            }
            if (txtContactLastName.value == "") {
                errorMessage += "\r\nYou must enter a Contact Last Name.";
            }
            if (txtFirstName.value == "") {
                errorMessage += "\r\nYou must enter an Affiliate First Name.";
            }
            if (txtLastName.value == "") {
                errorMessage += "\r\nYou must enter an Affiliate Last Name.";
            }
            if (txtEmail.value == "") {
                errorMessage += "\r\nYou must enter an email address.";
            }
            if (txtEmail.value != "") {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                if (!filter.test(txtEmail.value)) {
                    errorMessage += "\r\nPlease provide a valid email address.";
                }
            }
            if (ddlSalesRep.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate sale reps.";
            }
            if (ddlStatus.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate status.";
            }
            if (ddlCoManagement.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate CoManagement.";
            }

            if (ddlAttendedDinner.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate if they attended a dinner.";
            }
            if (ddlEvent.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate if the event they attended.";
            }
            if (ddlContractSigned.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate ContractSigned or not.";
            }
            if (ddlSource.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate a marketing source.";
            }
            if (ddlLeadStatus.selectedIndex == -1) {
                errorMessage += "\r\nYou must indacate a lead status.";
            }

            if (errorMessage == "") {
                
                grdReseller.save();
            }
            else {
                alert(errorMessage);
                return false;
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="persist" style="background-color: Transparent; padding-left: 0px;">
        <p class="PageTitle">Affiliate Admin</p>
        <obout:Grid ID="grdReseller" runat="server" AllowFiltering="true"
            AllowSorting="true" AllowPageSizeSelection="true" PageSize="25"
            AutoGenerateColumns="false" FolderStyle="grid_styles/Style_7"
            OnUpdateCommand="grdReseller_UpdateInsert" OnInsertCommand="grdReseller_UpdateInsert"
            OnRebind="grdReseller_Rebind" OnRowDataBound="grdReseller_RowDataBound" EnableTypeValidation="false">
            <FilteringSettings FilterPosition="Top" />
            <Columns>
                <obout:Column DataField="ResellerID" Visible="false" HeaderText="ID" Width="75">
                    <TemplateSettings RowEditTemplateControlId="lblReselleriD" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ResellerNumber" HeaderText="ID" Width="75" Visible="false" />
                <obout:Column DataField="BusinessName" HeaderText="Business Name" Width="100">
                    <TemplateSettings RowEditTemplateControlId="txtBusinessName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ContactFirstName" HeaderText="Contact First Name" Width="120">
                    <TemplateSettings RowEditTemplateControlId="txtContactFirstName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ContactLastName" HeaderText="Contact Last Name" Width="120">
                    <TemplateSettings RowEditTemplateControlId="txtContactLastName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="FirstName" HeaderText="Affiliate First Name" Width="100">
                    <TemplateSettings RowEditTemplateControlId="txtFirstName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="LastName" HeaderText="Affiliate Last Name" Width="100">
                    <TemplateSettings RowEditTemplateControlId="txtLastName" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Phone" Width="100">
                    <TemplateSettings RowEditTemplateControlId="txtPhone" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Fax" Width="100" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtFax" RowEditTemplateControlPropertyName="value" />
                </obout:Column>

                <obout:Column DataField="Email" Width="100">
                    <TemplateSettings RowEditTemplateControlId="txtEmail" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Status" Width="100">
                </obout:Column>
                <obout:Column DataField="StatusID" Width="100" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlStatus" />
                </obout:Column>
                <obout:CheckBoxColumn DataField="AttendedDinner" HeaderText="Attended Dinner" Width="110"
                    AllowFilter="false">
                    <TemplateSettings RowEditTemplateControlId="ddlAttendedDinner" RowEditTemplateControlPropertyName="value" />
                </obout:CheckBoxColumn>
                <obout:Column DataField="StreetAddress" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtStreetAddress" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="City" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtCity" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="State" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtState" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Zip" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtZip" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Description" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtDescription" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="Notes" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtNotes" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="SalesRep" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlSalesRep" />
                </obout:Column>
                <obout:Column DataField="EventID" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlEvent" />
                </obout:Column>
                <obout:Column DataField="CoManageAgreement" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlCoManagement" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="DateEnrolled" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtDateEnrolled" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="CoManageDate" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtCoManageDate" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ContractSigned" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlContractSigned" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ContractDate" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="tdtContractDate" RowEditTemplateControlPropertyName="value" />
                </obout:Column>
                <obout:Column DataField="ResellerMarketingSourceID" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="ddlSource" />
                </obout:Column>
                <obout:Column DataField="LeadStatus" Visible="true" HeaderText="Lead Status" Width="100">
                    <TemplateSettings RowEditTemplateControlId="ddlLeadStatus" RowEditTemplateControlPropertyName="value" />
                </obout:Column>

                <obout:Column DataField="ContactString" Visible="false"></obout:Column>


            </Columns>
            <ClientSideEvents OnClientDblClick="onDoubleClickCurrent" ExposeSender="False" OnBeforeClientAdd="grdReseller_ClientAdd"
                OnBeforeClientEdit="grdReseller_ClientEdit" OnClientPopulateControls="grdReseller_PopulateControls" OnBeforeClientInsert="grdReseller_Validate"/>
            <TemplateSettings RowEditTemplateId="tplRowEdit" />

            <CssSettings CSSFolderImages="grid_styles/Style_7" />

            <Templates>
                <obout:GridTemplate runat="server" ID="tplRowEdit" ControlID="" ControlPropertyName="">
                    <Template>
                        <asp:Label ID="lblReselleriD" runat="server" ClientIDMode="Static" />
                        <table class="rowEditTable" cellpadding="6" cellspacing="6" width="100%">
                            <tr>
                                <td valign="top" style="width: 100px;" align="right">Business Name
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtBusinessName" runat="server" ClientIDMode="Static" onkeypress="return Restrictspecialchar(event)" BackColor="LightGray"
                                        TabIndex="1" /><font color="red">*</font>
                                </td>
                                <td valign="top" align="right">Contact Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactFirstName" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="2" onkeypress="return Restrictspecialchar(event)" />
                                    <asp:TextBox ID="txtContactLastName" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="3" onkeypress="return Restrictspecialchar(event)" /><font color="red">*</font>
                                </td>
                                <td rowspan="5" valign="top">Description:<br />
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="6" Columns="30"
                                        BackColor="LightGray" ClientIDMode="Static" TabIndex="19"></asp:TextBox><br />
                                    Notes:<br />
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="6" Columns="30"
                                        BackColor="LightGray" ClientIDMode="Static" TabIndex="20"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Affiliate Name
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="4" onkeypress="return Restrictspecialchar(event)" />
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="5" onkeypress="return Restrictspecialchar(event)" /><font color="red">*</font>
                                </td>
                                <td valign="top" align="right">Address
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtStreetAddress" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="6" /><br />
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" Columns="15" TabIndex="7" />,
									<asp:TextBox ID="txtState" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" Columns="2" TabIndex="8" />
                                    <asp:TextBox ID="txtZip" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" Columns="9" TabIndex="9" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Phone
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" Columns="10" TabIndex="10" />&nbsp;&nbsp;&nbsp;&nbsp;Fax&nbsp;
									<asp:TextBox ID="txtFax" runat="server" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" Columns="10" TabIndex="11" />
                                </td>
                                <td align="right" valign="top">Email
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtEmail" runat="server" Columns="20" CssClass="regText" ClientIDMode="Static"
                                        BackColor="LightGray" TabIndex="12" /><font color="red">*</font><br />
                                    <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="You must enter an ameil address" ForeColor="Red" Display="Dynamic"
                                        EnableClientScript="true" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Sales Rep
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlSalesRep" runat="server" DataTextField="EmployeeName"
                                        DataValueField="SalesRep" ClientIDMode="Static" TabIndex="13" />
                                    <font color="red">*</font>
                                </td>
                                <td align="right" valign="top">Status
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static" DataValueField="StatusID" DataTextField="Status" TabIndex="14" />
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Co Management<br />
                                    Aggreement?
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlCoManagement" runat="server" ClientIDMode="Static" TabIndex="16">
                                        <asp:ListItem Text="Yes" Value="True" />
                                        <asp:ListItem Text="No" Value="False" />
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                    <br />
                                    <asp:TextBox ID="txtCoManageDate" runat="server" ClientIDMode="Static" ReadOnly="true" />
                                    <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtCoManageDate"
                                        DatePickerImagePath="images/date_picker1.gif" />
                                </td>
                                <td align="right" valign="top">Attended Dinner
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlAttendedDinner" runat="server" ClientIDMode="Static" TabIndex="16">
                                        <asp:ListItem Text="Yes" Value="True" />
                                        <asp:ListItem Text="No" Value="False" />
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Event
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlEvent" runat="server" ClientIDMode="Static" TabIndex="17"
                                        DataTextField="EventName" DataValueField="EventID" />
                                    <font color="red">*</font>
                                </td>
                                <td align="right" valign="top">Date Enrolled
                                </td>
                                <td valign="top">
                                    <asp:TextBox runat="server" ID="txtDateEnrolled" ClientIDMode="Static" ReadOnly="true" onkeypress="return Restrictspecialchar(event)"></asp:TextBox>
                                    <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="txtDateEnrolled"
                                        DatePickerImagePath="images/date_picker1.gif">
                                    </obout:Calendar>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Affiliate contract signed
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlContractSigned" runat="server" ClientIDMode="Static" TabIndex="16">
                                        <asp:ListItem Text="Yes" Value="True" />
                                        <asp:ListItem Text="No" Value="False" />
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                    <br />
                                    <asp:TextBox ID="tdtContractDate" runat="server" ClientIDMode="Static" ReadOnly="true" />
                                    <obout:Calendar ID="Calendar3" runat="server" DatePickerMode="true" TextBoxId="tdtContractDate"
                                        DatePickerImagePath="images/date_picker1.gif" />
                                </td>
                                <td align="right" valign="top">Marketing Source
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlSource" runat="server" ClientIDMode="Static" TabIndex="17"
                                        DataTextField="SourceName" DataValueField="ResellerMarketingSourceID" />
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">Lead status
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlLeadStatus" runat="server" ClientIDMode="Static" TabIndex="16">
                                        <asp:ListItem Text="Red" Value="Red" />
                                        <asp:ListItem Text="Yellow" Value="Yellow" />
                                        <asp:ListItem Text="Green" Value="Green" />
                                        <asp:ListItem Text="None" Value="None" />
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                    <br />
                                </td>
                                <td align="right" valign="top">&nbsp;
                                </td>
                                <td valign="top">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <input type="button" id="btnUpdate" value="Update" class="button" onclick="grdReseller_Validate();" />
                                    <input type="button" id="btnCancel" value="Cancel" class="button" onclick="grdReseller.cancel();" />
                                </td>
                            </tr>
                        </table>
                        <cc1:CollapsiblePanelExtender ID="collContact" runat="server" TargetControlID="pnlContact"
                            ExpandControlID="pnlContactTitle" CollapseControlID="pnlContactTitle" TextLabelID="Label2"
                            CollapsedText="Click to manage contacts" ExpandedText="Click to hide contacts"
                            Collapsed="false" SuppressPostBack="True" ImageControlID="Image2" ExpandedImage="~/images/collapse.jpg"
                            CollapsedImage="~/images/expand.jpg" Enabled="True" ClientIDMode="Static">
                        </cc1:CollapsiblePanelExtender>
                        <asp:Panel ID="pnlContactTitle" runat="server" CssClass="border" BackColor="#D6B781"
                            ClientIDMode="Static">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0px"></asp:Image>
                            <asp:Label ID="Label2" runat="server" Text="New Prescription" CssClass="regText"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlContact" runat="server" ClientIDMode="Static">
                            <br />
                            <div id="divContacts" class="regText">
                            </div>
                        </asp:Panel>
                    </Template>
                </obout:GridTemplate>
            </Templates>
        </obout:Grid>
        <owd:Window ID="winAddContact" runat="server" IsModal="True" ShowCloseButton="True"
            Status="" RelativeElementID="txtBusinessName" Top="400" Left="100" Height="475" Width="750"
            VisibleOnLoad="False" StyleFolder="wdstyles/grandgray" Title="Add Affiliate Contact"
            DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL"
            IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose=""
            OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen=""
            OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" ShowMaximizeButton="False"
            ShowStatusBar="True">
            <div class="PageTitle">
                <asp:Button ID="btnAddContact" runat="server" Text="Add" CssClass="button" OnClientClick="winAddContact.Close(); window.setTimeout('grdReseller.refresh()',5000); return true;"
                    OnClick="btnAddContact_Click" />
                <input type="button" class="button" value="Cancel" onclick="winAddContact.Close();" />
                <br />
                <obout:Editor ID="ContactEd" runat="server" Height="300px" Width="700px" Content="">
                    <TopToolbar Appearance="Custom">
                        <AddButtons>
                            <obout:Bold />
                            <obout:Italic />
                            <obout:Underline />
                            <obout:StrikeThrough />
                            <obout:HorizontalSeparator />
                            <obout:FontName />
                            <obout:FontSize />
                            <obout:VerticalSeparator />
                            <obout:Undo />
                            <obout:Redo />
                            <obout:HorizontalSeparator />
                            <obout:PasteWord />
                            <obout:HorizontalSeparator />
                            <obout:JustifyLeft />
                            <obout:JustifyCenter />
                            <obout:JustifyRight />
                            <obout:JustifyFull />
                            <obout:HorizontalSeparator />
                            <obout:SpellCheck />
                            <custom:ImmediateImageInsert ID="btnImageInsert" runat="server" />
                        </AddButtons>
                    </TopToolbar>
                </obout:Editor>
                <br />
            </div>
        </owd:Window>
    </div>

</asp:Content>
