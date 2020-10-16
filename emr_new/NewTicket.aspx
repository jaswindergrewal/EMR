<%@ Page Title="New Ticket" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="NewTicket.aspx.cs" Inherits="NewTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="Scripts/jquery-1.7.2.js"></script>
    <script type="text/javascript">
        function validate() {
            var mess = "";
            var isFocus = "";
            var isValid = true;
            var subject = document.getElementById('<%=txtSubject.ClientID%>');
            var Dropdown = $('#<%=ddlAssign.ClientID %>');
            var editor = oboutGetEditor('ed').getContent();
            var targlist = "";
            var rbList = document.getElementById('<%= rdoDept.ClientID %>');
            var rbCount = rbList.getElementsByTagName("input");

            for (var i = 0; i < rbCount.length; i++) {
                if (rbCount[i].checked == true) {
                    targlist = rbCount[i].value;
                }
            }
            if (targlist == 'Emp') {
                targlist = "";
                targlist = "Employee";
            }
            else {
                targlist = "";
                targlist = "department";
            }
            if (Dropdown.val() == "0") {
                if (mess == "")
                { isFocus = Dropdown; isFocus.focus(); }
                $('#<%=ddlAssign.ClientID %>').addClass("dropDownAddFousClass");
                $('#<%=ddlAssign.ClientID %>').removeClass("dropDownRemoveFousClass");

                mess += "Please select assign to " + targlist + " name.";
            }
            else {
                $('#<%=ddlAssign.ClientID %>').removeClass("dropDownAddFousClass");
                $('#<%=ddlAssign.ClientID %>').addClass("dropDownRemoveFousClass");
            }

            if (subject.value == "") {
                if (mess == "")
                { isFocus = subject; isFocus.focus(); }
                mess += "\r\nPlease enter subject.";
            }
            if (editor == "") {
                mess += "\r\nPlease enter description.";
            }
            if (mess != "") {
                alert(mess);
                isValid = false;
            }
            return isValid;
        }

        $(function () {
            $('[id*=rdoDept] input').unbind().click(function (e) {
                var val = $('[id*=rdoDept]').find('input:checked').val();
                if (val == "Emp") {
                    BindAssignDropDown('Employee');
                }
                else {
                    BindAssignDropDown('Department');
                }
            });
        });

        $(document).ready(function () {
            var IsAutoshipTicket = document.getElementById('<%=hdnIsAutoshipTicket.ClientID%>');
            if (IsAutoshipTicket.value == "True") {
                BindAssignDropDown('Department');
            }
            else {
                BindAssignDropDown('Employee');
            }
        });

        function BindAssignDropDown(type) {
            var URL = "";
            $('#<%=ddlAssign.ClientID %>').removeClass("dropDownAddFousClass");
            $('#<%=ddlAssign.ClientID %>').addClass("dropDownRemoveFousClass");

            if (type == "Employee") {
                URL = "NewTicket.aspx/BindEmployee";
            }
            else
                if (type == "Department") {
                    URL = "NewTicket.aspx/BindDepartment";
                }

            $.ajax({
                type: "POST",
                url: URL,
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var Dropdown = $('#<%=ddlAssign.ClientID %>');
                    Dropdown.empty();
                    Dropdown.append($("<option></option>").val(0).html("Select"));

                    $.each(response.d, function (index, item) {
                        if (type == "Employee") {
                            Dropdown.append($("<option></option>").val(item.EmployeeID).html(item.EmployeeName));
                        }
                        else
                            if (type == "Department") {
                                Dropdown.append($("<option></option>").val(item.DepartmentID).html(item.DepartmentName));
                                var IsAutoshipTicket = document.getElementById('<%=hdnIsAutoshipTicket.ClientID%>');
                                 if (IsAutoshipTicket.value == "True") {
                                     $('#<%=ddlAssign.ClientID %>').val('30');
                                 }
                             }
                     });
                },
                error: function () {
                    alert("Failed to load data");
                }
            });
        }


    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div>
        <input type="hidden" id="inpPatientID" runat="server" />
        <input type="hidden" id="hdnIsAutoshipTicket" runat="server" />
        <br />
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="4">
                    <p>
                        <b>New Customer Ticket</b>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="regText">
                    <strong>Ticket Type </strong>
                </td>
                <td>
                    <asp:DropDownList ID="AptType" CssClass="FormField" runat="server" />
                </td>
                <td>
                    <strong>Assign to</strong><span class="Validation_StarMark_Color">*</span>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoDept" runat="server"
                        RepeatDirection="Horizontal" AutoPostBack="false">
                        <asp:ListItem Text="Employee" Value="Emp" Selected="True" />
                        <asp:ListItem Text="Department" Value="Dept" />
                    </asp:RadioButtonList>


                    <asp:DropDownList ID="ddlAssign" runat="server" Width="150" CssClass="FormField" onchange="SetDropDownBorder(this);" />

                </td>
            </tr>
            <tr>
                <td class="regText">Severity
                </td>
                <td class="regText" colspan="2">
                    <asp:RadioButtonList ID="rdoSeverity" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Severe" Value="1" />
                        <asp:ListItem Text="Normal" Value="2" Selected="True" />
                        <asp:ListItem Text="Low Priority" Value="3" />
                    </asp:RadioButtonList>
                </td>
                <td class="regText">
                    <strong>Due Date</strong>
                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="regText readOnly" Text="" /><cc1:CalendarExtender
                        ID="calDue" runat="server" TargetControlID="txtDueDate" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td valign="top" style="width: 10px;">
                    <asp:Label ID="lblSubject" runat="server" Text="Subject:" CssClass="regText" /><span class="Validation_StarMark_Color">*</span>
                    <asp:TextBox ID="txtSubject" runat="server" />

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        CssClass="button" OnClientClick="return validate();" />
                    <obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
                        ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14"
                        Submit="false" AjaxWait="true" Height="200" Width="600" SpellCheckAutoComplete="true"
                        InitialCleanUp="false">

                        <Buttons>
                            <obout:Toggle Name="Bold" />
                            <obout:Toggle Name="Italic" />
                            <obout:Toggle Name="Underline" />
                            <obout:Toggle Name="StrikeThrough" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="ClearStyles" />
                            <obout:HorizontalSeparator />
                            <obout:Select Name="FontName" />
                            <obout:Select Name="FontSize" />
                            <obout:HorizontalSeparator />
                            <obout:VerticalSeparator />
                            <obout:Method Name="Undo" />
                            <obout:Method Name="Redo" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="PasteWord" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="JustifyLeft" />
                            <obout:Method Name="JustifyCenter" />
                            <obout:Method Name="JustifyRight" />
                            <obout:Method Name="JustifyFull" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="SpellCheck" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="IncreaseHeight" />
                            <obout:Method Name="DecreaseHeight" />
                            <obout:TextIndicator />
                        </Buttons>

                    </obout:Editor>

                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function SetDropDownBorder(dropDownId) {

            if (dropDownId.value > 0) {
                $(dropDownId).removeClass("dropDownAddFousClass");
                $(dropDownId).addClass("dropDownRemoveFousClass");

            }
            else {
                $(dropDownId).removeClass("dropDownRemoveFousClass");
                $(dropDownId).addClass("dropDownAddFousClass");
            }
        }
    </script>
</asp:Content>
