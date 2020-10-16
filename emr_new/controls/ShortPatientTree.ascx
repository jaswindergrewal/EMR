<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShortPatientTree.ascx.cs" Inherits="Controls_ShortPatientTree" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups" TagPrefix="obout" %>

<script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
<script type="text/javascript">



</script>
<div id="PatientName" style="width: 100%;">

    <p>
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PnlContent"
            ExpandControlID="pnlTitle" CollapseControlID="pnlTitle" TextLabelID="Label1"
            CollapsedText="Click to Show Notes and Alerts" ExpandedText="Click to Hide Notes and Alerts"
            Collapsed="True" SuppressPostBack="true" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
            CollapsedImage="~/images/expand.jpg">
        </cc1:CollapsiblePanelExtender>
        <asp:Panel ID="pnlTitle" runat="server" CssClass="border">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0"></asp:Image>
            <asp:Label ID="Label1" runat="server" Text="Show Notes and Alerts" CssClass="regText">
            </asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="borderPanel">


            <table cellpadding="2" cellspacing="2" height="325px">
                <tr>
                    <td width="48%">
                        <table>
                            <tr>
                                <td><strong>Notes</strong></td>
                                </tr>
                            <tr>
                                <td>
                                    <obout:Editor ID="edContent" runat="server" Height="240px" Width="450px" Enabled="false">
                                        <TopToolbar Appearance="Custom">
                                            <AddButtons>
                                                <obout:Bold />
                                                <obout:Italic />
                                                <obout:Underline />
                                                <obout:StrikeThrough />
                                                <obout:HorizontalSeparator />
                                                <obout:FontName />
                                                <obout:FontSize />
                                                <obout:VerticalSeparator />
                                                <obout:Undo />
                                                <obout:Redo />
                                                <obout:HorizontalSeparator />
                                                <obout:PasteWord />
                                                <obout:HorizontalSeparator />
                                                <obout:JustifyLeft />
                                                <obout:JustifyCenter />
                                                <obout:JustifyRight />
                                                <obout:JustifyFull />
                                                <obout:HorizontalSeparator />
                                                <obout:SpellCheck />

                                            </AddButtons>
                                        </TopToolbar>
                                    </obout:Editor>

                                </td>
                                </tr>
                            <tr>
                                <td><%--<asp:TextBox ID="txtDiscount" runat="server" TextMode="MultiLine" Width="262px" CssClass="regText" Rows="6" /><br />--%>
                                    <asp:Button ID="btnEditDiscount" runat="server" CssClass="button" OnClick="btnEditDiscount_Click"
                                        Text="Edit" />&nbsp;
								<asp:Button ID="btnSaveDiscount" runat="server" Text="Save" OnClick="btnSaveDiscount_Click"
                                    CssClass="button" Enabled="false" /></td>
                            </tr>
                        </table>






                    </td>
                    <%--<td width="262px">
								<strong>Notes</strong><br />
								<asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="262px" CssClass="regText" Rows="6"/><br />
								<asp:Button ID="btnEditNote" runat="server" CssClass="button" OnClick="btnEditNote_Click"
									Text="Edit" />&nbsp;
								<asp:Button ID="btnSaveNote" runat="server" Text="Save" OnClick="btnSaveNote_Click"
									CssClass="button" Enabled="false" />
							</td>--%>
                    <td width="2%">
                        <asp:Label ID="lblOrderPending" ForeColor="Red" runat="server" Text="Order Generated, awaiting fulfillment."
                            Visible="false" Width="262px" />
                        <asp:Label ID="lblInactive" ForeColor="Red" runat="server" Text="This patient is inactive!" Visible="false" />
                    </td>
                    <td width="48%">
                        <table>
                            <tr>
                                <td>
                                     <strong>Hot Notes</strong><br />
                                </td>
                                </tr>
                         <tr>
                           
                       <td>

                        <obout:Editor ID="edHotNotes" runat="server" Height="240px" Width="450px" Enabled="false">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>

</td>
                             </tr>
                            <tr>
                                <td>
                        <asp:Button ID="btnEditHoteNote" runat="server" CssClass="button" OnClick="btnEditHoteNote_Click"
                            Text="Edit" />&nbsp;
								<asp:Button ID="btnSaveHotNotes" runat="server" Text="Save" OnClick="btnSaveHotNotes_Click"
                                    CssClass="button" Enabled="false" />
                                    </td>
                                 </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </asp:Panel>
    </p>
</div>


