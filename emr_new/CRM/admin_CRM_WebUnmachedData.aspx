<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_CRM_WebUnmachedData.aspx.cs" MasterPageFile="~/external/Site.master" Inherits="CRM_admin_CRM_WebUnmachedData" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function HideContactPopUp() {
            $("#ContactAddPopUp").hide();
            $("#SemiTransparentBG").hide();
        }




        function ShowContactPopUp(iRecordIndex) {



            //var list = document.getElementById('hdnData').value;
            var ProspectID = (grdProspect.Rows[iRecordIndex].Cells[0].Value);
            $("input[id=hdnProspectID]").val(ProspectID);
            var postData = new Object();
            postData.ProspectID = ProspectID;
            //postData.List = list;

            $.ajax({
                type: "POST",
                url: "admin_CRM_WebUnmachedData.aspx/ShowData",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    if (listObj != null) {
                        document.getElementById('lblSeminar').innerHTML = listObj.Seminar;
                        document.getElementById('lblFirstName').innerHTML = listObj.FirstName;
                        document.getElementById('lblLastName').innerHTML = listObj.LastName;
                        document.getElementById('lblPhone').innerHTML = listObj.Phone;
                        document.getElementById('lblEmail').innerHTML = listObj.Email;
                        document.getElementById('lblHear').innerHTML = listObj.HowHear;
                        document.getElementById('lblProspectID').innerHTML = listObj.ProspectID;

                        var ddlSeminar = document.getElementById('ddlSeminar');
                        $('#spnSeminar').hide();
                        $('#chkSeminar').prop('checked', false);

                        ddlSeminar.selectedIndex = 0;

                        for (var i = 0, j = ddlSeminar.options.length; i < j; ++i) {

                            if (ddlSeminar.options[i].innerHTML === listObj.Seminar) {
                                ddlSeminar.selectedIndex = i;
                                $('#spnSeminar').hide();
                                break;
                            }
                        }


                        $('#spnHowHear').show();
                        $('#chkHowHear').prop('checked', false);

                        var ddlHowHear = document.getElementById('ddlHowHear');
                        ddlHowHear.selectedIndex = 0;
                        for (var i = 0, j = ddlHowHear.options.length; i < j; ++i) {

                            if (ddlHowHear.options[i].innerHTML === listObj.HowHear) {
                                ddlHowHear.selectedIndex = i;
                                $('#spnHowHear').hide();
                                break;
                            }
                        }
                        $("#ContactAddPopUp").show();
                        $("#SemiTransparentBG").show();
                    }
                    else {

                        alert("Some problem occured.");

                    }

                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });




        }

        function AddProspect() {

            var chkSeminar = $('#chkSeminar').is(':checked');
            var ChkEventTrue = 0;
            if (chkSeminar == true) {
                ChkEventTrue = 1;
            }
            var EventName = document.getElementById('lblSeminar').innerHTML;
            var EventID = 0;

            //if ($('#spnSeminar').is(':visible')) {
            //    if (chkSeminar == false) {
            var EventID = document.getElementById('ddlSeminar').value;
            if (EventID == 0) {
                alert('Please match the Seminar from the list. or checked the checkbox for new enty.');
                return false;
            }
            //    }
            //}

            var chkHowHear = $('#chkHowHear').is(':checked');
            var chkMarketSource = 0;
            if (chkHowHear == true) {

                chkMarketSource = 1;
            }
            var MarketSourceName = document.getElementById('lblHear').innerHTML;
            var MarketSourceID = 0;

            if ($('#spnHowHear').is(':visible')) {
                if (chkHowHear == false) {
                    var MarketSourceID = document.getElementById('ddlHowHear').value;
                    if (MarketSourceID == 0) {
                        alert('Please match the Market source from the list. or checked the checkbox for new enty.');
                        return false;
                    }
                }
            }

            var postData = new Object();
            postData.ProspectID = document.getElementById('lblProspectID').innerHTML;
            postData.ChkEventTrue = ChkEventTrue;
            postData.EventName = EventName;
            postData.EventID = EventID;
            postData.chkMarketSource = chkMarketSource;
            postData.MarketSourceName = MarketSourceName;
            postData.MarketSourceID = MarketSourceID;
            postData.Email = document.getElementById('lblEmail').innerHTML;

            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/CRM/admin_CRM_WebUnmachedData.aspx/AddProspectData")%>',
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    if (listObj != "") {

                        alert(listObj);
                        HideContactPopUp();
                        grdProspect.refresh();

                    }
                    else {

                        alert("Some problem occured.");
                        HideContactPopUp();
                    }

                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnData" runat="server" ClientIDMode="Static" />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td>
                <b>List of Unassigned Prospect </b>
            </td>

        </tr>
        <tr>
            <td>
                <obout:Grid ID="grdProspect" runat="server" AllowFiltering="true"
                    AllowPageSizeSelection="true" AllowPaging="true" PageSize="20" AllowSorting="true"  
                    AutoGenerateColumns="false"  FolderStyle="grid_styles/Style_7"
                    Width="100%" OnRebind="grdProspect_Rebind" >
                    <Columns>
                        <obout:Column DataField="ProspectID" HeaderText="" Width="10%">
                            <TemplateSettings TemplateId="templateProspectID" />
                        </obout:Column>
                        <obout:Column DataField="FirstName" HeaderText="First Name" Width="20%">
                            <TemplateSettings TemplateId="templatePatientNameFirstName" />
                        </obout:Column>
                        <obout:Column DataField="LastName" HeaderText="Last Name" Width="20%">
                            <TemplateSettings TemplateId="templatePatientNameLastName" />
                        </obout:Column>

                        <obout:Column DataField="Email" HeaderText="Email" Width="20%">
                        </obout:Column>
                        <obout:Column DataField="Seminar" HeaderText="Event Name" Width="15%">
                            <TemplateSettings TemplateId="templateSeminar" />
                        </obout:Column>

                        <obout:Column DataField="HowHear" HeaderText="How Hear" Width="15%">
                            <TemplateSettings TemplateId="templateHowHear" />
                        </obout:Column>

                    </Columns>
                    <ClientSideEvents OnClientDblClick="ShowContactPopUp" />
                    <Templates>
                        <obout:GridTemplate ID="templateProspectID">
                            <Template>

                                 <a >Match
                               </a>
                          
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templatePatientNameFirstName">
                            <Template>
                                <%# Container.Value%>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templatePatientNameLastName">
                            <Template>
                               <%# Container.Value%>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templateSeminar">
                            <Template>
                                <%# Container.Value%>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="templateHowHear">
                            <Template>
                                <%# Container.Value%>
                            </Template>
                        </obout:GridTemplate>

                    </Templates>


                </obout:Grid>
            </td>
        </tr>
    </table>


    <div id="SemiTransparentBG" class="fadePanel " style="display: none;" visible="false"></div>

    <%--Add block start--%>
    <div id="ContactAddPopUp" class="Popup " style="display: none;">

        <div style="padding-top: 30px;" class="InnerPopup">
            <input type="hidden" id="hdnProspectID" name="hdnProspectID" runat="server" value="" />

            <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr bgcolor="#D6B781">
                    <td colspan="2">
                        <strong>Match Prospect Records </strong>
                    </td>

                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Prospet ID that need to match with EMR :
                            <asp:Label ID="lblProspectID" runat="server" ClientIDMode="Static"></asp:Label></strong>
                    </td>

                </tr>
                <tr>
                    <td class="FormLabel" style="text-align: right; width: 100px"><b>First Name:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblFirstName" runat="server" ClientIDMode="Static"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FormLabel" style="text-align: right;"><b>Last Name:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" ClientIDMode="Static"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FormLabel" style="text-align: right;"><b>Phone:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblPhone" Text="erewrwe" runat="server" ClientIDMode="Static"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FormLabel" style="text-align: right;"><b>Email:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" ClientIDMode="Static"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="FormLabel" style="text-align: right;"><b>Seminar:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblSeminar" runat="server" ClientIDMode="Static"></asp:Label>&nbsp;&nbsp;<span id="spnSeminar"><asp:CheckBox ID="chkSeminar" Text="(Check if you want to insert this value)" runat="server" ClientIDMode="Static" /></span><br />
                        <br />
                        <asp:DropDownList ID="ddlSeminar" runat="server" DataTextField="EventName" ClientIDMode="Static"
                            DataValueField="EventID" CssClass="FormField" />

                    </td>
                </tr>

                <tr>
                    <td class="FormLabel" style="text-align: right;"><b>How Hear:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblHear" runat="server" ClientIDMode="Static"></asp:Label>&nbsp;&nbsp;<span id="spnHowHear"><asp:CheckBox ID="chkHowHear" runat="server" ClientIDMode="Static" Text="(Check if you want to insert this value)" /></span><br />
                        <br />
                        <asp:DropDownList ID="ddlHowHear" runat="server" DataTextField="MarketingSourceName" ClientIDMode="static"
                            DataValueField="MarketingSourceID" CssClass="FormField" />

                    </td>
                </tr>
                <br />
                <tr>
                    <td colspan="2">

                        <input type="button" id="btnSubmit" value="Match Prospect" onclick="return AddProspect();" class="button" />
                        <input type="button" id="btnCancel" value="Cancel" onclick="HideContactPopUp();" class="button" />
                    </td>
                </tr>
            </table>
            <br />


        </div>
    </div>

    <%--End block start--%>

   
</asp:Content>
