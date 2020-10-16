<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CrmDashBoard.master" AutoEventWireup="true" CodeFile="CRM_Dashboard.aspx.cs" Inherits="CRM_CRM_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="../jqPlot/plugins/excanvas.js"></script><![endif]-->
    <link href="../css/CRMdashboard.css" rel="stylesheet" />
    <link rel="stylesheet" href="../jqPlot/plugins/jquery.css">
    <script src="../jqPlot/plugins/jquery.js"></script>


    <script src="../jqPlot/plugins/jquery_002.js"></script>
    <script src="../jqPlot/plugins/jquery_003.js" lang="javascript"></script>
    <script src="../jqPlot/plugins/jquery_004.js"></script>
    <script src="../Scripts/dashboard.js"></script>


    <script type="text/javascript" src="../jqPlot/plugins/jqplot_005.js" lang="javascript"></script>
    <script type="text/javascript" src="../jqPlot/plugins/jqplot.js" lang="javascript"></script>
   
    <script type="text/javascript" src="../jqPlot/plugins/jqplot_006.js" lang="javascript"></script>
    <script type="text/javascript" src="../jqPlot/plugins/jqplot_004.js" lang="javascript"></script>
    <script type="text/javascript" src="../jqPlot/plugins/jqplot_003.js" lang="javascript"></script>
    <script type="text/javascript" src="../jqPlot/plugins/jqplot_002.js" lang="javascript"></script>
    <script type="text/javascript" src="../jqPlot/plugins/jqplot_005.js" lang="javascript"></script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="dashboard">
        <section class="section_top">
            <div>
                <a href="@Url.Action">
                    <asp:Button CssClass="ClassRead" ID="btnManageProspect" Text="Manage Prospect" runat="server" OnClick="btnManageProspect_Click" /></a>

                <a href="@Url.Action">
                    <asp:Button CssClass="ClassRead" ID="btnNewProspect" Text="New Prospect" runat="server" OnClick="btnNewProspect_Click" /></a>

            </div>
        </section>
        <section class="div_sec_container">
            <section class="section_left">
                <label>Statistics</label>
                <div class="div_line"></div>

                <div class="left_div">
                    <div>
                        <label>Total</label>
                    </div>
                    <div class="txt_cntr">
                        <label id="lblLeftTotal" runat="server"></label>
                    </div>
                </div>
                <div class="left_div">
                    <div>
                        <label>Attendant</label>
                    </div>
                    <div class="txt_cntr">
                        <label id="lblLeftAttendant" runat="server"></label>
                    </div>
                </div>
                <div class="left_div">
                    <div>
                        <label>Converted</label>
                    </div>
                    <div class="txt_cntr">
                        <label id="lblLeftConverted" runat="server"></label>
                    </div>
                </div>
                <div class="left_div">
                    <div>
                        <label>
                            Med Start              
                        </label>
                    </div>
                    <div class="txt_cntr">
                        <label id="lblLeftMedStart" runat="server"></label>
                    </div>
                </div>
            </section>
            <section class="section_center">
                <label>Prospects by Campaign </label>
                <div class="div_line"></div>
                <div style="margin-left: 68%">
                    <%--<select id="ddlLeadInYear">
                    <option>--Year--</option>
                </select>--%>
                    <asp:DropDownList ID="ddlCampaign" runat="server" DataTextField="CampaignName"
                        DataValueField="CampaignID" Style="width: 77%" ClientIDMode="static">
                    </asp:DropDownList>
                    <input type="button" value="Go" onclick="GetDashBoardData()" />
                </div>
                <div id="barDiv1"></div>
            </section>
            <section class="section_right">
                <label>Prospect Reports</label>
                 <div class="div_line"></div>
                 <div class="right_div">
                    <div>
                        <label> <a href="ReportCRM.aspx?rptID=1" runat="server">Registration by campaign</a></label>
                    </div>
                   
                </div>
                 <div class="right_div">
                    <div>
                        <label> <a  href="ReportCRM.aspx?rptID=2" runat="server">Registration by Event</a></label>
                    </div>
                   
                </div>
                 <div class="right_div">
                    <div>
                        <label> <a  href="ReportCRM.aspx?rptID=3" runat="server">Total Converted prospect</a></label>
                    </div>
                   
                </div>
                 <div class="right_div">
                    <div>
                        <label> <a  href="ReportCRM.aspx?rptID=4" runat="server">Total Med start</a></label>
                    </div>
                   
                </div>

                   <div class="right_div">
                    <div>
                        <label> <a  href="ReportCRM.aspx?rptID=4" runat="server">Total Attentend Prospect</a></label>
                    </div>
                   
                </div>
               

            </section>


            <section class="section_bottom">
                <div style="margin: 10px;">
                    <label class="labeltext">Prospects by Event </label>
                    <div style="float: right">


                        <asp:DropDownList ID="ddlEvent" runat="server" DataTextField="EventName"
                            DataValueField="EventID" ClientIDMode="static">
                        </asp:DropDownList>
                        <input type="button" value="Go" onclick="GetDashBoardEventData()" />
                    </div>
                </div>
                <div class="div_detailed">
                    Total<label id="lbltotal"></label>
                    Converted<label id="lblConverted"></label>
                    Attendent<label id="lblAttendent"></label>
                    Med start<label id="lblMedstart"></label>
                    Not Converted<label id="lblNotConverted"></label>
                </div>
                <div id="lineMultiple"></div>
            </section>
        </section>
    </div>
    <div id="loading-div-background">
        <div id="loading-div" class="ui-corner-all">
            <img src="images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>

</asp:Content>

