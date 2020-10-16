<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="supplement_history.aspx.cs" Inherits="supplement_history" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .rxNotes {
            position: absolute;
            z-index: 100;
            top: -999px;
            left: -999px;
            visibility: hidden;
            display: none;
            width: 400px;
            height: 150px;
            background-color: #ffffff;
            border: 2px solid #000000;
            padding-left: 4px;
            padding-right: 4px;
            padding-top: 2px;
            padding-bottom: 2px;
        }
    </style>


    <script type="text/javascript">
        var PatientID = '<%= PatientID %>';
        $("document").ready(function () {

            BindData1();
        });               
        //function to fetch supplement history by patient Id
        function BindData1() {
            var postData = new Object();
            postData.PatientID = PatientID;
            $.ajax({
                type: "POST",
                url: "supplement_history.aspx/BindData",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;

                    if (listObj != null) {
                        var drugName = "";

                        for (var i = 0 ; i < listObj.length; i++) {
                            if (drugName != listObj[i].DrugName) {
                                drugName = listObj[i].DrugName;
                                $("#SupplementLayer1").append('<table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#D6B781" >' +
                                   "<tr bgcolor='#D6B781'>" +
                              "<td colspan='2'><p><strong>" + drugName + "</strong></p>" +
                               "</td></tr></table>");
                            }

                            if (listObj[i].Closed_YN != 0) {
                                $("#SupplementLayer1").append("<div style='width:270 px;padding-left:6px; overflow-x: auto;'>" +
                                    "<table border='0'  cellpadding='6' cellspacing='0' class='border'>" +
                                    "<tr>" +
                                    "<td width='270'>" +
                                    "<font color='999999'>" +  
                                    "<strong>Date </strong>: " +  convertDate(listObj[i].DateEntered)+ "<br>" + 
                                    "<strong>Supplement Dose</strong>: " + listObj[i].SuppDose+ "<br>" +
                                    "<strong># of Refills</strong>: " + listObj[i].Drug_NumbRefills + "<br>" +
                                    "<strong>Prescribed By</strong>: " + listObj[i].EmployeeName + "<br>" +
                                    "</font>" +
                                    "</td><td>&nbsp; &nbsp;</td></tr>" +
                                    '</table></div><br>');
                            }
                            else {

                                $("#SupplementLayer1").append("<div style='width:270 px;padding-left:6px; overflow-x: auto;'>" +
                                    "<table border='0' cellpadding='6' cellspacing='0' class='border'>" +
                                    "<tr>" +
                                    "<td width='270'>" + 
                                     "<strong>Date </strong>: "+  convertDate(listObj[i].DateEntered)+ "<br>" +
                                        "<strong>Supplement Dose</strong>: " + listObj[i].SuppDose+ "<br>" +
                                    "<strong># of Refills</strong>:  " + listObj[i].Drug_NumbRefills + "<br>" +
                                    "<strong>Prescribed By</strong>:  " + listObj[i].EmployeeName + "<br>" +
                                    "</td><td>&nbsp; &nbsp;</td></tr>" +
                                    '</table></div><br>');

                            }

                        }

                    }

                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="PatientName" style="position: absolute; z-index: 7; left: 8; top: 32; visibility: visible; width: 648px;">
        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">

                <td nowrap bgcolor="#D6B781">
                    <div align="right">
                        <input name="Button" type="button" class="button" onclick="MM_goToURL('self','PatientInfo.aspx?PatientID=<%=PatientID%>    ');return document.MM_returnValue" value="Back to Patient Profile">
                        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('self','./PresrcriptionList.aspx?patientid=<%=PatientID %>    &MasterPage=sub.master');return document.MM_returnValue" value="Back to Prescriptions">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="SupplementLayer1" style="position: absolute; z-index: 2; top: 72px; width: 706px; background-color: #D6B781;">
    </div>
    <div class="rxNotes"></div>
</asp:Content>

