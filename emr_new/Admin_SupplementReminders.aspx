<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="Admin_SupplementReminders.aspx.cs" Inherits="Admin_SupplementReminders" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ListBox" Assembly="obout_ListBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        var applyFilterTimeout = null;
        function applyFilter() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeoutDiag);
            }

            applyFilterTimeoutDiag = window.setTimeout(doFiltering, 1000);
        }
        function doFiltering() {
            grdSymptomSupplement.filter();
        }

    </script>
    <script type="text/javascript">

        var applyFilterTimeoutDiag = null;
        function applyFilterDiag() {
            if (applyFilterTimeoutDiag) {
                window.clearTimeout(applyFilterTimeoutDiag);
            }

            applyFilterTimeoutDiag = window.setTimeout(doFilteringDiag, 1000);
        }
        function doFilteringDiag() {
            grdDiagnosisSupplement.filter();
        }

    </script>
    <script type="text/javascript">
        var applyFilterTimeoutLab = null;
        function applyFilterLab() {
            if (applyFilterTimeoutLab) {
                window.clearTimeout(applyFilterTimeoutLab);
            }

            applyFilterTimeoutDiag = window.setTimeout(doFilteringLab, 1000);
        }
        function doFilteringLab() {
            grdLabSupplement.filter();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer ID="tabScrips" runat="server" Width="800px" CssClass="lmc_tab"
        ActiveTabIndex="0">
        <cc1:TabPanel HeaderText="Symptoms" runat="server" ID="pnlSymptom" BackColor="#EFE1C9">
            <ContentTemplate>
                <p class="regText">
                    Symptom
					<asp:DropDownList ID="ddlSymptom" runat="server" DataSourceID="sqlSymptom" DataTextField="SymptomName"
                        DataValueField="SymptomID" ClientIDMode="Static" OnSelectedIndexChanged="ddlSymptom_SelectedIndexChanged"
                        AutoPostBack="True" />
                    <br />
                    <br />
                    <asp:Button ID="btnRecord" runat="server" CssClass="button" CausesValidation="False" Text="Record checked supplements"
                        OnClick="btnRecord_Click" />
                    <br />
                    <obout:Grid ID="grdSymptomSupplement" runat="server" OnRebind="grdSymptomSupplement_Rebind"
                        AutoGenerateColumns="False" AllowAddingRecords="False" FolderStyle="grid_styles/Style_7"
                        AllowFiltering="True" KeepSelectedRecords="False" OnPreRender="Grid_PreRender"
                        CallbackMode="False" Width="700px">
                        <Columns>
                            <obout:CheckBoxSelectColumn AllowSorting="False" ShowHeaderCheckBox="False" Width="75"
                                Index="0" />
                            <obout:Column DataField="Assigned" HeaderText="Currently Assigned" Width="120" AllowFilter="False"
                                Index="1" />
                            <obout:Column HeaderText="Supplement - enter text in box for filter" DataField="ProductName"
                                AllowSorting="False" Width="500" ReadOnly="True" ShowFilterCriterias="False"
                                FilterTemplateId="NameFilterHist" Index="2">
                                <TemplateSettings FilterTemplateId="NameFilterHist" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="True" Type="Contains" />
                                </FilterOptions>
                            </obout:Column>
                            <obout:Column DataField="ProductID" Visible="False" HeaderText="ProductID" Index="3" />
                        </Columns>
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                        <Templates>
                            <obout:GridTemplate runat="server" ID="NameFilterHist" ControlID="NameHist" ControlPropertyName="">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="NameHist" Width="150px" autocomplete="off">
								<ClientSideEvents OnKeyUp="applyFilter" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
                    <asp:SqlDataSource ID="sqlSymptom" runat="server" SelectCommand="select * from Symptoms where viewable_yn=1 and SymptomName <> '' order by SymptomName"
                        ConnectionString="<%$ ConnectionStrings:db %>" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Diagnosis" runat="server" ID="pnlDiagnosis" BackColor="#EFE1C9">
            <ContentTemplate>
                <p class="regText">
                    Symptom
					<asp:DropDownList ID="ddlDiagnosis" runat="server" DataSourceID="sqlDiagnosis" DataTextField="Diag_Title"
                        DataValueField="Diagnosis_ID" ClientIDMode="Static" OnSelectedIndexChanged="ddlDiagnosis_SelectedIndexChanged"
                        AutoPostBack="true" />
                    <br />
                    <br />
                    <asp:Button ID="btnRecordDiag" runat="server" CssClass="button" CausesValidation="false" Text="Record checked supplements"
                        Visible="true" OnClick="btnRecordDiag_Click" />
                    <br />
                    <obout:Grid ID="grdDiagnosisSupplement" runat="server" OnRebind="grdDiagnosisSupplement_Rebind"
                        AutoGenerateColumns="false" AllowAddingRecords="false" AllowPaging="true" PageSize="10"
                        FolderStyle="grid_styles/Style_7" AllowSorting="true" AllowFiltering="true" CallbackMode="true"
                        KeepSelectedRecords="false" OnPreRender="Grid_PreRender">
                        <Columns>
                            <obout:CheckBoxSelectColumn AllowSorting="false" ShowHeaderCheckBox="false" HeaderText=""
                                Width="75" />
                            <obout:Column AllowSorting="true" DataField="Assigned" HeaderText="Currently Assigned"
                                Width="120" AllowFilter="false" />
                            <obout:Column HeaderText="Supplement - enter text in box for filter" DataField="ProductName"
                                AllowSorting="false" Width="500" ReadOnly="true" ShowFilterCriterias="false"
                                FilterTemplateId="NameFilterDiag">
                                <TemplateSettings FilterTemplateId="NameFilterDiag" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="True" Type="Contains" />
                                </FilterOptions>
                            </obout:Column>
                            <obout:Column DataField="ProductID" Visible="false" />
                        </Columns>
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                        <Templates>
                            <obout:GridTemplate runat="server" ID="NameFilterDiag" ControlID="NameDiag" ControlPropertyName="">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="NameDiag" Width="150px" autocomplete="off">
								<ClientSideEvents OnKeyUp="applyFilterDiag" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
                    <asp:SqlDataSource ID="sqlDiagnosis" runat="server" SelectCommand="select * from Diagnosis_tbl where viewable_yn=1 and Diag_Title <> '' order by Diag_Title"
                        ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
                </p>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Labs" runat="server" ID="pnlLabs" BackColor="#EFE1C9">
            <ContentTemplate>
                <p class="regText">
                    Symptom
					
                    <asp:DropDownList ID="ddlGroups" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlGroups_SelectedIndexChanged"
                        AutoPostBack="true" />

                    <br />
                    <b>Select a range:</b>

                    <obout:ListBox ID="ddlRanges" runat="server" FolderStyle="lsStyles/plain"
                        ClientIDMode="Static" SelectionMode="Single" OnSelectedIndexChanged="ddlRanges_SelectedIndexChanged"
                        AutoPostBack="true">
                    </obout:ListBox>
                    &nbsp;&nbsp;&nbsp;
					<input type="button" class="button" value="Add range" onclick="skedWindow.Open();" />
                    <br />
                    <br />
                    <asp:Button ID="btnRecordLabs" runat="server" CssClass="button" CausesValidation="false" Text="Record checked supplements"
                        Visible="true" OnClick="btnRecordLabs_Click" />
                    <br />
                    <obout:Grid ID="grdLabSupplement" runat="server" OnRebind="grdLabSupplement_Rebind"
                        AutoGenerateColumns="false" AllowAddingRecords="false" AllowPaging="true" PageSize="10"
                        FolderStyle="grid_styles/Style_7" AllowSorting="true" AllowFiltering="true" CallbackMode="true"
                        KeepSelectedRecords="false" OnPreRender="Grid_PreRender">
                        <Columns>
                            <obout:CheckBoxSelectColumn AllowSorting="false" ShowHeaderCheckBox="false" HeaderText=""
                                Width="75" />
                            <obout:Column AllowSorting="true" DataField="Assigned" HeaderText="Currently Assigned"
                                Width="120" AllowFilter="false" />
                            <obout:Column HeaderText="Supplement - enter text in box for filter" DataField="ProductName"
                                AllowSorting="false" Width="500" ReadOnly="true" ShowFilterCriterias="false"
                                FilterTemplateId="NameFilterLab">
                                <TemplateSettings FilterTemplateId="NameFilterLab" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="True" Type="Contains" />
                                </FilterOptions>
                            </obout:Column>
                            <obout:Column DataField="ProductID" Visible="false" />
                        </Columns>
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                        <Templates>
                            <obout:GridTemplate runat="server" ID="NameFilterLab" ControlID="NameLab" ControlPropertyName="">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="NameLab" Width="150px" autocomplete="off">
								<ClientSideEvents OnKeyUp="applyFilterLab" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
                    <owd:Window ID="skedWindow" runat="server" IsModal="true" ShowCloseButton="true"
                        Status="" Top="200" Left="100" Height="200" Width="400" VisibleOnLoad="false"
                        StyleFolder="wdstyles/grandgray" Title="Add / Edit Record">                        
                      &nbsp;&nbsp;  Low value (number only)
						<asp:TextBox ID="txtLow" runat="server" /><br />
                       &nbsp;&nbsp; High Value (number only)
						<asp:TextBox ID="txtHigh" runat="server" /><br />
                        &nbsp;&nbsp;<obout:OboutButton ID="OboutButton1" runat="server" Text="Save" OnClientClick="skedWindow.Close();"
                            OnClick="SaveChanges" Width="75" />
                        <obout:OboutButton ID="OboutButton2" runat="server" Text="Cancel" OnClientClick="skedWindow.Close();" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Low value" Display="None" ControlToValidate="txtLow">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLow" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"
                            Display="None" ErrorMessage="Please enter only numeric value in Low."></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter High value" Display="None" ControlToValidate="txtHigh">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtHigh" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" Display="None"
                            ErrorMessage="Please enter only numeric value in High."></asp:RegularExpressionValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" />
                    </owd:Window>

                </p>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
