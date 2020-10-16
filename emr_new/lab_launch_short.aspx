<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="lab_launch_short.aspx.cs" Inherits="lab_launch_short" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #apDiv1 {
            position: absolute;
            left: 618px;
            top: 70px;
            width: 164px;
            z-index: 26;
            height: 94px;
        }
    </style>
    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="apDiv1">
        <p>&nbsp;</p>
        <a href="/chart/labchart.aspx?patientid=ABC"></a>
        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td bgcolor="#D6B781">
                    <div align="center">
                        <p><a href="./labchart/LabTable.aspx?patientID=<%= PatientId %>" target="_blank">Launch Lab Charts</a></p>
                    </div>
                </td>
            </tr>
        </table>
        <br>
        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td bgcolor="#D6B781">
                    <div align="center">
                        <p><a href="./labchart/LabTableOld.aspx?patientID=<%= PatientId %>" target="_blank">Old Lab Charts</a></p>
                    </div>
                </td>
            </tr>
        </table>
        <p>&nbsp;</p>
    </div>
    <div id="PatientName" style="width: 600px; visibility: visible;">
        <p class="PageTitle"><strong>Lab Review Launch Page</strong></p>
        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td width="81%" bgcolor="#D6B781"><b>Patient Name:</b>
                    <asp:Label runat="server" ID="LabelPatientName"></asp:Label></td>
                <td width="19%" bgcolor="#D6B781">
                    <div align="right">
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div id="PageDetails" style="visibility: visible;">
        <asp:Repeater ID="RepeaterPatientInformation" runat="server">

            <HeaderTemplate>
                <table width="600" border="0" cellpadding="3" cellspacing="0" class="border">
                    <tr>
                        <td><strong>Lab Draw Date </strong></td>
                        <td><strong>Date of  Entry into EMR </strong></td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="600" border="0" cellpadding="3" cellspacing="0" class="border">
                    <tr>
                        <td>
                            <asp:HyperLink ID="HyperLinkAction" runat="server" Text='<%# Eval("ObservationDateTime") %>' NavigateUrl='<%#String.Format("lab_report_short.aspx?message_id={0}&patientid={1}", Eval("MessageID"), PatientId)%>'></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelLastChanged" Text='<%# Eval("LastChanged") %>'></asp:Label></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

