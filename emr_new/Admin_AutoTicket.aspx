<%@ Page Title="Manage Ticket" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Admin_AutoTicket.aspx.cs" Inherits="Admin_AutoTicket" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/Auto_Ticket.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div width="100%">
        <obout:Grid ID="grdATicket" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="25" CellPadding="6" CellSpacing="6"
            Serialize="true" AllowRecordSelection="false"
            AllowAddingRecords="true" AllowFiltering="true" FolderStyle="grid_styles/Style_7"
            OnInsertCommand="grdATicket_Insert" EnableTypeValidation="false" OnRebind="grdATicket_Rebind" OnUpdateCommand="grdATicket_Insert_Update" OnDeleteCommand="grdATicket_Delete">

            <ClientSideEvents OnClientDblClick="onDoubleClick" OnBeforeClientUpdate="Validate" OnBeforeClientInsert="Validate" />
            <Columns>
                <obout:Column DataField="AutoTicketID" Visible="false" />
                <obout:Column DataField="AutoticketName" HeaderText="Autoticket Name" runat="server">
                    <TemplateSettings TemplateId="Template_AutoticketName" EditTemplateId="TemplateEdit_AutoticketName" />
                </obout:Column>
                <obout:Column DataField="Subject" HeaderText="Subject" runat="server">
                    <TemplateSettings TemplateId="Template_Subject" EditTemplateId="TemplateEdit_Subject" />
                </obout:Column>
                <obout:Column DataField="Body" HeaderText="Body" runat="server">

                    <TemplateSettings TemplateId="BodyTmp" EditTemplateId="BodyTmpEdit" />
                </obout:Column>
                <obout:Column DataField="followup_type_desc" Visible="false" />
                <obout:Column ID="FollowUp_Type_IDCol" DataField="FollowUp_Type_ID" HeaderText="Ticket Type"
                    runat="server">

                    <TemplateSettings TemplateId="TemplateFollowUp_Type_ID" EditTemplateId="TemplateEditFollowUp_Type_ID" />
                </obout:Column>
                <obout:Column DataField="DepartmentName" Visible="false" />
                <obout:Column DataField="EmployeeName" Visible="false" />
                <obout:Column DataField="CreatedBY" Visible="false" />
                <obout:Column ID="AssignedCol" DataField="Assigned" HeaderText="Assigned" runat="server">
                    <TemplateSettings TemplateId="TemplateAssigned" EditTemplateId="TemplateEditAssigned" />
                </obout:Column>
                <obout:Column DataField="DeptAssign" HeaderText="Group" runat="server">
                    <TemplateSettings TemplateId="TemplateDept" EditTemplateId="TemplateDEditDept" />
                </obout:Column>
                <obout:Column DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" runat="server">
                    <TemplateSettings EditTemplateId="templatedate" />
                </obout:Column>

                <obout:Column DataField="LastSent" HeaderText="Last Sent" DataFormatString="{0:MM/dd/yyyy}" runat="server">
                    <TemplateSettings EditTemplateId="templateLastdate" />
                </obout:Column>

                <obout:Column ID="CreatedID" DataField="CreatedID" HeaderText="Created By" runat="server">
                    <TemplateSettings TemplateId="CrBy" EditTemplateId="CrByEdit" />
                </obout:Column>
                <obout:Column ID="Frequency" DataField="Frequency" HeaderText="Frequency" runat="server"
                    Width="75">
                    <TemplateSettings TemplateId="Template_Frequency" EditTemplateId="TemplateEdit_Frequency" />
                </obout:Column>
                <obout:Column ID="FrequencyTypeCol" DataField="FrequencyType" HeaderText="Frequency Type">
                    <TemplateSettings TemplateId="FreqType" EditTemplateId="FreqTypeEdit" />
                </obout:Column>
                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" Width="125" runat="server" AllowDelete="true" />
            </Columns>
            <Templates>
                <obout:GridTemplate ID="Template_AutoticketName" runat="server">
                    <Template>
                        <%#  Container.DataItem["AutoticketName"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateEdit_AutoticketName" runat="server" ControlID="txtAutoticketName" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtAutoticketName" runat="server" MaxLength="200" ClientIDMode="Static" CssClass="FormFieldWhite" />
                    </Template>
                </obout:GridTemplate>

                <obout:GridTemplate ID="Template_Subject" runat="server">
                    <Template>
                        <%#  Container.DataItem["Subject"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateEdit_Subject" runat="server" ControlID="txtSubject" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtSubject" runat="server" MaxLength="200" ClientIDMode="Static" CssClass="FormFieldWhite" />
                    </Template>
                </obout:GridTemplate>


                <obout:GridTemplate ID="TemplateDept" runat="server">
                    <Template>
                        <%#  Container.DataItem["DepartmentName"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateDEditDept" runat="server" ControlID="ddlDept" ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList ID="ddlDept" runat="server" DataTextField="DepartmentName"
                            DataValueField="DepartmentID" CssClass="FormField" ClientIDMode="Static" Class="FormFieldWhite" />

                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateFollowUp_Type_ID" runat="server">
                    <Template>
                        <%#  Container.DataItem["followup_type_desc"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateEditFollowUp_Type_ID" runat="server" ControlID="ddlFtype"
                    ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList ID="ddlFtype" runat="server" DataTextField="FollowUp_Type_Desc"
                            DataValueField="FollowUp_Type_ID" CssClass="FormField" ClientIDMode="Static" Class="FormFieldWhite" Width="150px" />

                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateAssigned" runat="server">
                    <Template>
                        <%#  Container.DataItem["EmployeeName"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateEditAssigned" runat="server" ControlID="ddlAssigned"
                    ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList ID="ddlAssigned" runat="server" DataTextField="EmployeeName"
                            DataValueField="EmployeeId" CssClass="FormField" ClientIDMode="Static" Class="FormFieldWhite" />

                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="CrBy" runat="server">
                    <Template>
                        <%#  Container.DataItem["CreatedBY"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="CrByEdit" runat="server" ControlID="ddlCreatedBy" ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList ID="ddlCreatedBy" runat="server" DataTextField="EmployeeName"
                            DataValueField="EmployeeId" CssClass="FormField" ClientIDMode="Static" Class="FormFieldWhite" />

                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="FreqType" runat="server">
                    <Template>
                        <%#  Container.DataItem["FrequencyType"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="FreqTypeEdit" runat="server" ControlID="ddlFreqType" ControlPropertyName="value">
                    <Template>
                        <asp:DropDownList ID="ddlFreqType" runat="server" CssClass="FormField" ClientIDMode="Static" Class="FormFieldWhite">

                            <asp:ListItem Value="d">Days</asp:ListItem>
                            <asp:ListItem Value="w">Weeks</asp:ListItem>
                            <asp:ListItem Value="m">Months</asp:ListItem>
                            <asp:ListItem Value="y">Years</asp:ListItem>
                        </asp:DropDownList>



                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="BodyTmp" runat="server">
                    <Template>
                        <%# Container.DataItem["Body"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="BodyTmpEdit" runat="server" ControlID="txtBody" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtBody" runat="server" CssClass="FormFieldWhite" TextMode="MultiLine" Rows="4" Columns="20" ClientIDMode="Static" />
                    </Template>
                </obout:GridTemplate>

                <obout:GridTemplate ID="templatedate" runat="server" ControlID="txtStartDate" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="FormFieldWhite" ClientIDMode="Static"
                            BackColor="LightGray" ReadOnly="true" />
                        <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="txtStartDate"
                            DatePickerImagePath="images/date_picker1.gif">
                        </obout:Calendar>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="templateLastdate" runat="server" ControlID="txtLastDate" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtLastDate" runat="server" ClientIDMode="Static"
                            BackColor="LightGray" ReadOnly="true" CssClass="FormFieldWhite" />
                        <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="txtLastDate"
                            DatePickerImagePath="images/date_picker1.gif">
                        </obout:Calendar>
                    </Template>
                </obout:GridTemplate>

                <obout:GridTemplate ID="Template_Frequency" runat="server">
                    <Template>
                        <%# Container.DataItem["Frequency"]%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate ID="TemplateEdit_Frequency" runat="server" ControlID="txtFrequency" ControlPropertyName="value">
                    <Template>
                        <asp:TextBox ID="txtFrequency" runat="server" MaxLength="10" ClientIDMode="Static" Width="40px" CssClass="FormFieldWhite" />
                    </Template>
                </obout:GridTemplate>

            </Templates>
        </obout:Grid>
    </div>
</asp:Content>
