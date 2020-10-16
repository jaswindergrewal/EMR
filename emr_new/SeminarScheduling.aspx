<%@ Page Title="" Language="C#" MasterPageFile="~/external/Site.master" AutoEventWireup="true" CodeFile="SeminarScheduling.aspx.cs" Inherits="Seminar_scheduling_SeminarScheduling" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .rcorners {
            border-radius: 25px;
            -moz-border-radius: 25px ;
            -webkit-border-radius: 25px ;
            width: 100px;
            height: 66px;
            border: 2px solid #00FFFF;
            
    
            background: #00FFFF;
            font-weight: bold;
        }

        .WalkInButton {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 180px;
            height: 66px;
            border: 2px solid #00FFFF;
            background: #00FFFF;
            font-weight: bold;
            font-size: 17px;
        }

        .selectedClinicButton {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 100px;
            height: 66px;
            border: 2px solid #0080FF;
            background: #0080FF;
            font-weight: bold;
        }

        .rcornersWeek {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 128px;
            height: 66px;
            border: 2px solid #1ff56f;
            background: #1ff56f;
            font-weight: bold;
        }

        .selectedWeekDayButton {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 128px;
            height: 66px;
            border: 2px solid #29b527;
            background: #29b527;
            font-weight: bold;
            
        }

        .NoColorWeekDayButton {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 128px;
            height: 66px;
            border: 2px solid #ff0000;
            background: #ff0000;
            font-weight: bold;
            
        }

        .selectedClinicButton {
            border-radius: 25px;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            width: 100px;
            height: 66px;
            border: 2px solid #0080FF;
            background: #0080FF;
            font-weight: bold;
        }

       

        .ob_gBody .ob_gC, .ob_gBody .ob_gCW {
            font-size: 13px !important;
        }
        /* Body rows */
        .ob_gBody tbody .ob_gC, .ob_gBody tbody .ob_gCW {
            height: 50px !important;
        }

        .tableRow {
            background-color: #e0710f;
            font-size: 13px;
            font-weight: bold;
        }
    </style>


    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/ScheduleSeminar.js"></script>
    <script type="text/javascript" src="../Scripts/Common.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnAppointmentId" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnDay" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnEventId" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="StaffID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnClincId" runat="server" ClientIDMode="Static" />
             <asp:HiddenField ID="hdnIsRegistrant" runat="server" ClientIDMode="Static" />
            <table style="width:750px" border="0" cellpadding="5" class="borderText">
                <tr class="tableRow">
                    <td colspan="3">Seminar Schedule</td>
                </tr>
                <tr>
                    <td width="100px"><b>Select Seminar</b><br />
                        <asp:DropDownList ID="drpEvents" Style="font-size: 18px;" runat="server" OnSelectedIndexChanged="drpEvents_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnKirkland" ClientIDMode="Static" runat="server" Text="Kirkland" CssClass="rcorners" OnClick="btnKirkland_Click" />
                        <asp:Button ID="btnTacoma" ClientIDMode="Static" runat="server" Text="Tacoma" CssClass="rcorners" OnClick="btnTacoma_Click" />
                        <asp:Button ID="btnLynnwood" ClientIDMode="Static" runat="server" Text="Lynnwood" CssClass="rcorners" OnClick="btnLynnwood_Click" />
                    </td>
                    <td width="100px"><b>Select Week</b><br />
                        <asp:DropDownList ID="drpDate" Style="font-size: 18px;" runat="server" OnSelectedIndexChanged="drpDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="height: 5px"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btnMonday" ClientIDMode="Static" runat="server" Text="Monday" Enabled="false" CssClass="NoColorWeekDayButton" OnClick="btnMonday_Click" />
                        <asp:Button ID="btnTuesday" ClientIDMode="Static" runat="server" Text="Tuesday" Enabled="false" CssClass="NoColorWeekDayButton" OnClick="btnTuesday_Click" />
                        <asp:Button ID="btnWednesday" ClientIDMode="Static" runat="server" Text="Wednesday" Enabled="false" CssClass="NoColorWeekDayButton" OnClick="btnWednesday_Click" />
                        <asp:Button ID="btnThursday" ClientIDMode="Static" runat="server" Text="Thursday" Enabled="false" CssClass="NoColorWeekDayButton" OnClick="btnThursday_Click" />
                        <asp:Button ID="btnFriday" ClientIDMode="Static" runat="server" Text="Friday" Enabled="false" CssClass="NoColorWeekDayButton" OnClick="btnFriday_Click" />
                    </td>

                </tr>
                <tr class="tableRow">
                    <td colspan="3">
                        <asp:Label runat="server" ClientIDMode="Static" ID="grdHeading"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <obout:Grid ID="grdSeminar" runat="server" Width="100%" OnRebind="grdSeminar_Rebind"
                            OnSelect="grdSeminar_Select" AutoGenerateColumns="False" AllowAddingRecords="False" FolderStyle="grid_styles/Style_7"
                            AllowFiltering="false" ShowFooter="true" PageSize="-1" AllowPageSizeSelection="false" CallbackMode="true">
                            <Columns>
                                <obout:Column DataField="ProviderName" ControlStyle-Font-Size="14px" HeaderText="Details" ReadOnly="True" Width="80%" Index="0" Align="center" />
                                <obout:Column HeaderText="Edit" Width="15%" Index="1" ControlStyle-Font-Size="14px">
                                    <TemplateSettings TemplateId="templateEdit" />
                                </obout:Column>
                                <obout:Column DataField="apt_id" Visible="False" HeaderText="AptId" Index="2" />

                            </Columns>
                            <Templates>

                                <obout:GridTemplate ID="templateEdit">
                                    <Template>
                                        <a href="#" onclick="grdSeminar_Edit(<%# Container.DataItem["apt_id"].ToString()%>);">Edit</a>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>


                            <LocalizationSettings NoRecordsText="No Record Found!" />

                        </obout:Grid>



                    </td>

                </tr>


            </table>

            <owd:Window ID="ScheduleAppointmentWindow" runat="server" IsModal="True" ShowCloseButton="True"
                Status="" Top="200" Left="100" Height="400" Width="600" VisibleOnLoad="False"
                StyleFolder="../wdstyles/grandgray" Title="Seminar Schedule" DebugMode="True" EnableClientSideControl="False" IconPath="" InitialMode="NORMAL" IsDraggable="True" IsResizable="True" MinHeight="0" MinWidth="0" OnClientClose="" OnClientDrag="" OnClientInit="" OnClientOpen="" OnClientPreClose="" OnClientPreOpen="" OnClientResize="" Opacity="100" Overflow="HIDDEN" PageOpacity="25" RelativeElementID="" ShowMaximizeButton="False" ShowStatusBar="True">
                <br />

                <table style="margin-left: 10px; margin-right: 5px; width: 500px;">
                    <tr>
                        <td width="250px">Registrant<br />
                            <asp:DropDownList ID="drpRegistrants" Style="font-size: 17px;" Width="225px" runat="server" OnSelectedIndexChanged="drpRegistrants_SelectedIndexChanged1" ClientIDMode="Static" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td width="250px" style="text-align: center">
                            <asp:Button ID="btnWalkIn" ClientIDMode="Static" runat="server" Text="Walk in" CssClass="WalkInButton" OnClick="btnWalkIn_Click" />
                        </td>
                    </tr>


                    <tr>

                        <td width="250px">
                            <br />
                            First Name<br />
                            <asp:TextBox ID="txtProspectFirstName" Width="225px" Style="font-size: 17px;" Enabled="false" runat="server" ClientIDMode="Static" BackColor="LightGray" onkeypress="return Restrictspecialchar(event)"
                                TabIndex="1" CssClass="regText" MaxLength="100" />
                        </td>

                        <td width="250px">
                            <br />
                            Last Name<br />
                            <asp:TextBox ID="txtProspectLastName" Width="225px" Style="font-size: 17px;" Enabled="false" runat="server" CssClass="regText" ClientIDMode="Static" onkeypress="return Restrictspecialchar(event)"
                                BackColor="LightGray" TabIndex="2" MaxLength="100" />
                        </td>
                    </tr>


                    <tr>

                        <td width="250px">
                            <br />
                            Phone<br />
                            <asp:TextBox ID="txtProspectMainPhone" Width="225px" Style="font-size: 17px;" Enabled="false" runat="server" CssClass="regText txtMainPhone" ClientIDMode="Static"
                                BackColor="LightGray" TabIndex="7" />
                        </td>

                        <td width="250px">
                            <br />
                            Email<br />
                            <asp:TextBox ID="txtProspectEmail" Width="225px" Style="font-size: 17px;" Enabled="false" runat="server" CssClass="regText" ClientIDMode="Static"
                                BackColor="LightGray" TabIndex="9" MaxLength="100" />
                        </td>
                    </tr>


                    <tr>
                        <td valign="top" style="text-align: center" colspan="2">
                            <br />
                            <asp:Button ID="btnScheduleAppointment" CssClass="WalkInButton"  runat="server" Text="Schedule" TabIndex="6" OnClientClick="saveChangesforAppointment(); return false;"
                                FolderStyle="" />&nbsp;&nbsp;&nbsp;
                       
                             <asp:Button ID="btnCancel" runat="server" CssClass="WalkInButton" Text="Cancel" TabIndex="7" OnClientClick="CloseWindow(); return false;"
                                 FolderStyle="" /></td>
                    </tr>

                </table>




            </owd:Window>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
    </table>



</asp:Content>



