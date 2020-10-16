<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowUps.aspx.cs" Inherits="FollowUps"
    MasterPageFile="Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
        //function to show loader
        function ShowProgress() {
            setTimeout(function () {
                $("#loading-div-background").show();
               
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
            return false;
        });
   
        //validate  for empty checkbox
        function ValidateFollowUP() {

            {
                var loTable = document.all("<%=FollowupGrid.ClientID%>");
                count = 0;
                with (document.forms[0]) {
                    for (var i = 0; i < elements.length; i++) {
                        var e = elements[i];
                        if (e.type == "checkbox" && e.checked == true) {
                            count += 1;
                        }
                    }
                }
                if (count == 0) {
                    
                    alert("please select at least one followups.");
                 
                    return false;
                }
                else {
                    return true;
                }

            }
        }
    </script>

    <asp:UpdatePanel ID="pnlFup" runat="server">
        <ContentTemplate>
            <div>
                <h4>Manage Followups</h4>
                <asp:Button ID="btnComplete" runat="server" Text="Complete all checked" CssClass="button"
                    OnClick="btnComplete_Click" OnClientClick="return ValidateFollowUP();" />
                <asp:GridView ID="FollowupGrid" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" AllowPaging="true" PageSize="20" AutoGenerateColumns="true"
                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No Followups Found." OnPageIndexChanging="FollowupGrid_PageIndexChanging"
                    AllowSorting="True" DataKeyNames="ID,Patient,Start,End,AppointmentType,Provider,Result,HomePhone,HomeHIPAA,WorkPhone, WorkHiPAA,CellPhone, CellHIPAA,Notes" OnSorting="FollowupGrid_Sorting" OnRowDataBound="FollowupGrid_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Complete" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbComplete" runat="server" Checked="false" />
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>

                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </div>

            <div id="loading-div-background">
                <div id="loading-div" class="ui-corner-all">
                    <img src="images/indicator.gif" alt="Loading.." />
                    <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
