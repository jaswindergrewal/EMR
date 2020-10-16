<%@ Page Title="Manage CRM System" Language="C#" MasterPageFile="~/external/Site.master" AutoEventWireup="true"
    CodeFile="Manage.aspx.cs" Inherits="CRM_Manage" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register TagName="TDD" TagPrefix="Longevity" Src="~/controls/TimeDropDown.ascx" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/CRMManage.js"></script>
    <script type="text/javascript" src="../Scripts/Common.js"></script>

    <script src="../Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">



        $(function () {
            $('.txtMainPhone').mask("999-999-9999");
            $('.txtAltPhone').mask("999-999-9999");
            //$("#txtTime").setMask({ mask: "time", defaultValue: "hh:mm" });

        });
        $("document").ready(function () {
            $("#loading-div-background").show();



        });

        function CheckDateEalier(sender, args) {
            var txtEndDate = $('[id$=txtEndDateAppt]');
            txtEndDate.val(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))


        }
        //function to  mark the event record as attendend
        function RecordAttend1() {
            var eventID = $("[id*='ddlEvent'] :selected").val();
            var prospectID;
            debugger;
            for (var i = 0; i < grdAttend.SelectedRecords.length; i++) {
                var record = grdAttend.SelectedRecords[i];

                prospectID = record.ProspectID;
                var postData = new Object();
                postData.eventID = eventID;
                postData.prospectID = prospectID;
                $.ajax({
                    type: "POST",
                    url: "Manage.aspx/btnAttendedRecord",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (response) {
                        var res = response.d;

                        if (res == false) {
                            alert("Some problem occurred .");

                        }
                    },
                    error: function (obj) {

                        alert(obj.responseText);
                    }
                });
            }
            grdAttend.SelectedRecordsContainer.value = "";
            grdAttend.SelectedRecords = new Array();
            grdAttend.refresh();
            //return true;
        }

        //function to  delete the event record of attendend
        function DeleteRecordAttend() {
            var eventID = $("[id*='ddlEvent'] :selected").val();
            var prospectID;
            debugger;
            $("#grdAttend").trigger("reloadGrid");
            for (var i = 0; i < grdAttend.SelectedRecords.length; i++) {
                var record = grdAttend.SelectedRecords[i];
                grdAttend.deselectRecord[i];
                prospectID = record.ProspectID;
                var postData = new Object();
                postData.eventID = eventID;
                postData.prospectID = prospectID;
                $.ajax({
                    type: "POST",
                    url: "Manage.aspx/btnDeleteAttendedRecord",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (response) {
                        var res = response.d;

                        if (res == "Patient Exist") {
                            alert("Patient has been assigned to the prospect " + prospectID);
                        }
                    },
                    error: function (obj) {
                        alert(obj.responseText);
                    }
                });
            }
            debugger;
            grdAttend.SelectedRecordsContainer.value = "";
            grdAttend.SelectedRecords = new Array();
            grdAttend.refresh();

            //
            //Refreshing grid leads to duplicates record in next event where as reloading will delete all
            //grid schema data as well refreshed data.
            $("#grdAttend").trigger("reloadGrid");

            //return true;
        }


        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">
        Manage CRM System
    </p>

    <input type="hidden" id="StaffID" runat="server" clientidmode="Static" />
    <div class="CenterPB">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd">
            <ProgressTemplate>
                <div id="loading-div-background">
                    <div id="loading-div" class="ui-corner-all">
                        <img src="images/indicator.gif" alt="Loading.." />
                        <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
                    </div>
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="Autoship" runat="server" Width="100%" ActiveTabIndex="0" OnActiveTabChanged="Autoship_ActiveTabChanged" AutoPostBack="true"
                CssClass="lmc_tab">
                <cc1:TabPanel HeaderText="Manage Prospects" runat="server" ID="ManageProspects" CssClass="TabPanel">
                    <ContentTemplate>
                        <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete Prospects" OnClientClick="DeleteProspects(); return false;" />
                        <obout:Grid ID="grdProspect" runat="server" AllowFiltering="True" AutoGenerateColumns="False"
                            FolderStyle="grid_styles/style_5" OnInsertCommand="InsertUpdateProspect"
                            OnUpdateCommand="InsertUpdateProspect" EnableTypeValidation="False" OnDeleteCommand="grdProspect_Delete" OnRebind="grdProspect_Rebind">
                            <FilteringSettings FilterPosition="Top" />
                            <Columns>
                                <obout:CheckBoxSelectColumn AllowSorting="False" ShowHeaderCheckBox="False" HeaderText="Check to Delete"
                                    Width="110" Index="0" />
                                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="True" Width="120" Index="1">
                                </obout:Column>
                                <obout:Column ID="Column4" HeaderText="Delete" AllowDelete="True" Width="60" Index="5">
                                </obout:Column>
                                <obout:Column DataField="ProspectID" Visible="False" HeaderText="ProspectID" Index="10">
                                    <TemplateSettings RowEditTemplateControlId="lblProspectID" />
                                </obout:Column>
                                <obout:Column DataField="ProspectID" Visible="False" HeaderText="ProspectID" Index="11">
                                    <TemplateSettings RowEditTemplateControlId="txtID" />
                                </obout:Column>
                                <obout:Column DataField="FirstName" HeaderText="First Name" Width="120" Index="20">
                                    <TemplateSettings RowEditTemplateControlId="txtFirstName" />
                                </obout:Column>
                                <obout:Column DataField="LastName" HeaderText="Last Name" Width="120" Index="30">
                                    <TemplateSettings RowEditTemplateControlId="txtLastName" />
                                </obout:Column>
                                <obout:Column DataField="Address" HeaderText="Address" Width="100" Visible="False"
                                    Index="40">
                                    <TemplateSettings RowEditTemplateControlId="txtAddress" />
                                </obout:Column>
                                <obout:Column DataField="City" HeaderText="City" Width="100" Visible="False" Index="50">
                                    <TemplateSettings RowEditTemplateControlId="txtCity" />
                                </obout:Column>
                                <obout:Column DataField="State" HeaderText="State" Width="100" Visible="False" Index="60">
                                    <TemplateSettings RowEditTemplateControlId="ddlState" />
                                </obout:Column>
                                <obout:Column DataField="Zip" HeaderText="Zip" Width="80" Visible="False" Index="70">
                                    <TemplateSettings RowEditTemplateControlId="txtZip" />
                                </obout:Column>
                                <obout:Column DataField="MainPhone" HeaderText="Main Phone" Width="120" Index="80">
                                    <TemplateSettings RowEditTemplateControlId="txtMainPhone" />
                                </obout:Column>
                                <obout:Column DataField="AltPhone" HeaderText="Alt Phone" Width="120" Index="90">
                                    <TemplateSettings RowEditTemplateControlId="txtAltPhone" />
                                </obout:Column>
                                <obout:Column DataField="Email" HeaderText="Email" Width="200" Index="100">
                                    <TemplateSettings RowEditTemplateControlId="txtEmail" />
                                </obout:Column>
                                <obout:Column DataField="ContactMethod" HeaderText="Contact Method" Width="100" Visible="False"
                                    Index="110">
                                    <TemplateSettings RowEditTemplateControlId="txtContactMethod" />
                                </obout:Column>
                                <obout:Column DataField="StatusID" HeaderText="Status" Width="100" Visible="False"
                                    Index="120">
                                    <TemplateSettings RowEditTemplateControlId="ddlStatus" />
                                </obout:Column>
                                <obout:CheckBoxColumn DataField="Flagged" HeaderText="Flagged" Width="100" Visible="False"
                                    Index="130">
                                    <TemplateSettings RowEditTemplateControlId="cbFlagged" RowEditTemplateControlPropertyName="checked"
                                        RowEditTemplateUseQuotes="False" />
                                </obout:CheckBoxColumn>
                                <obout:Column DataField="MarketingSources" Visible="False" HeaderText="MarketingSources"
                                    Index="140">
                                    <TemplateSettings RowEditTemplateControlId="combo" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="Notes" Visible="False" HeaderText="Notes" Index="150">
                                    <TemplateSettings RowEditTemplateControlId="txtNotes" />
                                </obout:Column>
                                <obout:Column DataField="EventID" Visible="False" HeaderText="EventID" Index="151">
                                    <TemplateSettings RowEditTemplateControlId="cboEvents" />
                                </obout:Column>
                                <obout:Column DataField="EventName" HeaderText="Event" Index="160" Width="190" />
                                <obout:Column DataField="CreatedBy" HeaderText="Created by" Width="150" Index="161" Visible="false" />
                                <obout:Column DataField="StaffName" HeaderText="Created by" Width="150" Index="161" />
                            </Columns>
                            <TemplateSettings RowEditTemplateId="tplEditProspect" />
                            <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientUpdate="ValiDateManageProspect" OnBeforeClientInsert="ValiDateManageProspect" OnBeforeClientEdit="SelectMarketingResourceDropDown" />
                            <Templates>
                                <obout:GridTemplate ID="tplEditProspect" runat="server">
                                    <Template>
                                        <asp:Label ID="lblProspectID" runat="server" ClientIDMode="Static" />

                                        <table class="rowEditTable" cellpadding="6" cellspacing="6">
                                            <tr>
                                                <td colspan="2" style="display: none">
                                                    <asp:TextBox ID="txtID" runat="server" ClientIDMode="Static" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 100px">First Name<span style="color: red">*</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" BackColor="LightGray" onkeypress="return Restrictspecialchar(event)"
                                                        TabIndex="1" CssClass="regText" MaxLength="100" />
                                                </td>
                                                <td valign="top" align="right" style="width: 100px">Last Name<span style="color: red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="regText" ClientIDMode="Static" onkeypress="return Restrictspecialchar(event)"
                                                        BackColor="LightGray" TabIndex="2" MaxLength="100" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Address
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="3" MaxLength="200" />
                                                </td>
                                                <td valign="top" align="right">City
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="4" MaxLength="100" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">State
                                                </td>
                                                <td valign="top" nowrap="nowrap">
                                                    <asp:DropDownList runat="server" CssClass="FormField" ID="ddlState" BackColor="LightGray" ClientIDMode="Static" TabIndex="4">
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
                                                </td>
                                                <td align="right" valign="top">Zip
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="regText txtZip" ClientIDMode="Static" MaxLength="7"
                                                        BackColor="LightGray" TabIndex="6" Columns="6" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Main Phone<span style="color: red">*</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtMainPhone" runat="server" CssClass="regText txtMainPhone" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="7" />
                                                </td>
                                                <td valign="top" align="right">Alternate Phone
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtAltPhone" runat="server" CssClass="regText txtAltPhone" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="8" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Email
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="9" MaxLength="100" />
                                                </td>
                                                <td valign="top" align="right">Contact Method
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtContactMethod" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="10" MaxLength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Status<span style="color: red">*</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="StatusName"
                                                        DataValueField="StatusID" BackColor="LightGray" TabIndex="11" CssClass="FormField" ClientIDMode="Static" />

                                                </td>
                                                <td valign="top" align="right">Flagged
                                                </td>
                                                <td valign="top">
                                                    <asp:CheckBox ID="cbFlagged" runat="server" Text="" BackColor="LightGray" TabIndex="12" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Marketing Sources
                                                </td>
                                                <td valign="top">

                                                    <obout:ComboBox ID="combo" runat="server" EmptyText="Select multiple sources ..."
                                                        DataTextField="MarketingSourceName" DataValueField="MarketingSourceID"
                                                        ClientIDMode="Static" SelectionMode="Multiple" TabIndex="13" />
                                                </td>
                                                <td valign="top" align="right">Notes
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtNotes" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TextMode="MultiLine" Rows="4" Columns="20" TabIndex="15" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Event<span style="color: red">*</span>

                                                </td>
                                                <td valign="top">

                                                    <asp:DropDownList ID="cboEvents" runat="server" DataTextField="EventName"
                                                        DataValueField="EventID" BackColor="LightGray" TabIndex="14" CssClass="FormField" ClientIDMode="Static" />

                                                </td>
                                                <td valign="top" align="right">&nbsp;
                                                </td>
                                                <td valign="top">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                        </obout:Grid>
                    </ContentTemplate>
                </cc1:TabPanel>

                <cc1:TabPanel HeaderText="Status and Sources" runat="server" ID="StatusSources" CssClass="TabPanel">
                    <ContentTemplate>
                        <table cellpadding="6" cellspacing="6">
                            <tr>
                                <td class="PageTitle">Status Management
                                </td>
                                <td class="PageTitle">Marketing Source Management
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <obout:Grid ID="grdStatus" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                                        AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                                        AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5"
                                        OnUpdateCommand="grdStatus_UpdateInsert" OnInsertCommand="grdStatus_UpdateInsert"
                                        OnRebind="grdStatus_Rebind" EnableTypeValidation="false" OnDeleteCommand="DeleteStatusMgmt">
                                        <Columns>

                                            <obout:Column DataField="StatusID" Visible="false" />
                                            <obout:Column DataField="StatusName" HeaderText="Status Name">
                                                <TemplateSettings RowEditTemplateControlId="StatusName" />
                                            </obout:Column>

                                            <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                                            <obout:Column ID="Column2" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />

                                        </Columns>


                                        <ClientSideEvents OnBeforeClientUpdate="ValiDateStatus" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientInsert="ValiDateStatus" OnBeforeClientDelete="ConfirmDelete" />
                                    </obout:Grid>
                                </td>
                                <td valign="top">
                                    <obout:Grid ID="grdMSource" runat="server" AllowAddingRecords="true" AllowFiltering="false"
                                        AllowPageSizeSelection="false" AllowPaging="false" PageSize="-1" AllowSorting="true"
                                        AutoGenerateColumns="false" CallbackMode="true" FolderStyle="grid_styles/Style_5"
                                        OnUpdateCommand="grdMSource_UpdateInsert" OnInsertCommand="grdMSource_UpdateInsert" OnDeleteCommand="DeleteMarketingSourceMgmt"
                                        OnRebind="grdMSource_Rebind" EnableTypeValidation="false">
                                        <Columns>
                                            <obout:Column DataField="MarketingSourceID" Visible="false" />
                                            <obout:Column DataField="MarketingSourceName" HeaderText="Marketing Source Name" />
                                            <obout:CheckBoxColumn DataField="Active_YN" HeaderText="Active" />
                                            <obout:Column ID="Column3" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
                                        </Columns>
                                        <ClientSideEvents OnBeforeClientInsert="ValiDateMarketingSource" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientUpdate="ValiDateMarketingSource" OnBeforeClientDelete="ConfirmDelete" />
                                    </obout:Grid>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>

                <cc1:TabPanel HeaderText="Campaigns and Events" runat="server" ID="CampaignsEvents"
                    CssClass="TabPanel">
                    <ContentTemplate>
                        <obout:Grid runat="server" ID="grdCampaign" AutoGenerateColumns="false" PageSize="5" Serialize="true"
                            FolderStyle="grid_styles/style_5" AllowAddingRecords="true" OnInsertCommand="InsertUpdateCampaign" EnableTypeValidation="False" CallbackMode="true"
                            OnUpdateCommand="InsertUpdateCampaign" OnRebind="grdCampaign_Rebind" AllowMultiRecordSelection="false" OnDeleteCommand="grdCampaign_Delete" OnRowDataBound="grdCampaign_RowDataBound">
                            <Columns>
                                <obout:Column DataField="CampaignID" Visible="false" HeaderText="Campaign ID" ReadOnly="true">
                                    <TemplateSettings RowEditTemplateControlId="lblCampaignID" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="CampaignName" HeaderText="Campaign Name">
                                    <TemplateSettings RowEditTemplateControlId="txtCampaignName" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="MarketingBudget" HeaderText="Total Expense" ApplyFormatInEditMode="true" DataFormatString="{0:C}">
                                    <TemplateSettings RowEditTemplateControlId="txtMarketingBudget" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="CampaignTypeID" Visible="false" HeaderText="CampaignTypeID" Index="151">
                                    <TemplateSettings RowEditTemplateControlId="cboCampaignType" />
                                </obout:Column>
                                <obout:Column DataField="CampaignType" HeaderText="Campaign Type" Index="160" Width="151" />
                                <%--   <obout:Column DataField="CampaignType" HeaderText="Campaign Type">
                            <TemplateSettings RowEditTemplateControlId="txtCampaignType" RowEditTemplateControlPropertyName="value" />
                        </obout:Column>--%>
                                <obout:Column DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}"
                                    Width="100" ApplyFormatInEditMode="true">
                                    <TemplateSettings RowEditTemplateControlId="txtStartDate" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}"
                                    Width="100" ApplyFormatInEditMode="true">
                                    <TemplateSettings RowEditTemplateControlId="txtEndDate" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="MarketingSources" Visible="false">
                                    <TemplateSettings RowEditTemplateControlId="cboSources" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="" HeaderText="" AllowEdit="true" AllowDelete="true">
                                </obout:Column>

                                <obout:Column DataField="Enabled" HeaderText="Enabled Type" Visible="false" />
                                <%-- <obout:Column HeaderText="" Width="10%" Index="2">
                            <TemplateSettings TemplateId="tblActivate" />
                        </obout:Column>--%>
                            </Columns>

                            <TemplateSettings RowEditTemplateId="tplEditCampaign" />
                            <ClientSideEvents OnBeforeClientInsert="ValiDateCampaign" OnBeforeClientCancelEdit="SelectMarketingResourceDropDownTab2" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientUpdate="ValiDateCampaign" OnBeforeClientDelete="Active_DeactiveCampaign" OnBeforeClientEdit="SelectMarketingResourceDropDownTab2" />
                            <LocalizationSettings DeleteLink="Deactivate" />
                            <Templates>
                                <obout:GridTemplate ID="tplEditCampaign" runat="server" ControlID="" ControlPropertyName="">
                                    <Template>
                                        <asp:Label ID="lblCampaignID" runat="server" ClientIDMode="Static" Style="display: none" />
                                        <table class="rowEditTable" cellpadding="6" cellspacing="6">
                                            <tr>
                                                <td valign="top" align="right" style="width: 100px">Campaign Name
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtCampaignName" runat="server" ClientIDMode="Static" BackColor="LightGray"
                                                        TabIndex="1" CssClass="regText" />
                                                </td>
                                                <td valign="top" align="right" style="width: 100px">Total Expense
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarketingBudget" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="2" onkeypress="return check_digit(event,this,8,2);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Campaign Type
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="cboCampaignType" runat="server" DataTextField="CampaignType"
                                                        DataValueField="CampaignID" BackColor="LightGray" TabIndex="14" CssClass="FormField" ClientIDMode="Static" />

                                                    <%-- <asp:TextBox ID="txtCampaignType" runat="server" CssClass="regText" ClientIDMode="Static"
                                                BackColor="LightGray" TabIndex="4" />--%>
                                                </td>
                                                <td valign="top" align="right">Start Date
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="6" ReadOnly="true" />
                                                    <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="txtStartDate"
                                                        DatePickerImagePath="images/date_picker1.gif">
                                                    </obout:Calendar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">End Date
                                                </td>
                                                <td valign="top" nowrap="nowrap">
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="10" ReadOnly="true" />
                                                    <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtEndDate"
                                                        DatePickerImagePath="images/date_picker1.gif" />
                                                </td>
                                                <td align="right" valign="top">Marketing Sources
                                                </td>
                                                <td valign="top">
                                                    <obout:ComboBox ID="cboSources" runat="server" EmptyText="Select multiple sources ..."
                                                        DataTextField="MarketingSourceName" DataValueField="MarketingSourceID"
                                                        Mode="ComboBox" SelectionMode="Multiple" BackColor="LightGray" AllowEdit="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Template>

                                </obout:GridTemplate>



                                <%--  <obout:GridTemplate ID="tblActivate">
                            <Template>
                                <asp:HyperLink ClientIDMode="Static" CommandName="Delete" runat= "server" Text="Activate" ID="lnkGrdCampaignActive">Active</asp:HyperLink>
                            </Template>
                        </obout:GridTemplate>--%>
                            </Templates>

                            <MasterDetailSettings LoadingMode="OnCallback" />
                            <DetailGrids>
                                <obout:DetailGrid runat="server" ID="grdEvent" AutoGenerateColumns="false" AllowAddingRecords="true"
                                    ShowFooter="true" PageSize="5" FolderStyle="grid_styles/style_5" ForeignKeys="CampaignID"
                                    OnInsertCommand="InsertUpdateEvent" OnUpdateCommand="InsertUpdateEvent" DataSourceID="sqlEvent"
                                    CSSFolderImages="resources/custom-styles/new" ClientIDMode="Static" AllowMultiRecordSelection="false"
                                    EnableTypeValidation="false" OnDeleteCommand="grdEvent_Delete" OnRowDataBound="grdEvent_RowDataBound">
                                    <Columns>
                                        <obout:Column DataField="CampaignID" ReadOnly="false" Visible="false"></obout:Column>
                                        <obout:Column DataField="CampaignID" ReadOnly="false" Visible="false" HeaderText="Campaign ID">
                                            <TemplateSettings RowEditTemplateControlId="lblCampaignID" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="EventID" Visible="false" HeaderText="Event ID" ReadOnly="true">
                                            <TemplateSettings RowEditTemplateControlId="lblEventID" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="EventName" HeaderText="Event Name" Width="300">
                                            <TemplateSettings RowEditTemplateControlId="txtEventName" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="EventDate" HeaderText="Event Date" DataFormatString="{0:MM/dd/yyyy}"
                                            Width="100" ApplyFormatInEditMode="true">
                                            <TemplateSettings RowEditTemplateControlId="txtEventDate" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Venue" HeaderText="Venue" Width="150">
                                            <TemplateSettings RowEditTemplateControlId="txtVenue" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Notes" Visible="false" HeaderText="Notes" Width="300">
                                            <TemplateSettings RowEditTemplateControlId="txtNotes" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Location" Visible="true" Width="100">
                                            <TemplateSettings RowEditTemplateControlId="txtLocation" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="EventLength" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="txtEventLength" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Walkins" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="txtWalkins" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Appointments" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="txtAppointments" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Callbacks" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="txtCallbacks" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="OverallPerformance" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlOverallPerformance" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="FacilityInteriorExterior" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlFacilityInteriorExterior" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="VenueQuality" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlVenueQuality" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="Parking" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlParking" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="AudienceReaction" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlAudienceReaction" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="AudienceQuality" Visible="false">
                                            <TemplateSettings RowEditTemplateControlId="ddlAudienceQuality" RowEditTemplateControlPropertyName="value" />
                                        </obout:Column>
                                        <obout:Column DataField="" HeaderText="" AllowEdit="true" AllowDelete="true" Align="center"
                                            Width="125">
                                        </obout:Column>
                                        <obout:Column DataField="Enabled" HeaderText="Enabled Type" Visible="false" />
                                    </Columns>
                                    <ClientSideEvents OnBeforeClientInsert="ValidateEvent" ExposeSender="true" OnClientPopulateControls="onPopulateControls_grdEvent" OnClientInsert="OnInsert" OnClientUpdate="OnUpdate" OnBeforeClientUpdate="ValidateEvent" OnBeforeClientDelete="ConfirmDelete" />
                                    <TemplateSettings RowEditTemplateId="tplEdit" />
                                    <LocalizationSettings DeleteLink="Deactivate" />
                                    <Templates>
                                        <obout:GridTemplate ID="tplEdit" runat="server" ControlID="" ControlPropertyName="">
                                            <Template>
                                                <asp:Label ID="lblCampaignID" runat="server" ClientIDMode="Static" Style="display: block" />
                                                <asp:Label ID="lblEventID" runat="server" ClientIDMode="Static" Style="display: block" />
                                                <table class="rowEditTable" cellpadding="6" cellspacing="6">
                                                    <tr>
                                                        <td valign="top" align="right" style="width: 100px">Event Name
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtEventName" runat="server" ClientIDMode="Static" BackColor="LightGray"
                                                                TabIndex="1" CssClass="regText" /><font color="red">*</font>
                                                        </td>
                                                        <td valign="top" align="right" style="width: 100px">Event Date and Time<br />
                                                            (mm/dd/yy hh:mm AM or PM)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEventDate" runat="server" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="2" ReadOnly="false" /><font color="red">*</font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Venue
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtVenue" runat="server" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="4" /><font color="red">*</font>
                                                        </td>
                                                        <td valign="top" align="right">Location (city)
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="6" /><br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Walkins
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtWalkins" runat="server" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" Columns="5" TabIndex="10" />
                                                        </td>
                                                        <td align="right" valign="top">Appointments
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtAppointments" runat="server" Columns="5" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="12" onkeypress="return check_digit(event,this,8,0);" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Call Backs
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtCallbacks" runat="server" Columns="5" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="12" onkeypress="return check_digit(event,this,8,0);" />
                                                        </td>
                                                        <td align="right" valign="top">Overall performance
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlOverallPerformance" runat="server" ClientIDMode="Static"
                                                                BackColor="LightGray" TabIndex="16">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Quality of Facility Interior/Exterior
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlFacilityInteriorExterior" runat="server" ClientIDMode="Static"
                                                                TabIndex="16" BackColor="LightGray">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" valign="top">Venue Location Quality
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlVenueQuality" runat="server" ClientIDMode="Static" TabIndex="16"
                                                                BackColor="LightGray">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Parking at Venue
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlParking" runat="server" ClientIDMode="Static" TabIndex="16"
                                                                BackColor="LightGray">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" valign="top">Audience Reaction
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlAudienceReaction" runat="server" ClientIDMode="Static" TabIndex="16"
                                                                BackColor="LightGray">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">Overall Audience Quality
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlAudienceQuality" runat="server" ClientIDMode="Static" TabIndex="16"
                                                                BackColor="LightGray">
                                                                <asp:ListItem Text="Poor" Value="1" />
                                                                <asp:ListItem Text="Fair" Value="2" />
                                                                <asp:ListItem Text="Excellent" Value="3" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" valign="top">Event Length
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtEventLength" runat="server" CssClass="regText" ClientIDMode="Static"
                                                                BackColor="LightGray" Columns="5" TabIndex="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">Notes<br />
                                                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="80"
                                                                BackColor="LightGray" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Template>
                                        </obout:GridTemplate>
                                    </Templates>
                                </obout:DetailGrid>
                            </DetailGrids>
                        </obout:Grid>

                        <asp:SqlDataSource runat="server" ID="sqlEvent" SelectCommand="SELECT * FROM [CRM_Events] WHERE ([CampaignID] = @CampaignID) "
                            ConnectionString='<%$ ConnectionStrings:db %>'>
                            <SelectParameters>
                                <asp:Parameter Name="CampaignID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </ContentTemplate>
                </cc1:TabPanel>

                <cc1:TabPanel ID="Activity" runat="server" HeaderText="Marketing Activity">
                    <ContentTemplate>
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <p class="regText">
                            Campaign:
							<asp:DropDownList ID="ddlCampaign" runat="server" DataTextField="CampaignName"
                                DataValueField="CampaignID" ClientIDMode="Static" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged" AutoPostBack="true" />
                        </p>
                        <obout:Grid ID="grdMarketingActivity" runat="server" AutoGenerateColumns="false" PageSize="-1" ShowFooter="true"
                            AllowAddingRecords="true" FolderStyle="grid_styles/style_5" AllowPageSizeSelection="false"
                            OnInsertCommand="InsertUpdateActivity" OnUpdateCommand="InsertUpdateActivity"
                            CSSFolderImages="resources/custom-styles/new" ClientIDMode="Static" AllowMultiRecordSelection="false" AllowFiltering="false"
                            EnableTypeValidation="false" ShowLoadingMessage="false" OnRebind="grdMarketingActivity_Rebind">
                            <Columns>
                                <obout:Column DataField="" HeaderText="" AllowEdit="true" AllowDelete="false" Align="center"
                                    Width="135">
                                </obout:Column>
                                <obout:Column DataField="MarketingActivityID" Visible="false">
                                    <TemplateSettings RowEditTemplateControlId="lblMarketingActivityID" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="CampaignID" Visible="false">
                                    <TemplateSettings RowEditTemplateControlId="lblCampaignID1" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="SourceType" HeaderText="Source Type">
                                    <TemplateSettings RowEditTemplateControlId="txtSourceType" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}">
                                    <TemplateSettings RowEditTemplateControlId="txtStartDate1" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}">
                                    <TemplateSettings RowEditTemplateControlId="txtEndDate1" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="Notes" Visible="false">
                                    <TemplateSettings RowEditTemplateControlId="txtActivityNotes1" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="SourceID" Visible="false">
                                    <TemplateSettings RowEditTemplateControlId="ddlSource1" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                                <obout:Column DataField="Money_Spent" HeaderText="Money Spent" DataFormatString="{0:C2}">
                                    <TemplateSettings RowEditTemplateControlId="txtMoneySpent" RowEditTemplateControlPropertyName="value" />
                                </obout:Column>
                            </Columns>
                            <TemplateSettings RowEditTemplateId="tplEditActivity" />
                            <ClientSideEvents OnBeforeClientInsert="ValiDateActivity" OnBeforeClientUpdate="ValiDateActivity" OnBeforeClientAdd="CheckForCampaign" />
                            <LocalizationSettings LoadingText="Loading Activity" />
                            <Templates>
                                <obout:GridTemplate ID="tplEditActivity" runat="server">
                                    <Template>
                                        <asp:Label ID="lblMarketingActivityID" runat="server" ClientIDMode="Static" />
                                        <asp:Label ID="lblCampaignID1" runat="server" ClientIDMode="Static" />
                                        <table class="rowEditTable" cellpadding="6" cellspacing="6">
                                            <tr>
                                                <td valign="top" align="right" style="width: 100px">Source Type
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtSourceType" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="1" />
                                                </td>
                                                <td valign="top" align="right" style="width: 100px">Start Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate1" runat="server" CssClass="regText txtStartDate1" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="2" ReadOnly="true" />
                                                    <obout:Calendar ID="Calendar3" runat="server" DatePickerMode="true" TextBoxId="txtStartDate1"
                                                        DatePickerImagePath="images/date_picker1.gif" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">End Date
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtEndDate1" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="3" ReadOnly="true" />
                                                    <obout:Calendar ID="Calendar4" runat="server" DatePickerMode="true" TextBoxId="txtEndDate1"
                                                        DatePickerImagePath="images/date_picker1.gif" />
                                                </td>
                                                <td valign="top" align="right">Notes
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtActivityNotes1" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" Rows="5" Columns="7" Width="410px" Height="80px" TextMode="MultiLine" TabIndex="4" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">Money Spent
                                                </td>
                                                <td valign="top" nowrap="nowrap">
                                                    <asp:TextBox ID="txtMoneySpent" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="6" Columns="10" />
                                                </td>
                                                <td align="right" valign="top">Source
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlSource1" runat="server" DataTextField="MarketingSourceName"
                                                        DataValueField="MarketingSourceID" ClientIDMode="Static" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                        </obout:Grid>

                    </ContentTemplate>
                    <%--  </asp:UpdatePanel>
            </ContentTemplate>--%>
                </cc1:TabPanel>

                <cc1:TabPanel ID="Seminars" runat="server" HeaderText="Events">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr >
                                <td style="width:50%" >Event:
					<asp:DropDownList ID="ddlEvent" runat="server" DataTextField="EventName"
                        DataValueField="EventID" ClientIDMode="Static" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" AutoPostBack="true" />
                               </td>
                                <td style="font-size:15px"><asp:Label ID="lblEventName" runat="server"></asp:Label></td>    
                            </tr>
                          <tr><td> Clinic:
					<asp:DropDownList ID="ddlClinic" runat="server" DataTextField="ClinicName"
                        DataValueField="ClinicID" ClientIDMode="Static" /></td></tr>
                        </table>
                         
                        <span id="WindowPositionHelper"></span>
                        <asp:Button ID="btnAttended" runat="server" CssClass="button" Text="Record Attendees" OnClientClick="RecordAttend1(); return false;" />

                        <asp:Button ID="btnDeleteAtten" runat="server" CssClass="button" Text="Delete Attendees" OnClientClick="DeleteRecordAttend(); return false;" />

                        <asp:Button ID="btnAddProspect" runat="server" CssClass="button" Text="Add Prospect" OnClientClick="AddProspect(); return false;" />

                        <obout:Grid ID="grdAttend" runat="server" OnRebind="grdAttend_Rebind" Width="100%"
                            OnSelect="grdAttend_Select" AutoGenerateColumns="False" AllowAddingRecords="False" FolderStyle="grid_styles/Style_7"
                            AllowFiltering="false" ShowFooter="true" PageSize="-1" AllowPageSizeSelection="false" CallbackMode="true">
                            <Columns>
                                <obout:CheckBoxSelectColumn AllowSorting="False" ShowHeaderCheckBox="False" HeaderText="Check if Attended"
                                    Width="140" Index="0" />
                                <obout:Column HeaderText="Attended" DataField="Attended" AllowSorting="False" Width="85"
                                    ReadOnly="True" Index="1" />


                                <obout:Column HeaderText="Followup" Width="10%" Index="2">
                                    <TemplateSettings TemplateId="templateFollowup" />
                                </obout:Column>
                                <obout:Column HeaderText="Schedule" Width="10%" Index="3">
                                    <TemplateSettings TemplateId="templateSchedule" />
                                </obout:Column>
                                <obout:Column HeaderText="Create" Width="10%" Index="4">
                                    <TemplateSettings TemplateId="templateCreate" />
                                </obout:Column>
                                <obout:Column DataField="FirstName" HeaderText="First Name" ReadOnly="True" Width="130" Index="5" />
                                <obout:Column DataField="LastName" HeaderText="Last Name" ReadOnly="True" Width="130" Index="6" />
                                <obout:Column DataField="Email" HeaderText="Email" Width="150" ReadOnly="True" Index="7" />
                                <obout:Column DataField="MainPhone" AccessibleHeaderText="Phone" ReadOnly="True"
                                    Width="100" HeaderText="MainPhone" Index="8" />
                                <obout:Column DataField="StatusName" AccessibleHeaderText="Status" ReadOnly="True"
                                    Width="100" HeaderText="Status" Index="9" />
                                <obout:Column DataField="ProspectID" Visible="False" HeaderText="ProspectID" Index="10" />
                                <obout:Column ID="Column5" DataField="Appointments" HeaderText="Appointment" Width="245"
                                    ReadOnly="True" Index="11">
                                </obout:Column>
                            </Columns>

                            <Templates>

                                <obout:GridTemplate ID="templateFollowup">
                                    <Template>
                                        <a href="#" onclick="grdAttend_Followup(<%# Container.DataItem["ProspectID"].ToString()%>);">Followup</a>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate ID="templateSchedule">
                                    <Template>
                                        <a href="#" onclick="grdAttend_ClientEdit(<%# Container.DataItem["ProspectID"].ToString()%>);">Schedule</a>
                                    </Template>
                                </obout:GridTemplate>

                                <obout:GridTemplate ID="templateCreate">
                                    <Template>
                                        <a href="#" onclick="grdAttenendCreateappointment(<%# Container.DataItem["ProspectID"].ToString()%>);">Create</a>
                                    </Template>
                                </obout:GridTemplate>


                            </Templates>
                            <LocalizationSettings NoRecordsText="No Registrants Found!" />

                        </obout:Grid>
                        <owd:Window ID="skedWindow" runat="server" IsModal="True" ShowCloseButton="True"
                            Status="" Top="200" Left="100" Height="200" Width="400" VisibleOnLoad="False"
                            StyleFolder="../wdstyles/grandgray" Title="Schedule Appointment" DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL" IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose="" OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen="" OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" RelativeElementID="" ShowMaximizeButton="False" ShowStatusBar="True">
                            <input type="hidden" id="ProspectID" value="" />
                            <asp:DropDownList ID="ddlAppts" runat="server" DataTextField="ApptText" DataValueField="apt_id"
                                CssClass="FormField" ClientIDMode="Static" />
                            <br />
                            <br />
                            <obout:OboutButton ID="OboutButton1" runat="server" Text="Save" OnClientClick="saveChanges(); return false;"
                                Width="75px" FolderStyle="" />

                            <obout:OboutButton ID="OboutButton2" runat="server" Text="Cancel" OnClientClick="cancelChanges(); return false;"
                                Width="75px" FolderStyle="" />

                        </owd:Window>

                        <owd:Window ID="AppointmentWindow" runat="server" IsModal="True" ShowCloseButton="True"
                            Status="" Top="200" Left="100" Height="300" Width="500" VisibleOnLoad="False"
                            StyleFolder="../wdstyles/grandgray" Title="Create Appointment" DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL" IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose="" OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen="" OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" RelativeElementID="" ShowMaximizeButton="False" ShowStatusBar="True">
                            <br />
                            <table style="margin-left: 10px; margin-right: 10px;" width="450px" height="250px">
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Provider:</td>
                                    <td>
                                        <asp:DropDownList ID="drpProviders" runat="server" CssClass="FormField" ClientIDMode="Static"
                                            DataTextField="ProviderName" DataValueField="id" TabIndex="1" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Start Date:</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtStartDateAppt" Columns="12" TabIndex="2" CssClass="FormField" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDateAppt"
                                            OnClientDateSelectionChanged="CheckDateEalier" />
                                    </td>
                                    <td>Start Time:</td>
                                    <td>
                                        <asp:DropDownList ID="drpStartTime" runat="server" CssClass="FormField" ClientIDMode="Static"
                                            TabIndex="3" Width="120px" onclick="setEndTime();" />
                                        <%--<asp:TextBox ID="txtStartTime" runat="server" ClientIDMode="Static"  Columns="12" CssClass="FormField" TabIndex="3"></asp:TextBox>

                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" SkinID="maskedTimeEditSkin" TargetControlID="txtStartTime"
                                    ClearMaskOnLostFocus="true" Mask="99:99" MaskType="Time"
                                    AcceptAMPM="true" MessageValidatorTip="true" OnFocusCssClass="maskedEditFocus"
                                    OnInvalidCssClass="maskedEditError" />--%>

                                    </td>
                                </tr>
                                <tr>
                                    <td>End Date:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtEndDateAppt" Columns="12" TabIndex="4" CssClass="FormField" />
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndDateAppt" />
                                    </td>
                                    <td>End Time:</td>
                                    <td>
                                        <asp:DropDownList ID="drpEndTime" runat="server" CssClass="FormField" ClientIDMode="Static"
                                            TabIndex="5" Width="120px" />

                                        <%-- <asp:TextBox ID="txtEndTime" runat="server" ClientIDMode="Static" Columns="12" CssClass="FormField" TabIndex="5" ></asp:TextBox>
 onclick="this.size=1;" onMouseOver="this.size=10;" onMouseOut="this.size=1;" onfocus="this.size=10;" style="position:absolute;"
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" SkinID="maskedTimeEditSkin" TargetControlID="txtEndTime"
                                    ClearMaskOnLostFocus="true" Mask="99:99" MaskType="Time"
                                    AcceptAMPM="true" MessageValidatorTip="true" OnFocusCssClass="maskedEditFocus"
                                    OnInvalidCssClass="maskedEditError" />--%>


                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <obout:OboutButton ID="OboutButton3" runat="server" Text="Save" TabIndex="6" OnClientClick="saveChangesforAppointment(); return false;"
                                            Width="75px" FolderStyle="" />
                                        <obout:OboutButton ID="OboutButton4" runat="server" Text="Cancel" TabIndex="7" OnClientClick="cancelChangesAppointment(); return false;"
                                            Width="75px" FolderStyle="" /></td>
                                </tr>

                            </table>




                        </owd:Window>

                        <owd:Window ID="AddProspectWindow" runat="server" IsModal="True" ShowCloseButton="True"
                            Status="" Top="200" Left="100" Height="300" Width="500" VisibleOnLoad="False"
                            StyleFolder="../wdstyles/grandgray" Title="Add Prospect" DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL" IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose="" OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen="" OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" RelativeElementID="" ShowMaximizeButton="False" ShowStatusBar="True">
                            <br />
                            <asp:HiddenField ID="hdnProspectEventId" runat="server" ClientIDMode="Static" />
                            <table style="margin-left: 10px; margin-right: 10px;" width="450px" height="250px">
                                                                       
                                            <tr>
                                                <td colspan="2"><asp:Label ID="lblProspectEventName"  runat="server" ClientIDMode="Static"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 100px">First Name<span style="color: red">*</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtProspectFirstName" runat="server" ClientIDMode="Static" BackColor="LightGray" onkeypress="return Restrictspecialchar(event)"
                                                        TabIndex="1" CssClass="regText" MaxLength="100" />
                                                </td>
                                                <td valign="top" align="right" style="width: 100px">Last Name<span style="color: red">*</span>
                                                </td>
                                                <td valign="top"> 
                                                    <asp:TextBox ID="txtProspectLastName" runat="server" CssClass="regText" ClientIDMode="Static" onkeypress="return Restrictspecialchar(event)"
                                                        BackColor="LightGray" TabIndex="2" MaxLength="100" />
                                                </td>
                                            </tr>
                                           
                                           
                                            <tr>
                                                <td valign="top" align="right">Main Phone<span style="color: red">*</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtProspectMainPhone" runat="server" CssClass="regText txtMainPhone" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="7" />
                                                </td>
                                               <td valign="top" align="right">Email
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtProspectEmail" runat="server" CssClass="regText" ClientIDMode="Static"
                                                        BackColor="LightGray" TabIndex="9" MaxLength="100" />
                                                </td>
                                            </tr>
                                                                                   

                                <tr>
                                    <td colspan="2" valign="top">
                                        <obout:OboutButton ID="OboutButton5" runat="server" Text="Save" TabIndex="6" OnClientClick="saveProspect(); return false;"
                                            Width="75px" FolderStyle="" />
                                        <obout:OboutButton ID="OboutButton6" runat="server" Text="Cancel" TabIndex="7" OnClientClick="cancelProspect(); return false;"
                                            Width="75px" FolderStyle="" /></td>
                                </tr>

                            </table>




                        </owd:Window>

                    </ContentTemplate>
                </cc1:TabPanel>

                <cc1:TabPanel HeaderText="Survey" runat="server" ID="Survey" CssClass="TabPanel">
                    <ContentTemplate>
                        <table cellpadding="6" cellspacing="6">
                            <tr>
                                <td class="PageTitle">Survey Management
                                </td>

                            </tr>
                            <tr>
                                <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
                                    <tr bgcolor="#D6B781">
                                        <td colspan="3">
                                            <p><b>Email Template</b></p>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Wufoo Form Link:<asp:TextBox ID="txtCRMWufooFormLink" runat="server"></asp:TextBox></td>
                                        <td>IsActive:<asp:CheckBox ID="chkIsActive" runat="server" /></td>
                                        <td>
                                            <asp:Button ID="btnSumit" runat="server" Text="Submit" CssClass="button" OnClick="btnSumit_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">Choose the common URL that need to set in a form</td>

                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btnUserName" runat="server" Text="UserName" OnClick="btnUserName_Click" CssClass="button" />&nbsp;<asp:Button ID="btnURL" runat="server" Text="URL" CssClass="button" OnClick="btnURL_Click" /></td>

                                    </tr>


                                </table>

                                <table width="600" height="40%" border="0" cellpadding="0" cellspacing="0" class="border">

                                    <tr valign="top" align="left">
                                        <td valign="top">
                                            <table width="100%" border="0">
                                                <tr valign="top">
                                                    <td width="100%">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr valign="top">
                                                                <td bgcolor="#FFFFFF">
                                                                    <obout:Editor ID="edContent" runat="server" Height="300px" Width="600px">
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

                                                                            </AddButtons>
                                                                        </TopToolbar>
                                                                    </obout:Editor>
                                                                    <asp:RequiredFieldValidator ID="rfvContent" ControlToValidate="edContent" EnableClientScript="True" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>

            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

