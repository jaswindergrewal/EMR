<%@ Page Title="CRM :: Add Prospect Data" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeFile="AddProspect.aspx.cs" Inherits="CRM_AddProspect" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.filter_input.js" type="text/javascript"></script>
    <script type="text/javascript">
        var checkFlag = false;
        function Validate() {
            var txtFirstName = document.getElementById('txtFirstName');
            var varLastName = document.getElementById('txtLastName');
            var txtEmail = document.getElementById('txtEmail');
            var txtPhone=document.getElementById('txtPhone');
            var IsFocus = "";
            var errString = "";
            if (txtFirstName.value == "") {
                if (errString == "")
                { IsFocus = txtFirstName }
                errString += "Please enter first name.";
            }
            if (varLastName.value == "") {
                if (errString == "")
                { IsFocus = varLastName }
                errString += "\r\nPlease enter last name.";
            }

            if (txtPhone.value == "")
            {
                if (errString == "")
                { IsFocus = txtPhone }
                errString += "\r\nPlease enter phone number.";
            }
            //if (txtEmail.value == "") {
            //    if (errString == "")
            //    { IsFocus = txtEmail }
            //    errString += "\r\nPlease enter email address.";
            //}

            if (txtEmail.value != "") {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                if (!filter.test(txtEmail.value)) {
                    if (errString == "")
                    { IsFocus = txtEmail }
                    errString += "\r\nPlease enter valid email address.";
                }
            }


            if (errString == "") {
                //ValidateProspect();
                //if (!checkFlag)
                //    return false;
                //else
                    return true;
            }
            else {
                alert(errString);
                IsFocus.focus();
                return false;
            }
        }

        // this method is using for check the duplicate Email address during Add/Edit the record.
        // Tab: Manage Prospect
        function ValidateProspect() {
            var emailAddress = document.getElementById('txtEmail').value;
            if (emailAddress != "") {
                var isFlag = false;
                var prospectID = "0";
                url = '<%=Page.ResolveUrl("~/CRM/Manage.aspx/CheckDuplicateProspect")%>';
                $.ajax({
                    type: "POST",
                    url: url,
                    async: false,
                    data: "{prospectID:'" + prospectID + "', emailAddress:'" + emailAddress + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d != '') {
                            alert("Record is already exist with this email address('" + emailAddress + "').");
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
                    checkFlag = false;
                    return false;
                }
                else {
                    checkFlag = true;
                    return true;
                }
            }
            else {
                checkFlag = true;
                return true;
            }

        }

        $(function () {
            $('#<%= txtPhone.ClientID %>').mask("999-999-9999");
            $('#<%= txtZip.ClientID %>').filter_input({ regex: '[0-9]' });
        });

    </script>

    <p class="PageTitle">Add prospect data</p>
    <table cellpadding="5" cellspacing="3" width="90%" class="border" style="margin-left: 20px;">

        <tr>
            <td class="FormLabel" style="text-align: right; width: 100px">First Name:<span style="color: red">*</span>
            </td>
            <td>

                <asp:TextBox ID="txtFirstName" runat="server" CssClass="FormField" ClientIDMode="Static" />

            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Last Name:<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="FormField" ClientIDMode="Static" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Phone:<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="FormField" ClientIDMode="Static"/>
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Email:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="FormField" ClientIDMode="Static" /><br />

            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Seminar:
            </td>
            <td>
                <asp:DropDownList ID="ddlSeminar" runat="server" DataTextField="EventName"
                    DataValueField="EventID" CssClass="FormField" />

            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Address:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="FormField" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">City:
            </td>
            <td>
                <asp:TextBox ID="txtCity" runat="server" CssClass="FormField" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">State:
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="FormField" ID="ddlState">
                    <asp:ListItem Value="">Please select</asp:ListItem>
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="DC">District Of Columbia</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="text-align: right;">Zip:
            </td>
            <td>
                <asp:TextBox ID="txtZip" runat="server" CssClass="FormField" MaxLength="6" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel" style="font-style: italic; text-align: right;">Marketing Source:
            </td>

            <td>
                <asp:DropDownList ID="ddlHowHear" runat="server" DataTextField="MarketingSourceName"
                    DataValueField="MarketingSourceID" CssClass="FormField" />

            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td></td>
            <td>

                <asp:Button ID="btnAddContact" runat="server" Text="Submit" CssClass="button" OnClick="btnAddContact_Click" OnClientClick="return Validate();" />
            </td>
        </tr>

    </table>

</asp:Content>
