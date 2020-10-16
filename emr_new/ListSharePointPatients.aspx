<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListSharePointPatients.aspx.cs" Inherits="ListSharePointPatients" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>List of Share point patients</title>

    <script type="text/javascript" src="Scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.20.js"></script>
    <script type="text/javascript" src="Scripts/jquery.jqGrid.js"></script>
    <script type="text/javascript" src="Scripts/grid.locale-en.js"></script>


    <link href="css/base/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/base/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/Scrips.js" type="text/javascript"></script>
    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function grdSharePointPatients_Edit(record) {

            window.location = "SharePointPatientsAddEdit.aspx?Id=" + record;
        }

        function grdSharePointPatients_Add() {

            window.location = "SharePointPatientsAddEdit.aspx?Id=0";
        }

        function grdSharePointPatients_Delete(record) {

            if (confirm("Are you sure you want to delete ?") == true) {

                var postData = new Object();
                postData.PatientId = record;

                $.ajax({
                    type: "POST",
                    url: "ListSharePointPatients.aspx/DeleteSharePointPatient",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {

                        if (result.d != "") {

                            grdSharePointPatients.refresh();

                        }
                        else {
                            alert("some error occured");
                        }

                    },
                    error: function (obj) {

                        alert("some error occured");

                    }
                });

            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td><asp:Button ID="btnAddSharePointPatients" Text="Add Patient" CssClass="button" runat="server" OnClick="btnAddSharePointPatients_Click" /></td>
        </tr>
    </table>
    <obout:Grid ID="grdSharePointPatients" Serialize="true" runat="server" OnRebind="grdSharePointPatients_Rebind" Width="100%" AutoGenerateColumns="False" AllowAddingRecords="False" FolderStyle="../grid_styles/Style_7" AllowRecordSelection="false"
        AllowFiltering="false" ShowFooter="true" AllowPaging="true" PageSize="30" AllowPageSizeSelection="false" CallbackMode="false">
        <Columns>
           <%-- <obout:Column HeaderText="Add" Width="5%" Index="0">
                <TemplateSettings TemplateId="templateAdd" />
            </obout:Column>--%>
            <obout:Column HeaderText="Edit" Width="5%" Index="1">
                <TemplateSettings TemplateId="templateEdit" />
            </obout:Column>
            <obout:Column HeaderText="Delete" Width="5%" Index="1">
                <TemplateSettings TemplateId="templateDelete" />
            </obout:Column>
            <obout:Column DataField="Id" HeaderText="Id" Width="5%"
                Visible="false" Index="2" />

            <obout:Column DataField="CreatedDate" HeaderText="Created Date" ReadOnly="True" DataFormatString="{0:MM/dd/yyyy}"
                Index="3" Width="8%" />
            <obout:Column DataField="ClinicName" HeaderText="Clinic" ReadOnly="True"
                Visible="true" Index="4" Width="7%" />
            <obout:Column DataField="ProviderName" HeaderText="Provider" Width="10%" Index="5" />
            <obout:Column DataField="StartRange" HeaderText="Start Range" ReadOnly="True" DataFormatString="{0:MM/dd/yyyy}"
                Index="6" Width="8%" />
            <obout:Column DataField="EndRange" HeaderText="EndRange" ReadOnly="True" DataFormatString="{0:MM/dd/yyyy}"
                Index="7" Width="8%" />
            <obout:Column DataField="ApptDuration" HeaderText="Duration" Width="7%"
                Visible="true" Index="7" />
            <obout:Column DataField="FirstName" HeaderText="FirstName" Width="9%" Index="8" />

            <obout:Column DataField="LastName" HeaderText="LastName" Width="10%" Index="9" />
            

            <obout:Column DataField="Phone" HeaderText="Phone" ReadOnly="True" Wrap="true"
                Visible="true" Index="10" Width="10%">
            </obout:Column>
            <obout:Column DataField="Notes" HeaderText="Notes" ReadOnly="True" Wrap="true"
                Visible="true" Index="11" Width="10%">
            </obout:Column>
        </Columns>
        <Templates>

           <%-- <obout:GridTemplate ID="templateAdd">
                <Template>
                    <a href="#" onclick="grdSharePointPatients_Add();">Add</a>
                </Template>
            </obout:GridTemplate>--%>
            <obout:GridTemplate ID="templateEdit">
                <Template>
                    <a href="#" onclick="grdSharePointPatients_Edit(<%# Container.DataItem["Id"].ToString()%>);">Edit</a>
                </Template>
            </obout:GridTemplate>
            <obout:GridTemplate ID="templateDelete">
                <Template>
                    <a href="#" onclick="grdSharePointPatients_Delete(<%# Container.DataItem["Id"].ToString()%>);">Delete</a>
                </Template>
            </obout:GridTemplate>
        </Templates>
        <LocalizationSettings NoRecordsText="No Record Found!" />

    </obout:Grid>

</asp:Content>

