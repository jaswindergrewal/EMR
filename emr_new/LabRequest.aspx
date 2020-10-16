<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LabRequest.aspx.cs" Inherits="LabRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ListBox" Assembly="obout_ListBox" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <input type="hidden" id="inpPatientID" runat="server" />
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td bgcolor="#D6B781">
                    <span class="style1"><strong>Appointment - Lab Request</strong></span>
                </td>
                <td bgcolor="#D6B781">
                    <div align="right">
                        <input name="Button" type="button" class="button" onclick="MM_goToURL('self','PatientInfo.aspx?patientid=<%=PatientID%>');return document.MM_returnValue"
                            value="Back to Patient Details" />
                    </div>
                </td>
            </tr>
            <tr bgcolor="#D6B781" class="regText">
                <td width="81%" bgcolor="#D6B781">
                    <b>Patient Name:</b>
                    <asp:Label ID="lblPatientName" runat="server" />
                </td>
                <td width="19%" bgcolor="#D6B781">
                    <div align="right">
                        &nbsp;</div>
                </td>
            </tr>
        </table>
        <br />
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="borderText">
            <tr bgcolor="#D6B781">
                <td>
                    <strong>Follow Up Date </strong>(Enter the range of dates for this request to be
                    done- [mm/dd/yyyy] )
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Start Range</strong>
                    <asp:TextBox ID="txtRangeStart" runat="server" CssClass="borderText" Width="80" />
                    <cc1:CalendarExtender ID="estStart" runat="server" TargetControlID="txtRangeStart" />
                    <asp:RequiredFieldValidator ID="valStart" runat="server" ControlToValidate="txtRangeStart"
                        ErrorMessage="Range start required" ForeColor="Red" Display="Dynamic" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<strong>End Range </strong>
                    <asp:TextBox ID="txtRangeEnd" runat="server" CssClass="borderText" Width="80" />
                    <cc1:CalendarExtender ID="extEnd" runat="server" TargetControlID="txtRangeEnd" />
                    &nbsp;Fasting:
                    <asp:RadioButtonList ID="rdoFasting" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Text="Yes" Value="Yes" />
                        <asp:ListItem Text="No" Value="No" />
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="valFasting" runat="server" ControlToValidate="rdoFasting"
                        ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="PageTitle">
                    Panels - Use Ctrl Click to select multiple values
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox runat="server" ID="lstPanels" SelectionMode="Multiple" DataSourceID="sqlPanels"
                        DataTextField="PanelName" DataValueField="LabRequest_PanelID" CssClass="FormField" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="PageTitle">
                    Tests - Use Ctrl Click to select multiple values
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox runat="server" ID="lstTests" SelectionMode="Multiple" DataSourceID="sqlTests"
                        DataTextField="TestName" DataValueField="LabRequest_TestID" CssClass="FormField" Width="300" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="sqlPanels" runat="server" SelectCommand="select * from LabRequest_Panels where Active_YN=1  order by PanelName"
            ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
        <asp:SqlDataSource ID="sqlTests" runat="server" SelectCommand="select * from LabRequest_Tests where Active_YN=1  order by TestName"
            ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
            CssClass="button" />
    </div>
    <div id="apDiv2">
        <table width="100%" border="1" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <p>
                        <strong>Follow Up (4 wk)</strong></p>
                    <p>
                        Testosterone,<br />
                        Progesterone,<br />
                        DHEA<br />
                        Pregnenolone,<br />
                        Estrogen,<br />
                        VIT-D<br />
                        CMP<br />
                        CBC / ferritin<br />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <strong>3/4 Month Lab</strong></p>
                    <p>
                        Lipids<br />
                        Thyroid<br />
                        HgbA1c<br />
                        Nutrition panel<br />
                        Estradiol<br />
                        Testosterone<br />
                        Progesterone<br />
                        DHEA<br />
                        Pregnenolone<br />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <strong>7 month</strong></p>
                    <p>
                        Lipids<br />
                        Thyroid<br />
                        Estradiol<br />
                        Testosterone<br />
                        Progesterone<br />
                        DHEA<br />
                        CMP<br />
                        IGF-1<br />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <strong>11 month</strong></p>
                    <p>
                        IMMUNE PANEL<br />
                        Mental function testing<br />
                        Sex Hormone Testing<br />
                        DEXA<br />
                        Nutritional panel</p>
                </td>
            </tr>
        </table>
    </div>
    <div id="apDiv1">
        <table width="100%" border="1" cellpadding="5" cellspacing="0">
            <tr>
                <td bgcolor="#FFFFCC">
                    <p>
                        <strong>Mandatory in House</strong></p>
                    <p>
                        <font color="#FF0000">Testosterone<br />
                            Progesterone<br />
                            Estradiol<br />
                            Thyroid<br />
                            Pregnenolone<br />
                            DHT<br />
                            IODINE STUDY</font></p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <strong>Nutrient Testing</strong></p>
                    <p>
                        Vitamins<br />
                        Minerals<br />
                        Amino Acids<br />
                        Antioxidants<br />
                        Fatty Acids<br />
                        Metabolites<br />
                        Carbohydrates Metab</p>
                </td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
    </div>
</asp:Content>
