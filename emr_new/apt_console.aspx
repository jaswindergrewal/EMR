<%@ Page Title="Appointment Console" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="apt_console.aspx.cs" Inherits="apt_console"
    EnableEventValidation="false" ViewStateEncryptionMode="Always" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link href="resources/custom_scripts/row-reorder/row-reorder.css" rel="stylesheet"
        type="text/css" />

    <style type="text/css">
        #Layer3 {
            position: absolute;
            z-index: 27;
            left: 647px;
            top: 125px;
        }
        /*--.chkMale > tbody > tr > td  {*/
       .chkMale td
{
   width:33%; /* or percent value: 25% */
}
           
    </style>

    <style type="text/css" media="screen">
        /* commented backslash hack for ie5mac \*/
        html, body {
            height: 100%;
        }
        /* end hack */
        .CenterPB {
            position: absolute;
            left: 50%;
            top: 50%;
            margin-top: -30px; /* make this half your image/element height */
            margin-left: -30px; /* make this half your image/element width */
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <asp:Label ID="lblAptInfo" runat="server" CssClass="PageTitle" />
        &nbsp;
		<asp:Label ID="lblRenewalMonth" runat="server" CssClass="PageTitle" />
    </p>
    <div id="Layer3">
        <span class="regText">
            <input name="Submit3" type="submit" class="button" onclick="MM_goToURL('parent', 'manage.aspx?patientid=<%= PatientID %>    ');return document.MM_returnValue"
                value="Patient Details" />
        </span>
    </div>
    <div class="CenterPB">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd" DisplayAfter="0">
            <ProgressTemplate>
                <asp:Image ID="imgUpdate" runat="server" ImageUrl="images/indicator.gif" />Loading
				. . .
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="ConsoleContainer" runat="server" Width="1200px" CssClass="lmc_tab"
                ActiveTabIndex="0" OnActiveTabChanged="ConsoleContainer_ActiveTabChanged" AutoPostBack="true">
                <cc1:TabPanel HeaderText="Apt Details" runat="server" ID="Details" BackColor="#EFE1C9">
                    <ContentTemplate>
                        <asp:Panel ID="pnlDetails" runat="server">
                            <table width="925" cellpadding="5" cellspacing="0" class="border" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td colspan="8">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="10">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781" class="regText">
                                                                <strong>Medical Notes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>&nbsp;&nbsp;
																[<a href="MedicalNoteAdd_Long.aspx?patientid=<%=PatientID %>&aptid=<%= Request.QueryString["aptid"] %>&StaffID=<%= Session["StaffID"] %>&ActiveTab=Details">Add</a>]
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText">
                                                                <asp:Repeater ID="rptMedNotes" runat="server">
                                                                    <ItemTemplate>
                                                                        <span>[<font color="#993333"><%# ((DateTime)Eval("ContactDateEntered")).ToShortDateString() %></font>]</span>
                                                                        <span><%# Eval("MessageBody") %></span>
                                                                        <span>- [<font color="#666666"><%# Eval("EmployeeName") %></font>] </span>&nbsp;
                                                                        <span>[<a href="MedicalNoteUpdate.aspx?contactid=<%# Eval("ContactID") %>&StaffID=<%= Session["StaffID"] %>&ApptID=<%= AptID %>">Edit</a>] &nbsp;</span>
                                                                        <br />
                                                                        <br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    <br>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781" class="regText">
                                                                <strong>Schedule Consult</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<a href="ScheduleConsult.aspx?patientid=<%=PatientID%>&aptid=<%= Request.QueryString["aptid"] %>&StaffID=<%= Session["StaffID"] %>&MasterPage=~/site.master">Add</a>]
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText">
                                                                <asp:Repeater ID="rptConsult" runat="server">
                                                                    <ItemTemplate>
                                                                        [<font color="#993333"><%# Eval("Range_Start") == null ? "" : ((DateTime)Eval("Range_Start")).ToShortDateString()%>-
																			<%# Eval("Range_End") %></font>] - (<font color="#993333"><%# Eval("FollowUp_Type_Desc")%></font>)
																		<%# Eval("FollowUp_Body")%>
																		- [<font color="#666666"><%# Eval("EmployeeName")%></font>]
																		<br>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    <br>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781" class="regText">
                                                                <strong>Schedule General Follow Up &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong>[<a href="FollowupNote.aspx?patientid=<%=PatientID%>&aptid=<%=AptID %>&StaffID=<%=Session["StaffID"] %>&MasterPage=~/site.master">Add</a>]
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText">
                                                                <asp:Repeater ID="rptGenFollow" runat="server">
                                                                    <ItemTemplate>
                                                                        (<font color="#993333"><%# Eval("FollowUp_Type_Desc") %></font>)
																		<%# Eval("followup_Body") %>&nbsp;-&nbsp;[<font color="#666666"><%# Eval("EmployeeName") %></font>]<br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    <br>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781" class="regText">
                                                                <strong>Blood Draw Requests</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<a href="LabAddLong.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%=PatientID%>&StaffID=<%=Session["StaffID"] %>">Add</a>]
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText">
                                                                <asp:Repeater ID="rptBloodDraw" runat="server">
                                                                    <ItemTemplate>
                                                                        <a href="LabEditLong.aspx?LabID=<%# Eval("FollowUpID") %>&PatientID=<%=PatientID%>&aptid=<%= AptID %>">[<font color="#993333"><%# Eval("value") %>
                                                                            <%--<%# Eval("Range_End") %>--%>
                                                                        </font>]</a>
                                                                        <%# Eval("followup_body") %>- [<font color="#666666"><%# Eval("EmployeeName")%></font>]<p />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    <br>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td colspan="5" bgcolor="#D6B781" class="regText">
                                                                <strong>Prescription</strong>(s) <strong>Written</strong>&nbsp;&nbsp;&nbsp;&nbsp;[<a
                                                                    href="PresrcriptionList.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>">Add</a>]
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText" valign="top">
                                                                <strong>Drug</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Sig</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Dispenses</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Refills</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Written By </strong>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="rptRX" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("DrugName") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("Drug_Dose") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("Drug_Dispenses") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("Drug_NumbRefills") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("EmployeeName")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    <br>
                                                    <br>
                                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#E7D5B4"
                                                        class="border">
                                                        <tr>
                                                            <td colspan="6" bgcolor="#D6B781" class="regText">
                                                                <strong>Future Appointments</strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="regText" valign="top">
                                                                <strong>Patient</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Appointment Start</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Appointment End</strong>
                                                            </td>
                                                            <td class="regText" valign="top">
                                                                <strong>Status</strong>
                                                            </td>
                                                            <td class="regText" valign="top"></td>
                                                            <td></td>
                                                        </tr>


                                                        <asp:Repeater ID="rptAppointmentScheduled" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("LastName") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("ApptStart") %> 
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("ApptEnd") %>
                                                                    </td>
                                                                    <td class="regText" valign="top">
                                                                        <%# Eval("StatusName") %>
                                                                    </td>
                                                                    <td class="regText" valign="top"></td>
                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="RX" runat="server" ID="RX">
                    <ContentTemplate>
                        <asp:Panel ID="pnlRX" runat="server" Visible="false">
                            <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td colspan="9">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top">
                                                    <table width="275" border="0" cellpadding="5" cellspacing="0" class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781">
                                                                <strong><strong><font color="#000000">List of Current Medications</font></strong></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater ID="rptDrugs" runat="server">
                                                                    <ItemTemplate>
                                                                        <strong>Drug: </strong>
                                                                        <%# Eval("DrugName")%><strong><br>
                                                                            Sig</strong>:
																		<%# Eval("Drug_Dose")%><br />
                                                                        <br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table width="275" border="0" cellpadding="5" cellspacing="0" class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781">
                                                                <strong>List of <font color="#FF0000">Supplement</font>RX </strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater ID="rptSupp" runat="server">
                                                                    <ItemTemplate>
                                                                        <strong></font>Supp: </strong>
                                                                        <%# Eval("Supplement_Name") %><strong><br>
                                                                            Dose per day: </strong>
                                                                        <%# Eval("DosePerDay") %><br>
                                                                        <strong>Notes</strong>:
																		<%# Eval("Notes") %>><br />
                                                                        <br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table width="275" border="0" cellpadding="5" cellspacing="0" class="border">
                                                        <tr>
                                                            <td bgcolor="#D6B781">
                                                                <strong>List of <font color="#FF0000">3rd Party </font>RX </strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater ID="rptThirdParty" runat="server">
                                                                    <ItemTemplate>
                                                                        </font><strong>Drug: </strong>
                                                                        <%# Eval("DrugName")%><strong><br>
                                                                            Sig</strong>:
																		<%# Eval("Drug_Dose")%>><br />
                                                                        <br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Med Notes" runat="server" ID="MedNotes">
                    <ContentTemplate>
                        <asp:Panel ID="pnlNotes" runat="server" Visible="false">
                            <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td colspan="9">
                                        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                                            <tr bgcolor="#D6B781" class="regText">
                                                <td>
                                                    <b>Contact Entries (all)</b>
                                                </td>
                                                <td colspan="2">&nbsp;
                                                    <b>Contact Type:</b> &nbsp;&nbsp;<asp:DropDownList ID="drpContacttype" runat="server" ClientIDMode="static" CssClass="FormField"></asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                     <asp:Button ID="btnSearch" Text="Search" class="button" runat="server" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>

                                            <tr class="regText">
                                                <td width="150">
                                                    <b>Date</b>
                                                </td>
                                                <td colspan="2">
                                                    <b>Notes</b>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptNotes" runat="server">
                                                <ItemTemplate>
                                                    <tr valign="top" class="regText">
                                                        <td width="150">
                                                            <a href="contact_record_close.aspx?ContactID=<%# Eval("ContactID")%>">
                                                                <%# Eval("value") %></a>
                                                        </td>
                                                        <td colspan="2">
                                                            <%# Eval("MessageBody") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr valign="top" class="regText">
                                                <td colspan="3">
                                                    <hr size="1">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Lab Work" runat="server" ID="LabWork">
                    <ContentTemplate>
                        <asp:Panel ID="pnlLab" runat="server" Visible="False">
                            <cc1:TabContainer ID="LabContainer" runat="server" CssClass="lmc_sub">
                                <cc1:TabPanel ID="CurrentLab" runat="server" HeaderText="Last Drawn">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table width="500" border="0" cellpadding="6" cellspacing="0" class="border">
                                                        <tr bgcolor="#D6B781" class="regText">
                                                            <td>
                                                                <b>&quot;Last Drawn&quot; Lab Table</b>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr class="regText">
                                                            <td>
                                                                <strong>Test Name</strong>
                                                            </td>
                                                            <td>
                                                                <strong>Last Date</strong>
                                                            </td>
                                                            <td>
                                                                <strong># of Days</strong>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="rptCurrLab" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="regText">
                                                                    <td>
                                                                        <%# Eval("GroupName") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("LastComplete") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DispLine") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                                <td valign="top"></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History1" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist1" runat="server" width="87%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none; border-width: 0"
                                            frameborder="0" />
                                        <table id="WriteScrip1" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop1" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip1" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History2" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist2" runat="server" width="87%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip2" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop2" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip2" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History3" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist3" runat="server" width="87%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip3" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop3" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip3" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History4" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist4" runat="server" width="85%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip4" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop4" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip4" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History5" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist5" runat="server" width="87%" height="3000px" style="background-image: url(images/export/beige_back.gif); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip5" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop5" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip5" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History6" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist6" runat="server" width="87%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip6" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop6" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip6" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="History7" runat="server">
                                    <ContentTemplate>
                                        <iframe id="ifrHist7" runat="server" width="87%" height="3000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                        <table id="WriteScrip7" runat="server">
                                            <tr>
                                                <td colspan="5" bgcolor="#D6B781" class="regText">[<a href="prescriptionlist.aspx?aptid=<%= Request.QueryString["aptid"] %>&patientid=<%= PatientID %>"
                                                    target="_blank"><strong>Add Prescription</strong>(s) </a>]
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:AlwaysVisibleControlExtender ID="OnTop7" runat="server" HorizontalOffset="1075"
                                            HorizontalSide="Left" VerticalOffset="300" VerticalSide="Top" TargetControlID="WriteScrip7" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="LabHistory" runat="server" HeaderText="All Labs">
                                    <ContentTemplate>
                                        <table width="275" border="0" cellpadding="3" cellspacing="0" class="border">
                                            <tr>
                                                <td>
                                                    <strong>Lab Draw Date </strong>
                                                </td>
                                                <td>
                                                    <strong>Date of Entry into EMR </strong>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptLabs" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <a href="lab_report_short.aspx?message_id=<%# Eval("MessageID") %>&patientid=<%= PatientID %>"
                                                                target="_blank">
                                                                <%# ((DateTime)Eval("ObservationDateTime")).ToShortDateString() %></a>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LastChanged") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="LabCharts" runat="server" HeaderText="Lab Charts">
                                    <ContentTemplate>
                                        <iframe id="ifrNewChart" runat="server" width="100%" height="2000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="OldCharts" runat="server" HeaderText="Old Charts">
                                    <ContentTemplate>
                                        <iframe id="ifrOldChart" runat="server" width="100%" height="2000px" style="background-image: url('images/export/beige_back.gif'); border-style: none;"
                                            frameborder="0" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Vitals" runat="server" ID="Vitals">
                    <ContentTemplate>
                        <asp:Panel ID="pnlVitals" runat="server" Visible="false">
                            <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td>
                                        <asp:Repeater ID="rptVitals" runat="server">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="3" cellspacing="0" class="border">
                                                    <tr>
                                                        <td width="85" bgcolor="#D6B781">
                                                            <strong>Date Taken</strong>
                                                        </td>
                                                        <td width="140" bgcolor="#D6B781">
                                                            <%# Eval("Date_Entered") %>
                                                        </td>
                                                        <td bgcolor="#D6B781"></td>
                                                        <td width="130">[<a href="VitalsEdit.aspx?VitalID=<%# Eval("Vital_ID") %>&AptID=<%= AptID %>&ActiveTab=Vitals">Edit</a>]
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>Weight (lbs)</strong>
                                                        </td>
                                                        <td align="left" width="140">
                                                            <%# Eval("Wgt") %>
                                                        </td>
                                                        <td width="85">
                                                            <strong>Height (in)</strong>
                                                        </td>
                                                        <td align="left" width="130">
                                                            <%# Eval("Height") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>BP</strong>
                                                        </td>
                                                        <td width="140">
                                                            <%# Eval("BloodPres") %>
                                                        </td>
                                                        <td width="85">
                                                            <strong>Temp</strong>
                                                        </td>
                                                        <td width="130">
                                                            <%# Eval("Temperature") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>Pulse</strong>
                                                        </td>
                                                        <td width="140">
                                                            <%# Eval("Pulse") %>
                                                        </td>
                                                        <td width="85">
                                                            <strong>Resp</strong>
                                                        </td>
                                                        <td width="130">
                                                            <%# Eval("Respirations") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>Waist Cir</strong>
                                                        </td>
                                                        <td width="140">
                                                            <%# Eval("Waist_Circm")%>
                                                        </td>
                                                        <td width="85">
                                                            <strong>BMI</strong>
                                                        </td>
                                                        <td width="130">
                                                            <%# Eval("BMI") %>
                                                          
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>Hip Cir</strong>
                                                        </td>
                                                        <td align="left" width="140">
                                                            <%# Eval("Hip_Circm").ToString().Trim() %>
                                                        </td>
                                                        <td width="85">
                                                            <strong>Waist Hip Ratio</strong>
                                                        </td>
                                                        <td width="130">
                                                            <strong><%# Eval ("WaistHipRatio") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="85">
                                                            <strong>Body Fat</strong>
                                                        </td>
                                                        <td width="140">
                                                            <%# Eval("Perc_Body_fat")%>%
                                                        </td>
                                                        <td width="85">
                                                            <strong>Left Grip</strong>;
                                                        </td>
                                                        <td width="130">
                                                            <%# Eval ("grip_l_lbs") %>
															lbs
                                                        </td>
                                                        <tr>
                                                            <td width="85">
                                                                <strong>Right Grip</strong>
                                                            </td>
                                                            <td width="140">
                                                                <%# Eval("grip_r_lbs") %>
																lbs
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td width="130">&nbsp;
                                                            </td>

                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Problem List" runat="server" ID="IcdCode">
                    <ContentTemplate>
                        <asp:Panel ID="pnlIcdCode" runat="server" Visible="false">
                            <table width="1000" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td colspan="9">
                                        <br />
                                        <br>
                                        <table width="1000" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnCopy" Text="Copy" runat="server" OnClick="btnCopy_Click"/></td>
                                            </tr>
                                            <tr>
                                                <td><strong>Male</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList CssClass="chkMale" ID="chkMale" runat="Server" DataTextField="Description" DataValueField="Id" RepeatDirection="Horizontal"
   RepeatColumns="3" CellPadding="4" CellSpacing="4" RepeatLayout="Table"></asp:CheckBoxList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Female</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList CssClass="chkMale" ID="chkFemale" runat="Server" DataTextField="Description" DataValueField="Id" RepeatDirection="Horizontal"
   RepeatColumns="3" CellPadding="4" CellSpacing="4" RepeatLayout="Table"></asp:CheckBoxList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Both</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList CssClass="chkMale" ID="chkBoth" runat="Server" DataTextField="Description" DataValueField="Id" RepeatDirection="Horizontal"
   RepeatColumns="3" CellPadding="4" CellSpacing="4" RepeatLayout="Table"></asp:CheckBoxList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Other</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList CssClass="chkMale" ID="chkOther" runat="Server" DataTextField="Description" DataValueField="Id" RepeatDirection="Horizontal"
   RepeatColumns="3" CellPadding="4" CellSpacing="4" RepeatLayout="Table"></asp:CheckBoxList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>

                <cc1:TabPanel HeaderText="Suppliment List" runat="server" ID="SupplimentList">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSuppliment" runat="server" Visible="false">
                             <asp:Repeater ID="rptSuppliments" runat="server" >
                                                <ItemTemplate>
                                                    <tr>
                                                       
                                                       
                                                        <td>
                                                            <div align="left">
                                                              <br />
                                                               <font color='#000000'>
                                                                <%# Eval("ProductName") %></font><br />
                                                            </div>
                                                        </td>
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Old Problem List" runat="server" ID="ProblemList">
                    <ContentTemplate>
                        <asp:Panel ID="pnlProblems" runat="server" Visible="false">
                            <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td colspan="9">
                                        <br />
                                        <br>
                                        <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                                            class="border">
                                            <tr bgcolor="#D6B781">
                                                <td width="300">
                                                    <strong>Diagnoses</strong>
                                                </td>
                                                <td>
                                                    <strong>Date Entered </strong>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Priority</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Sev</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <p>
                                                        &nbsp;
                                                    </p>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptProblems" runat="server" OnItemCommand="rptProblems_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td width="300">
                                                            <asp:CheckBox ID="chkDiagnosis" runat="server" Checked='<%# (int)Eval("apt")==1?true:false%>' ToolTip='<%# Eval ("Diagnosis_ID")%>' OnCheckedChanged="chkDiagnosis_CheckedChanged" AutoPostBack="true" />
                                                            <%# Eval("LineColor") %>
                                                            <a href="problem_diag_edit.aspx?ProbDiagID=<%# Eval("ProbDiagID")%>&patientid=<%= PatientID %>&MasterPage=~/site.master&AptID=<%=AptID%>">
                                                                <%# Eval("Diag_Title") %>
										[<%# Eval("ICD9_Code") %>]</a>
                                                            <%# (int)Eval("BeingAddressed_YN")==1 ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>

                                            [<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProbDiagID") %>'>Delete</asp:LinkButton>]
                                  
									</font>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <%# Eval("LineColor") %>
                                                            <%# Eval("DateEntered") %>
                                                            <%# (int)Eval("Active_YN")==0 ?" - "+ Eval("inactive_date") : "" %>
									</font>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LineColor") %>
                                                            <%# Eval("Priority_num") %></font></div>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# Eval("LineColor") %>
                                                                <%# Eval("Severity_num") %></font>
                                                            </div>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <div align="right">
                                                                <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (int)Eval("Active_YN")==1?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                                                <asp:ImageButton ID="imgActive1" runat="server" Visible='<%#  (int)Eval("Active_YN")==0?true:false%>' ImageUrl="~/images/reactivate.png" AlternateText="Reactivate this issue" CommandName="Active" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                                                <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  (int)Eval("BeingAddressed_YN")==0 && (int)Eval("Active_YN") == 1?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("ProbDiagID")%>' />


                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="5" nowrap="nowrap" bgcolor="#D6B781">
                                                    <asp:DropDownList ID="ddlDiagnoses" runat="server" />
                                                    &nbsp;&nbsp;Priority
							<asp:DropDownList CssClass="FormField" ID="Priority" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    &nbsp;&nbsp;Severity
							<asp:DropDownList CssClass="FormField" ID="Severity" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    <asp:Button ID="btnProblems" runat="server" CssClass="button" Text="+ Diagnosis"
                                                        OnClick="btnProblems_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                        <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                                            class="border">
                                            <tr bgcolor="#D6B781">
                                                <td width="300">
                                                    <strong>Symptoms</strong>
                                                </td>
                                                <td>
                                                    <strong>Date Entered </strong>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Priority</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Sev</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Trend</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <p>
                                                        &nbsp;
                                                    </p>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptSymptoms" runat="server" OnItemCommand="rptSymptoms_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td width="300" nowrap="nowrap">
                                                            <%#(int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                            <a href="problem_symp_edit_Short.aspx?probsymptid=<%# Eval("SymptomID") %>&patientid=<%# Eval("PatientID") %>&MasterPage=~/site.master&AptID=<%=AptID%>">
                                                                <%#Eval("SymptomName") %></a>
                                                            <%# (int)Eval("BeingAddressed_YN")==1 ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>
									[<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SymptomID") %>'>Delete</asp:LinkButton>]
                                   </font>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                            <%# Eval("DateEntered") %>
                                                            <%# (int)Eval("Active_YN")==0 ?" - "+ Eval("Inactive_Date") : "" %>
									</font>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                                <%# Eval("priority_num") %></font>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                                <%# Eval("severity_num") %></font>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# (int)Eval("Active_YN") == 0 ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                                <%# Eval("Dir") %></font>
                                                            </div>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <p align="right">
                                                                <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (int)Eval("Active_YN")==1?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("SymptomID")%>' />
                                                                <asp:ImageButton ID="imgActive1" runat="server" Visible='<%#  (int)Eval("Active_YN")==0?true:false%>' ImageUrl="~/images/reactivate.png" AlternateText="Reactivate this issue" CommandName="Active" CommandArgument='<%# Eval("SymptomID")%>' />
                                                                <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  (int)Eval("BeingAddressed_YN")==0 && (int)Eval("Active_YN") == 1?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("SymptomID")%>' />

                                                            </p>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="6" nowrap="nowrap" bgcolor="#D6B781">
                                                    <asp:DropDownList ID="ddlSymptom" runat="server" />
                                                    &nbsp;&nbsp;Priority
							<asp:DropDownList name="Priority_sym" CssClass="FormField" ID="Priority_sym" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    &nbsp;&nbsp;Severity
							<asp:DropDownList name="Severity_sym" CssClass="FormField" ID="Severity_sym" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    <asp:Button ID="btnSympt" runat="server" CssClass="button" Text="+ Symptom" OnClick="btnSympt_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                        <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                                            class="border">
                                            <tr bgcolor="#D6B781">
                                                <td width="300">
                                                    <strong>Diagnoses handled by 3rd party </strong>
                                                </td>
                                                <td>
                                                    <strong>Date Entered </strong>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Priority</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <strong>Sev</strong>
                                                    </div>
                                                </td>
                                                <td>
                                                    <p>
                                                        &nbsp;
                                                    </p>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptMisc" runat="server" OnItemCommand="rptMisc_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td width="300" nowrap="nowrap">
                                                            <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                            <a href='problem_miscdiag_edit.aspx?ProbDiagID=<%# Eval("ProbDiagID") %>&patientid=<%= PatientID %>&MasterPage=~/site.master&AptID=<%=AptID%>'>
                                                                <%# Eval("Diag_Title") %></a>
                                                            <%# (bool)Eval("BeingAddressed_YN") ? ":&nbsp;<img src='images/exclamation.gif' alt='This issue is a current Priority'>" : "" %>
									[<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='return ConfirmDelete();'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProbDiagID") %>'>Delete</asp:LinkButton>]
                                           
									</font></font>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                            <%# Eval("DateEntered") %>
                                                            <%# (bool)Eval("Active_YN") ==false? " - " + Eval("inactive_date") : "" %>
									</font></font>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                                <%# Eval("Priority_num") %></font></font>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div align="center">
                                                                <%# (bool)Eval("Active_YN") == false ? "<font color='#999999'>" : "<font color='#000000'>" %>
                                                                <%# Eval("Severity_num") %></font></font>
                                                            </div>
                                                        </td>
                                                        </td>
								<td nowrap="nowrap">
                                    <div align="right">
                                        <asp:ImageButton ID="imgActive" runat="server" Visible='<%# (bool)Eval("Active_YN")?true:false%>' ImageUrl="~/images/close.png" AlternateText="Close this issue" CommandName="Inactive" CommandArgument='<%# Eval("ProbDiagID")%>' />

                                        <asp:ImageButton ID="imgActive2" runat="server" Visible='<%#  !(bool)Eval("BeingAddressed_YN") && (bool)Eval("Active_YN") ?true:false%>' ImageUrl="~/images/exclamation_add.gif" AlternateText="Set as a Priority" CommandName="Address" CommandArgument='<%# Eval("ProbDiagID")%>' />
                                    </div>
                                </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="5" nowrap="nowrap" bgcolor="#D6B781">
                                                    <asp:DropDownList ID="ddlMisc" runat="server" />
                                                    &nbsp;Priority
							<asp:DropDownList name="Misc_prio" CssClass="FormField" ID="Misc_prio" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    &nbsp;Severity
							<asp:DropDownList name="MiscSev" CssClass="FormField" ID="MiscSev" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                                                    <asp:Button ID="btnMisc" runat="server" CssClass="button" Text="+ 3rd Party Diagnosis"
                                                        OnClick="btnMisc_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br></br>
                                        <br></br>
                                        <br></br>
                                    </td>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="Uploads" runat="server" ID="Uploads">
                    <ContentTemplate>
                        <asp:Panel ID="pnlUploads" runat="server" Visible="false">
                            <table width="925" cellpadding="5" cellspacing="0" class="borderCopy" style="background-color: #EFE1C9; border-style: none;">
                                <tr>
                                    <td width="990" colspan="9">
                                        <table border="0" cellpadding="6" cellspacing="0" class="border">
                                            <tr bgcolor="#D6B781" class="regText">
                                                <td width="207">
                                                    <b>Patient Documents or Scans </b>
                                                </td>
                                                <td width="204"></td>
                                            </tr>
                                            <tr class="regText">
                                                <td>
                                                    <strong>Document Title</strong>
                                                </td>
                                                <td>
                                                    <strong>Category</strong>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptUploads" runat="server">
                                                <ItemTemplate>
                                                    <tr class="regText">
                                                        <td>
                                                            <a href="#" onclick="DownLoadFile(<%# Eval("PatientID")%>,<%# Eval("UploadID")%>);">
                                                                <%# Eval("Upload_Title")%>
                                                            </a>
                                                            <%--<a href="uploads/<%= PatientID %>/<%# Eval("Upload_Path") %>" target="_blank">
                                                                <%# Eval("Upload_Title") %>
                                                            </a>--%>
                                                        </td>
                                                        <td>
                                                            <a href="#" onclick="DownLoadFile(<%# Eval("PatientID")%>,<%# Eval("UploadID")%>);">
                                                                <%# Eval("Category") %></a> [<a href="upload_edit.aspx?UploadID=<%# Eval("UploadID")%>">Edit</a>]
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="CriticalTasks" runat="server" HeaderText="Critical Tasks">
                    <ContentTemplate>
                        <asp:Panel ID="pnlCriticalTasks" runat="server" Visible="false">
                            <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF"
                                class="border">
                                <tr bgcolor="#D6B781">
                                    <td>
                                        <strong>Critical Tasks</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdTasks" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            RowStyle-align="Center" CellPadding="4" CssClass="regText" RowStyle-valign="Middle"
                                            Width="672px">
                                            <Columns>
                                                <asp:BoundField DataField="TaskName" HeaderText="Task" ItemStyle-VerticalAlign="Middle" />
                                                <asp:TemplateField HeaderText="Requested">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbRequested" runat="server" Checked='<%# Eval("Requested") %>'
                                                            CssClass="regText" OnCheckedChanged="Requested_CheckedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="lblRequested" runat="server" Text='<%# Eval("RequestedDate") %>' CssClass="regText" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Received">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbReceived" runat="server" Checked='<%# Eval("Received") %>' CssClass="regText"
                                                            OnCheckedChanged="Received_CheckedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="lblReceived" runat="server" Text='<%# Eval("ReceivedDate") %>' CssClass="regText" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reviewed">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbReviewed" runat="server" Checked='<%# Eval("Reviewed") %>' CssClass="regText"
                                                            OnCheckedChanged="Reviewed_CheckedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="lblReviewed" runat="server" Text='<%# Eval("ReviewedDate") %>' CssClass="regText" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("TaskID") %>' Style="display: none" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkDoc" runat="server" NavigateUrl='<%# Eval("Upload_Path") %>'
                                                            Text='<%# Eval("Upload_Title") %>' Target="_blank" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Dummy" runat="server" Style="visibility: hidden;" />
                            <cc1:ModalPopupExtender ID="modReceived" BackgroundCssClass="ModalPopupBG" runat="server"
                                CancelControlID="" TargetControlID="Dummy" PopupControlID="pnlReceived" Y="200" />
                            <asp:Panel ID="pnlReceived" runat="server" CssClass="modalPopup" Width="600px">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="Div4">
                                        <div class="TitlebarLeft" id="Div5" runat="server">
                                            Recieve the document
                                        </div>
                                    </div>
                                    <div class="popup_Body">
                                        <p>
                                            <asp:Label ID="lblTaskName" runat="server" />
                                        </p>
                                        Select the upload that was received
										<asp:GridView ID="grdDocs" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="grdDocs_SelectedIndexChanged"
                                            DataKeyNames="ID,PatientID" Caption="Documents for patient" CssClass="FormField"
                                            EmptyDataText="No documents" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlDoc" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Upload_Path") %>'
                                                            Target="_blank" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:g}" />
                                                <asp:ButtonField ControlStyle-CssClass="button" Text="Select" ButtonType="Button"
                                                    CommandName="Select" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="popup_Buttons">
                                        <asp:Button ID="btnCancelDoc" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancelDoc_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel HeaderText="OVU" runat="server" ID="OVUPanel">
                    <ContentTemplate>
                        <asp:Panel ID="pnlOVU" runat="server" Visible="false">
                            <iframe frameborder="0" id="PageContents" runat="server" width="1070px" height="1600px"
                                style="overflow: auto;" />

                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function DownLoadFile(patientId, UploadId) {

            url = '<%=Page.ResolveUrl("~/apt_console.aspx/DownLoadFile")%>';
             var options = {
                 type: "POST",
                 url: url,
                 data: "{patientId:'" + patientId + "',UploadId:'" + UploadId + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (result) {
                     urllink = '<%=Page.ResolveUrl("~/Uploads")%>';
                    //Window.open(urllink + '/' + result);
                    MM_goToURL('self', 'Uploads/' + result.d)

                },
                error: function (obj) {
                    alert(obj.responseText);
                }
             };
             $.ajax(options);
        }




    </script>
</asp:Content>
