<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_prescriptions.aspx.cs" Inherits="intake_form_prescriptions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        var PatientID = '<%= PatientID %>';
        var FormID = '<%= FormID %>';
        $("document").ready(function () {

            BindData();
        });

        //bind data for prescription
        //jaswinder 26th aug 2013
        function BindData() {
            var postData = new Object();
            postData.PatientID = PatientID;
            $.ajax({
                type: "POST",
                url: "intake_form_prescriptions.aspx/BindData",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#RecentList").find("tr:gt(0)").remove();
                    if (listObj != null) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#RecentList tr:eq(0)").after("<tr><td><strong>" + listObj[i].Display + "</strong</td></tr>" +
                                "<tr><td> Date Entered:</td><td>" + convertDate(listObj[i].Date_Entered) + "</td></tr>" +
                                  "<tr><td> Medication Type:</td><td>" + listObj[i].drugtype + "</td></tr>" +
                                "<tr><td> Dosage:</td><td>" + listObj[i].dosage + "</td></tr><tr></tr>");
                        }

                    }

                }
            });
        }

        //save data for prescription
        //jaswinder 26th aug 2013
        function SaveData() {

            var DrugID = $("[id*='ddlDrug'] :selected").val();
            var medication = $('[id$=medication]').val();
            var dosage = $('[id$=dosage]').val();


            if (medication == "") {
                alert("Please enter medication name");
                $('[id$=medication]').focus();
                return false;
            }

            var postData = new Object();
            postData.DrugID = DrugID;
            postData.medication = medication;
            postData.dosage = dosage;
            postData.PatientID = PatientID;
            postData.FormID = FormID;

            $.ajax({
                type: "POST",
                url: "intake_form_prescriptions.aspx/InsertPrescriptionDetails",
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
            <td valign="top">
                <table width="400" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
                    <tr>
                        <td>
                            <table width="400" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                                <tr class="regText">
                                    <td><strong>Medication</strong></td>
                                    <td>
                                        <asp:DropDownList ID="ddlDrug" runat="server" CssClass="FormField" Width="180px"></asp:DropDownList></td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Other Medication (not listed) </strong></td>
                                    <td>
                                        <input name="medication" type="text" class="FormField" id="medication" maxlength="50" />
                                    </td>
                                </tr>
                                <tr class="regText">
                                    <td><strong>Dosage of Med. Indicated</strong></td>
                                    <td>
                                        <input name="dosage" type="text" class="FormField" id="dosage" maxlength="25" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="400" border="0" cellpadding="10" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div align="center">

                                <asp:Button ID="btnSubmit" runat="server" class="button" Text="Add Prescription" OnClientClick="return SaveData();" />
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
                        <td width="300" colspan="2" bgcolor="#D6B781"><span class="regText"><strong>List of Precscriptions</strong></span></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
