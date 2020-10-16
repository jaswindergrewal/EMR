<%@ Page Title="admin_lab_patientmatch" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="admin_lab_patientmatch.aspx.cs" Inherits="admin_lab_patientmatch" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td>
                <b>List of Unassigned Patients from Quest Import (<%=(totalPatient)%>)  </b>
            </td>

        </tr>
        <tr>
            <td>
                <obout:Grid ID="grdtUnmatchedLabsPatientData" EnableRecordHover="true"
                    runat="server" AllowFiltering="True" AutoGenerateColumns="false" AllowAddingRecords="false"
                    AllowSorting="true" ShowLoadingMessage="true" Width="100%" PageSize="50" EnableTypeValidation="false"
                    FolderStyle="grid_styles/style_7" OnRowDataBound="grdtUnmatchedLabsPatientData_RowDataBound">
                    <FilteringSettings FilterPosition="Top" />
                    <Columns>
                        <obout:Column DataField="ID" HeaderText="ID" Width="15%">
                            <TemplateSettings TemplateId="templateID" />
                        </obout:Column>
                        <obout:Column DataField="PatientNameFirstName" HeaderText="First Name" Width="25%">
                            <TemplateSettings TemplateId="templatePatientNameFirstName" />
                        </obout:Column>
                        <obout:Column DataField="PatientNameLastName" HeaderText="Last Name" Width="25%">
                            <TemplateSettings TemplateId="templatePatientNameLastName" />
                        </obout:Column>
                        <obout:Column DataField="DateOfBirth" HeaderText="Date of Birth" Width="25%">
                        </obout:Column>
                        <obout:Column DataField="Sex" HeaderText="Sex" Width="10%">
                            <TemplateSettings TemplateId="templateSex" />
                        </obout:Column>

                    </Columns>
                    <Templates>
                        <obout:GridTemplate ID="templateID">
                            <Template>
                                <a href="admin_lab_patientmatch_edit.aspx?labid=<%# Container.Value%>"><%# Container.Value%></a>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templatePatientNameFirstName">
                            <Template>
                                <a href="admin_lab_patientmatch_edit.aspx?labid=<%# Container.DataItem["ID"].ToString()%>"><%# Container.Value%></a>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templatePatientNameLastName">
                            <Template>
                                <a href="admin_lab_patientmatch_edit.aspx?labid=<%# Container.DataItem["ID"].ToString()%>"><%# Container.Value%></a>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templateSex">
                            <Template>
                                <%# Container.Value%>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                </obout:Grid>
            </td>
        </tr>
    </table>
</asp:Content>

