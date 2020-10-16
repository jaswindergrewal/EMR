<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sub.master" CodeFile="intake_form_surgeries.aspx.cs" Inherits="intake_form_surgeries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        var PatientID = '<%= PatientID %>';
        var FormID = '<%= FormID %>';
        $("document").ready(function () {

            BindData();
        });

        //bind  surgeries
        //jaswinder 26th aug 2013
        function BindData() {
            var postData = new Object();
            postData.PatientID = PatientID;
            $.ajax({
                type: "POST",
                url: "intake_form_surgeries.aspx/BindData",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#RecentList").find("tr:gt(0)").remove();
                    if (listObj != null) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#RecentList tr:eq(0)").after("<tr><td><strong>" + listObj[i].other_surgeries + "</strong></td></tr>" +
                                "<tr><td> Date entered:</td><td>" + convertDate(listObj[i].Date_Entered) + "</td></tr>" +
                                  "<tr><td> Date of surgery:</td><td>" + convertDate(listObj[i].other_date) + "</td></tr>" +
                                "<tr><td> Reason:</td><td >" + lineBreaks(listObj[i].other_reason, 50) + "</td></tr><tr style='border: 1px solid #646464;'><td></td></tr>");
                        }

                    }

                }
            });
        }

       
        //insert the surgies details for the patients
        //jaswinder 26th aug 2013
        function SaveData() {

            var surgery_name = $('[id$=surgery_name]').val();
            var surgery_date = $('[id$=txtSurgeryDate]').val();
            var surgery_reason = $('[id$=surgery_reason]').val();


            if (surgery_name == "") {
                alert("Please enter surgery name");
                $('[id$=surgery_name]').focus();
                return false;
            }
            else if (surgery_date == "") {
                alert("Please enter surgery date");
                $('[id$=txtSurgeryDate]').focus();
                return false;
            }
            var postData = new Object();
            postData.surgery_name = surgery_name;
            postData.surgery_date = surgery_date;
            postData.surgery_reason = surgery_reason;
            postData.PatientID = PatientID;
            postData.FormID = FormID;

            $.ajax({
                type: "POST",
                url: "intake_form_surgeries.aspx/InsertSurgeryDetails",
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
                        $('[id$=surgery_reason]').val('');
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
            <td valign="top">
                <table width="500" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
                    <tr>
                        <td>
                            <table width="500" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                                <tr class="regText">
                                    <td><strong>Type of Surgery </strong></td>
                                    <td>
                                        <input name="surgery_name" type="text" class="FormField" id="surgery_name" size="50" maxlength="50" /></td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Date of Surgery </strong></td>
                                    <td>

                                        <asp:TextBox ID="txtSurgeryDate" runat="server" CssClass="FormField" size="35"></asp:TextBox>
                                        (mm/dd/yyyy) 
                                         <cc1:CalendarExtender ID="CtrSurgeryDate" runat="server" TargetControlID="txtSurgeryDate" />
                                    </td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Reason</strong></td>
                                    <td>
                                        <textarea name="surgery_reason" cols="50" rows="3" class="border" id="surgery_reason" maxlength="200"></textarea></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="500" border="0" cellpadding="10" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div align="center">


                                <input type="button" class="button" value="Add Surgery" onclick="SaveData()" />
                                &nbsp;&nbsp;
                                 <asp:Button ID="btnNext" runat="server" class="button" Text="Next page" OnClick="btnNext_Click" />

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">&nbsp;</td>
            <td valign="top">
                <table border="0" cellpadding="5" cellspacing="0" bordercolor="#666666" bgcolor="#E7D5B4" class="border" id="RecentList">
                    <tr>
                        <td width="200px" colspan="2" bgcolor="#D6B781"><span class="regText"><strong>List of Surgeries</strong></span></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
