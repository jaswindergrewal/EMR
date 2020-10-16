<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManagementFeeProgram.aspx.cs" Inherits="ManagementFeeProgram" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/Auto_Ticket.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div width="100%">
      <obout:Grid ID="ManageProgramGrid" runat="server" ShowLoadingMessage="false" AllowPaging="true"
                            PageSize="10" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="true"
                            AllowPageSizeSelection="true" Serialize="false" CellPadding="0" Width="1050"
                            AllowFiltering="true" AllowRecordSelection="true" CellSpacing="0" ShowTotalNumberOfPages="false"
                            OnUpdateCommand="ManageProgramGrid_UpdateCommand" OnInsertCommand="ManageProgramGrid_InsertCommand"
                            FolderStyle="../grid_styles/Style_7" >
                            <Columns>

                                <obout:Column DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                                <obout:Column DataField="ProgramName" HeaderText="ProgramName" SortExpression="ProgramName" Width="250" HeaderAlign="center" />
                               
                                <obout:CheckBoxColumn DataField="IsActive" HeaderText="IsActive" Width="150" />
                                
                                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" 
                                    Width="125" runat="server" />
                            </Columns>


                            <%--<ClientSideEvents OnBeforeClientInsert="ValidateManageProgram" OnBeforeClientUpdate="ValidateManageProgram" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" />--%>

                        </obout:Grid>
    </div>
</asp:Content>