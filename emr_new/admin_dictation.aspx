<%@ Page Title="Manage Dictation Console " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_dictation.aspx.cs" Inherits="admin_dictation" %>

<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
    TagPrefix="NineRays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="6" cellspacing="6">
                <tr>
                    <td colspan="3" class="PageTitle" align="center">Manage Dictation Console
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <p class="regText">
                            Show only those items that contain
                            <asp:TextBox ID="txtSearchDiag" runat="server" Columns="10" />
                            <asp:Button ID="btnSearchDiag" runat="server" CssClass="button" Text="Search" OnClick="btnSearchDiag_Click" />
                        </p>
                        <NineRays:FlyTreeView ID="treDiags" runat="server" Padding="3px" FadeEffect="True"
                            ExpandLevel="2" BackColor="White" DragDropAcceptNames="Product" PostBackOnDropAccept="true"
                            OnNodeInserted="treDiags_NodeInserted" BackgroundImage="~/images/export/beige_back.gif"
                            CssClass="regText" Height="300" BorderWidth="1" BorderStyle="Solid" DragDropMode="Copy">
                            <HoverStyle Font-Underline="False" BackColor="Black" ForeColor="White" />
                            <Nodes>
                                <NineRays:FlyTreeNode Expanded="true" Text="Available Diagnoses">
                                    <NineRays:FlyTreeNode Text="Diagnosis 1" DragDropAcceptNames="plan" ContextMenuID="DiagMenu">
                                        <NineRays:FlyTreeNode Text="Plan 1" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 2" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 3" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    </NineRays:FlyTreeNode>
                                    <NineRays:FlyTreeNode Text="Diagnosis 2" DragDropAcceptNames="plan" ContextMenuID="DiagMenu">
                                        <NineRays:FlyTreeNode Text="Plan 1" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 2" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 3" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    </NineRays:FlyTreeNode>
                                    <NineRays:FlyTreeNode Text="Diagnosis 3" DragDropAcceptNames="plan" ContextMenuID="DiagMenu">
                                        <NineRays:FlyTreeNode Text="Plan 1" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 2" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 3" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    </NineRays:FlyTreeNode>
                                    <NineRays:FlyTreeNode Text="Plan items with no Diagnosis" DragDropAcceptNames="plan">
                                        <NineRays:FlyTreeNode Text="Plan 1" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 2" DragDropName="plan" ContextMenuID="PlanMenu" />
                                        <NineRays:FlyTreeNode Text="Plan 3" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    </NineRays:FlyTreeNode>
                                </NineRays:FlyTreeNode>
                            </Nodes>
                        </NineRays:FlyTreeView>
                        <NineRays:FlyContextMenu ID="PlanMenu" runat="server" OnCommand="PlanMenu_Command">
                            <Items>
                                <NineRays:FlyMenuItem Text="Edit" CommandName="Edit" AutoPostBack="true" />
                                <NineRays:FlyMenuItem Text="Remove" CommandName="Remove" AutoPostBack="true" />
                            </Items>
                        </NineRays:FlyContextMenu>
                        <NineRays:FlyContextMenu ID="DiagMenu" runat="server" OnCommand="DiagMenu_Command">
                            <Items>
                                <NineRays:FlyMenuItem Text="Edit" CommandName="Edit" AutoPostBack="true" />
                                <NineRays:FlyMenuItem Text="Remove" CommandName="Remove" AutoPostBack="true" />
                            </Items>
                        </NineRays:FlyContextMenu>
                    </td>
                    <td valign="top">
                        <p class="regText">
                            Show only those items that contain
                            <asp:TextBox ID="txtSearchPlans" runat="server" Columns="10" />
                            <asp:Button ID="btnSearchPlans" runat="server" CssClass="button" Text="Search" OnClick="btnSearchPlans_Click" />
                        </p>
                        <NineRays:FlyTreeView ID="trePlans" runat="server" Padding="3px" FadeEffect="True"
                            DragDropName="" DragDropMode="Copy" ExpandLevel="2" CssClass="regText" Width="300"
                            BorderStyle="Solid" BorderWidth="1" Height="300">
                            <HoverStyle Font-Underline="False" BackColor="Black" ForeColor="White" />
                            <Nodes>
                                <NineRays:FlyTreeNode Expanded="true" Text="Available Plan Items" DragDropAcceptNames="plan">
                                    <NineRays:FlyTreeNode Text="Run 30 miles a day" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    <NineRays:FlyTreeNode Text="Lose Weight" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    <NineRays:FlyTreeNode Text="Gaine Weigfht" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    <NineRays:FlyTreeNode Text="More Veggies" DragDropName="plan" ContextMenuID="PlanMenu" />
                                    <NineRays:FlyTreeNode Text="More Fruits" DragDropName="plan" ContextMenuID="PlanMenu" />
                                </NineRays:FlyTreeNode>
                            </Nodes>
                        </NineRays:FlyTreeView>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="regText">
                        <asp:Label ID="Label1" CssClass="PageTitle" Text="Create a new diagnosis<br/>All Entries except Key Words required"
                            runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="LabelMessage" runat="server" Visible="false" ForeColor="Red" />
                        <br />
                        <asp:Label ID="Label2" runat="server" CssClass="regText" Text="Diagnosis Name" />
                        <br />
                        <asp:TextBox ID="txtDiagName" runat="server" BackColor="LightGray" /><br />
                        <asp:Label ID="lblCode" runat="server" CssClass="regText" Text="ICD Code" /><br />
                        <asp:TextBox ID="txtCode" runat="server" CssClass="regText" BackColor="LightGray" />
                        <br />
                        Key Words used for searching:<br />
                        <asp:TextBox ID="txtKey1Diag" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey2Diag" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey3Diag" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey4Diag" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey5Diag" runat="server" Columns="6" BackColor="LightGray" />
                        <br />
                        <asp:Label ID="Label3" runat="server" CssClass="regText" Text="Diagnosis Description" /><br />
                        <asp:TextBox ID="txtDiagDescrip" runat="server" CssClass="regText" TextMode="MultiLine"
                            Rows="5" Columns="60" BackColor="LightGray" /><br />
                        <asp:Button ID="btnAddDiag" runat="server" CssClass="button" Text="Add  Diagnosis"
                            OnClick="btnAddDiag_Click" />
                    </td>
                    <td valign="top" class="regText">
                        <asp:Label ID="lblNewName" CssClass="PageTitle" Text="Create a new plan item<br/>All Entries except Key Words required"
                            runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="LabelMessagePlan" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                        <br />
                        <asp:Label ID="lblNewPlanName" runat="server" CssClass="regText" Text="Item Name" />
                        <br />
                        <asp:TextBox ID="txtNewPlanName" runat="server" BackColor="LightGray" /><br />
                        Category<br />
                        <asp:RadioButtonList ID="rdoPlanCat" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow" CssClass="regText">
                            <asp:ListItem Text="Diet" />
                            <asp:ListItem Text="Exercise" />
                            <asp:ListItem Text="Other" />
                        </asp:RadioButtonList>
                        <br />
                        Key Words used for searching:<br />
                        <asp:TextBox ID="txtKey1Plan" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey2Plan" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey3Plan" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey4Plan" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey5Plan" runat="server" Columns="6" BackColor="LightGray" />
                        <br />
                        <asp:Label ID="Label7" runat="server" CssClass="regText" Text="Item Description" /><br />
                        <asp:TextBox ID="txtNewPlanDesc" runat="server" CssClass="regText" TextMode="MultiLine"
                            Rows="5" Columns="60" BackColor="LightGray" /><br />
                        Associated Diagnosis:<br />
                        <asp:DropDownList ID="drpDiagnosis" runat="server" CssClass="regText"></asp:DropDownList><br />
                        <asp:Button ID="btnAddPlan" runat="server" CssClass="button" Text="Add  Plan Item"
                            OnClick="btnAddPlan_Click" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="dummy5" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="modEditDiag" BackgroundCssClass="ModalPopupBG" runat="server"
                CancelControlID="btnCancelEdit" TargetControlID="dummy5" PopupControlID="pnlEditDiag"
                Y="200" />
            <asp:Panel ID="pnlEditDiag" runat="server" CssClass="modalPopup" Width="450px" DefaultButton="btnCancelEdit">
                <div class="popup_Container">
                    <div class="popup_Titlebar" id="Div4">
                        <div class="TitlebarLeft" id="Div5" runat="server">
                            Edit Diagnosis
                        </div>
                    </div>
                    <div class="popup_Body">
                        <asp:Label ID="lblDiagID" runat="server" Style="display: none;" />
                        <asp:Label ID="Label4" runat="server" CssClass="regText" Text="Diagnosis Name" />
                        <br />
                        <asp:TextBox ID="txtEditDiagName" runat="server" BackColor="LightGray" /><br />
                        <asp:Label ID="Label5" runat="server" CssClass="regText" Text="ICD Code" /><br />
                        <asp:TextBox ID="txtEditCode" runat="server" CssClass="regText" BackColor="LightGray" />
                        <br />
                        Key Words used for searching:<br />
                        <asp:TextBox ID="txtKey1DiagEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey2DiagEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey3DiagEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey4DiagEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey5DiagEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <br />
                        <asp:Label ID="Label6" runat="server" CssClass="regText" Text="Diagnosis Description" /><br />
                        <asp:TextBox ID="txtEditDiagDesrcip" runat="server" CssClass="regText" TextMode="MultiLine"
                            Rows="5" Columns="60" BackColor="LightGray" /><br />
                    </div>
                    <div class="popup_Buttons">
                        <asp:Button ID="btnEditDiagOK" runat="server" CssClass="button" Text="Save" OnClick="btnEditDiagOK_Click" OnClientClick="return ValidateDiagnosis()" />
                        <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CssClass="button" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Button ID="dummy" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="modEditPlan" BackgroundCssClass="ModalPopupBG" runat="server"
                CancelControlID="btnCancelEdit" TargetControlID="dummy" PopupControlID="pnlEditPlan"
                Y="200" />
            <asp:Panel ID="pnlEditPlan" runat="server" CssClass="modalPopup" Width="450px" DefaultButton="btnCancelEditPlan">
                <div class="popup_Container">
                    <div class="popup_Titlebar" id="Div1">
                        <div class="TitlebarLeft" id="Div2" runat="server">
                            Edit Plan
                        </div>
                    </div>
                    <div class="popup_Body">
                        <asp:Label ID="lblPlanID" runat="server" Style="display: none;" />
                        <asp:Label ID="Label8" runat="server" CssClass="regText" Text="Item Name" />
                        <asp:Label ID="LabelMessageEditPlan" runat="server" Visible="false" ForeColor="Red" />
                        <br />
                        <asp:TextBox ID="txtEditPlanName" runat="server" BackColor="LightGray" /><br />
                        Category<br />
                        <asp:RadioButtonList ID="rdoEditPlanCategory" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow" CssClass="regText">
                            <asp:ListItem Text="Diet" />
                            <asp:ListItem Text="Exercise" />
                            <asp:ListItem Text="Other" />
                        </asp:RadioButtonList>
                        <br />
                        Key Words used for searching:<br />
                        <asp:TextBox ID="txtKey1PlanEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey2PlanEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey3PlanEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey4PlanEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <asp:TextBox ID="txtKey5PlanEdit" runat="server" Columns="6" BackColor="LightGray" />
                        <br />
                        <asp:Label ID="Label9" runat="server" CssClass="regText" Text="Item Description" /><br />
                        <asp:TextBox ID="txtEditPlanDescrip" runat="server" CssClass="regText" TextMode="MultiLine"
                            Rows="5" Columns="60" BackColor="LightGray" /><br />
                        Associated Diagnosis:<br />
                        <asp:DropDownList ID="drpEditDiagPlan" runat="server" CssClass="regText"></asp:DropDownList><br />
                    </div>
                    <div class="popup_Buttons">
                        <asp:HiddenField ID="HiddenFieldEditPlan" runat="server" />
                        <asp:HiddenField ID="HiddenFieldEditCat" runat="server" />
                        <asp:Button ID="btnEditPlanOK" runat="server" CssClass="button" Text="Save" OnClick="btnEditPlanOK_Click" OnClientClick="return CheckData()" />
                        <asp:Button ID="btnCancelEditPLan" runat="server" Text="Cancel" CssClass="button" />

                    </div>
                </div>
            </asp:Panel>

            <asp:Button ID="dummy6" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="modMissing" BackgroundCssClass="ModalPopupBG" runat="server"
                CancelControlID="btnCancel" TargetControlID="dummy6" PopupControlID="pnlMissing"
                Y="200" />
            <asp:Panel ID="pnlMissing" runat="server" CssClass="modalPopup" Width="250px" DefaultButton="btnCancelEdit">
                <div class="popup_Container">
                    <div class="popup_Titlebar" id="Div3">
                        <div class="TitlebarLeft" id="Div6" runat="server">
                            Msissing Entries
                        </div>
                    </div>
                    <div class="popup_Body">
                        <p class="PageTitle">
                            Some required entries are missing.
                        </p>
                    </div>
                    <div class="popup_Buttons">
                        <asp:Button ID="btnCancel" runat="server" Text="Continue" CssClass="button" />
                    </div>
                </div>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hdnDiagnosis" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div>

      
        <script type="text/javascript">


            function ValidateDiagnosis() {
                var diagName = $('#<%= txtEditDiagName.ClientID %>').val();
                var iCDCode = $('#<%= txtEditCode.ClientID %>').val();
                var diagnosisID = $('#<%= lblDiagID.ClientID %>').text();
                var isFlag = false;
                //var formattedValue = diagName.replace(/'/g, "&#39;");

                url = '<%=Page.ResolveUrl("~/admin_dictation.aspx/validateDiagnosisPlan")%>';
                $.ajax({
                    type: "POST",
                    url: url,
                    async: false,
                    data: "{diagnosisID:'" + diagnosisID + "', diagName:'" + htmlEncode(diagName) + "', iCDCode:'" + iCDCode + "'}",

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d != '') {
                            alert("You can not input the duplicate diagnose '" + result.d + "'!");
                            isFlag = false;
                        }
                        else {
                            isFlag = true;
                        }
                    },
                    error: function (obj) {

                        alert(obj.responseText);
                    }
                });
                if (isFlag == false) {
                    return false;
                }
                else {
                    return true;
                }
            }


            function CheckData() {

                var planName = $('#<%= txtEditPlanName.ClientID %>').val();                
                var hiddenValue = $('#<%= HiddenFieldEditPlan.ClientID %>').val();
                var category = $("input[type='radio']:checked").val();
                var hiddenCat = $('#<%= HiddenFieldEditCat.ClientID %>').val();
                var description = $('#<%= txtEditPlanDescrip.ClientID %>').val();
                var planId = $('#<%= lblPlanID.ClientID %>').text();
                var returnVal = false;

                url = '<%=Page.ResolveUrl("~/admin_dictation.aspx/checkExistingRecord")%>';
                $.ajax({
                    type: "POST",
                    url: url,
                    async: false,
                    data: "{category:'" + category + "', planName:'" + htmlEncode(planName) + "', PlanId:'" + planId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d != '') {
                            alert("You can not input the duplicate '" + result.d + "' data within category '" + category + "'!");
                            returnVal = false;
                        }
                        else {
                            returnVal = true;
                        }
                    },
                    error: function (obj) {
                        alert(obj.responseText);
                    }
                });


                if (returnVal == false) {
                    return false;
                }
                else {
                    return true;
                }

            }



            function ConfirmDelete() {

                $('#<%= pnlEditDiag.ClientID %>').hide();
                $('#<%= pnlEditPlan.ClientID %>').hide();
                $('#<%= pnlMissing.ClientID %>').hide();
                if (confirm("Do you want to delete diagnosis")) {

                    var hiddenValue = $('#<%= hdnDiagnosis.ClientID %>').val();

                    url = '<%=Page.ResolveUrl("~/admin_dictation.aspx/DeleteRecordDiag")%>';
                    var options = {
                        type: "POST",
                        url: url,
                        data: "{DiagnosisID:'" + hiddenValue + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            $('#<%= pnlMissing.ClientID %>').show();
                            $('#<%= pnlEditDiag.ClientID %>').show();
                            $('#<%= pnlEditPlan.ClientID %>').show();
                            window.location.reload();

                        },
                        error: function (obj) {
                            alert(obj.responseText);
                        }
                    };
                    $.ajax(options);
                    return true;
                }
                else {


                    return false;
                }
            }


            function ConfirmDeletePlan() {

                $('#<%= pnlEditDiag.ClientID %>').hide();
                $('#<%= pnlEditPlan.ClientID %>').hide();
                $('#<%= pnlMissing.ClientID %>').hide();
                if (confirm("Do you want to delete plan")) {

                    var hiddenValueDiag = $('#<%= hdnDiagnosis.ClientID %>').val();
                    var hiddenValuePlan = $('#<%= HiddenFieldEditPlan.ClientID %>').val();
                    var hiddenCat = $('#<%= HiddenFieldEditCat.ClientID %>').val();

                    url = '<%=Page.ResolveUrl("~/admin_dictation.aspx/DeleteRecordPlan")%>';
                    var options = {
                        type: "POST",
                        url: url,
                        data: "{DiagnosisID:'" + hiddenValueDiag + "', PlanID:'" + hiddenValuePlan + "',IsDiag:'" + hiddenCat + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            $('#<%= pnlMissing.ClientID %>').show();
                            $('#<%= pnlEditDiag.ClientID %>').show();
                            $('#<%= pnlEditPlan.ClientID %>').show();
                            window.location.reload();

                        },
                        error: function (obj) {
                            alert(obj.responseText);
                        }
                    };
                    $.ajax(options);
                    return true;
                }
                else {


                    return false;
                }
            }

        </script>

    </div>
</asp:Content>
