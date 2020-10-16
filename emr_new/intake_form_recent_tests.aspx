<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_recent_tests.aspx.cs" Inherits="intake_form_recent_tests" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        var PatientID = '<%= PatientID %>';
        var FormID = '<%= FormID %>';
        $("document").ready(function () {

            BindData();
        });

        //bind the vital data for paitients
        //jaswinder 14th aug 2013
        function BindData() {
            var postData = new Object();
            postData.PatientID = PatientID;
            $.ajax({
                type: "POST",
                url: "intake_form_recent_tests.aspx/BindData",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    $("#RecentList").find("tr:gt(0)").remove();
                    var listObj = response.d;
                    if (listObj != null) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#RecentList tr:eq(0)").after("<tr><td colspan='2'><strong>" + listObj[i].other_test + "</strong></td></tr>" +
                                "<tr><td> Date Entered:</td><td>" + convertDate(listObj[i].Date_Entered )+ "</td></tr>" +
                                  "<tr><td>Date of Test:</td><td>" + convertDate(listObj[i].other_date) + "</td></tr>" +
                                "<tr><td> Reason:</td><td>" + listObj[i].other_reason + "</td></tr>" +
                                "<tr><td> Result:</td><td>" + listObj[i].other_result + "</td></tr>");
                        }
                    }

                }
            });
        }

        //insert the vital details for the patients
        //jaswinder 14th aug 2013
        function SaveDate() {

            var test_name = $('[id$=test_name]').val();
            var test_date = $('[id$=test_date]').val();
            var test_reason = $('[id$=test_reason]').val();
            var test_result = $('[id$=test_result]').val();


            if (test_name == "") {
                alert("Please enter test name");
                $('[id$=test_name]').focus();
                return false;
            }
            else if (test_date == "") {
                alert("Please enter test date");
                $('[id$=test_date]').focus();
                return false;
            }
            var postData = new Object();
            postData.test_name = test_name;
            postData.test_date = test_date;
            postData.test_reason = test_reason;
            postData.test_result = test_result;
            postData.PatientID = PatientID;
            postData.FormID = FormID;

            $.ajax({
                type: "POST",
                url: "intake_form_recent_tests.aspx/InsertTestResult",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {

                    var result = response.d;
                    if (result == 0) {
                        alert("Data is not submitted succesfully there is some error occured!");
                        return false;
                    }
                    else {
                        BindData();
                        alert("Data entered successfully");
                        $('input[type=text]').each(function () {
                            $(this).val('');
                        });
                    }

                }

            });
            return false;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="900" border="0" cellpadding="10">
        <tr>

            <td>
                <p>
                    <span class="style1">Recent Tests </span>
                    <br>
                    Please complete the following if you have had any of these tests:
                </p>

            </td>
        </tr>

        <tr>
            <td valign="top">
                <table width="450" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
                    <tr>
                        <td>
                            <table width="500" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                                <tr class="regText">
                                    <td><strong>Test Name </strong></td>
                                    <td >
                                        <input name="test_name" type="text" class="FormField" id="test_name" size="50" maxlength="50"/></td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Date of Test </strong></td>
                                    <td>
                                         <asp:TextBox id="test_date" CssClass="FormField" runat="server" size="20" ></asp:TextBox>
                                        (mm/dd/yyyy) 
                                        <cc1:CalendarExtender ID="CtrSurgeryDate" runat="server" TargetControlID="test_date" />
                                    </td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Reason for Test</strong></td>
                                    <td>
                                        <input name="test_reason" type="text" class="FormField" id="test_reason" size="50" maxlength="50"/>
                                        (50 char. max) </td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Result of Test </strong></td>
                                    <td>
                                        <input name="test_result" type="text" class="FormField" id="test_result" size="50" maxlength="50"/>
                                        (50 char. max) </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="450" border="0" cellpadding="10" cellspacing="0">
                    <tr>
                        <td>
                            <div style="vertical-align: central;">


                                <asp:Button ID="btnSubmit" runat="server" Text="Add Test" class="button" OnClientClick="return SaveDate();" />
                                &nbsp;&nbsp;
                                 <asp:Button ID="btnNext" runat="server" class="button" Text="Next Page ->" OnClick="btnNext_Click" />

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">&nbsp;</td>
            <td valign="top">
                <table border="0" cellpadding="5" cellspacing="0" bordercolor="#666666" bgcolor="#E7D5B4" class="border" id="RecentList">
                    <tr>
                        <td width="200" bgcolor="#D6B781" colspan="2"><span class="regText"><strong>List of Recent Tests </strong></span></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>

</asp:Content>
