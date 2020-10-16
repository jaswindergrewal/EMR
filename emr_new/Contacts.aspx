<%@ Page Title="" Language="C#" MasterPageFile="~/LabSSRS/sub.master" AutoEventWireup="true"
    CodeFile="Contacts.aspx.cs" Inherits="Contacts" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="css/lmc_style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">


        var PAGE_SIZE;
        var count = 0;
        var currentPageNumber = 1;
        var cbocal = 3;
        var cboauto = 0;

        $("document").ready(function () {
            // Bind grid initially with page number as 1 

            PAGE_SIZE = 20;
            document.getElementById('rdBoth').checked = true;
            GetContactTypeValue(3);
            //GetAllContacts(1);
        });


        //Binds all contacts by patient id
        function GetAllContacts(PageIndex) {
            var txtEventDate = document.getElementById("txtDate");
            var dateFormatCheck = isValidDate(txtEventDate.value);
            if (dateFormatCheck == false) {
                alert("You must enter a valid date.");
                return;
            }

            else {

                var postData = new Object();
                var PID = '<%=PatientID%>';
                postData.PID = PID;
                postData.cboCalBox = cbocal;
                postData.CboAutoBox = cboauto;
                postData.PageSize = PAGE_SIZE;
                postData.PageIndex = PageIndex;
                postData.Contacttype = document.getElementById("drpContacttype").value;
                postData.txtEventDate = txtEventDate.value;


                $.ajax({
                    type: "POST",
                    url: "Contacts.aspx/GetAllContactsByPatientID",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {

                        var Contactlist = response.d;

                        $("#ContactsDiv").append('<table id ="tblContacts" width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#E7D5B4" ><tbody></tbody></table>');
                        $("#tblContacts tbody").append('<tr> <td align="center"></td></tr>');
                        if (Contactlist != null) {
                            for (var i = 0 ; i < Contactlist.length; i++) {
                                datetimeVar = convertDate(Contactlist[i].ContactDateEntered);
                                var ContactID = Contactlist[i].ContactID;
                                var msgBody = Contactlist[i].MessageBody;
                                $("#tblContacts tbody").append("<tr bgcolor='#E7D5B4' ><td style=''valign='top''><input type='hidden' value=" + ContactID + " class='HiddenContactID' /><a  href='#' class='UpdateContactClass'>" + datetimeVar + "</a>" + '  ' + "<a href='ContactPrint.aspx?contactID=" + ContactID + "' target='_blank'>[print]</a></td><td>" + msgBody + "</td></tr>");
                                count = Number(Contactlist[i].RecordCount);
                            }
                            if (Contactlist.length == 0) {
                                $("#tblContacts tbody").append("<tr bgcolor='#E7D5B4'><td >There is no contact record for this patient</td></tr>");
                            }


                            //For setting current page number and total page number
                            currentPageNumber = PageIndex;
                            var totalPages = 0;
                            if (Contactlist.length > 0) {
                                totalPages = Math.ceil(count / PAGE_SIZE);
                            }

                            $("#MainContent_lblCurrentPage").text(currentPageNumber);
                            $("#MainContent_lblTotalPages").text(totalPages);

                            //Enable or disable the previous or next button.
                            if (totalPages == 1) {

                                $("#MainContent_lblCurrentPage").hide();
                                $("#MainContent_lblTotalPages").hide();
                                $("#pagingtext").hide();
                                $("#tdButton").hide();
                                // $("#tdNoRecord").hide();

                                return;
                            }
                            else if (totalPages == 0) {

                                $("#MainContent_lblCurrentPage").hide();
                                $("#MainContent_lblTotalPages").hide();
                                $("#pagingtext").hide();
                                // $("#tdNoRecord").show();
                                $("#tdButton").hide();
                                return;
                            }
                            else {

                                $("#tdButton").show();
                                $("#MainContent_lblCurrentPage").show();
                                $("#MainContent_lblTotalPages").show();
                                $("#pagingtext").show();
                                //$("#tdNoRecord").hide();
                            }

                            if (currentPageNumber == 1) {
                                $("#Previous").attr("disabled", "disabled");
                                if ($("#MainContent_lblCurrentPage") > 0) {
                                    $("#Next").attr("disabled", "disabled");
                                }
                                else {
                                    $("#Next").removeAttr("disabled");
                                }
                            }

                            else {
                                $("#Previous").removeAttr("disabled");
                                if (currentPageNumber == $("#MainContent_lblTotalPages").text())
                                    $("#Next").attr("disabled", "disabled");
                                else
                                    $("#Next").removeAttr("disabled");

                            }


                        }

                    },
                    error: function (response) {
                        alert("error");
                    },
                    cache: false
                });
                $("#ContactsDiv").show();
                $("#ContactsDiv").empty();
            }
        }


        //Function when page number is change bind the grid
        function ChangePage(data) {
            var process = $(data).val();

            var page = Number($("#MainContent_lblCurrentPage").text());
            if (process == "Next") {
                GetAllContacts(page + 1);
            }
            if (process == "Previous") {
                GetAllContacts(page - 1);
            }
        }


        //$(document).on('click', "#cboCal", function () {
        //   // cbocal = $(this).is(":checked") ? 1 : 0;
        //    if (document.getElementById('gender_Male').checked) {
        //        //Male radio button is checked
        //    } else if (document.getElementById('gender_Female').checked) {
        //        //Female radio button is checked
        //    }
        //    GetAllContacts(1);
        //});

        function GetContactTypeValue(value)
        {
            cbocal = value;
            GetAllContacts(1);}

        //$(document).on('click', "#ContactRecords", function () {
        //   // cboauto = $(this).is(":checked") ? 1 : 0;
        //    if (document.getElementById('Calendar').checked) {
        //        cbocal = 1;
        //    } else if (document.getElementById('Autoship').checked) {
        //        cbocal=2
        //    }
        //    else
        //    { cbocal = 3}
        //    GetAllContacts(1);
        //});


        $(document).on('click', "a.AddContactlink", function () {
            ShowContactPopUp();
        });


        $(document).on('click', "#CancleButton", function () {
            HideContactPopUp();
        });


        function HideContactPopUp() {
            $("#ContactAddPopUp").hide();
            $("#SemiTransparentBG").hide();
        }

        function ShowContactPopUp() {
            $("#ContactAddPopUp").show();
            $("#SemiTransparentBG").show();
        }


        //=================================Add Contact block start=================================

        $(document).on('click', "#AddContactbtn", function () {
            var editorObject = $find("MainContent_ed");
            var _content = editorObject.get_content();
            if (_content == "") {
                alert('Please enter the content');
            }
            else {
                var apttype = $('#AptType option:selected').val();
                var patientID = '<%=PatientID%>';
                var editorObject = $find("<%= ed.ClientID %>");
                var ContactDesc = editorObject.get_content();
                var postData = new Object();
                postData.content = ContactDesc;
                postData.AptType = apttype;
                postData.PatientID = patientID;
                $.ajax({
                    type: "POST",
                    url: "Contacts.aspx/AddContactDetails",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        HideContactPopUp();
                        GetAllContacts(1);
                    },
                    error: function (response) {
                        alert("error");
                    },
                    cache: false


                });
            }

        });


        //=================================Add Contact block End=================================




        //=============================Update Contact block start====================================
        var ID;
        var descriptionVar;
        var datetimeVar;
        var enteredby;
        var firstname;
        var lastname;
        var content;


        $(document).on('click', "a.UpdateContactClass", function () {
            UpdateContactShowPopUp();

            var $tr = $(this).closest('tr');
            ID = $tr.find("input.HiddenContactID").val();

            var postData = new Object();
            postData.ContactID = ID;
            $.ajax({
                type: "POST",
                url: "Contacts.aspx/GetContactDetails",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var Contactlist = response.d;
                    if (Contactlist != null) {
                        descriptionVar = Contactlist.AptTypeDesc;
                        datetimeVar = convertDate(Contactlist.ContactDateEntered);
                        enteredby = Contactlist.EnteredBy;
                        content = Contactlist.MessageBody;
                        firstname = Contactlist.FirstName;
                        lastname = Contactlist.LastName;
                        var editorObject = $find("MainContent_updateED");
                        var panelDesc = editorObject.set_content(content);
                        $("#MainContent_PatientNameLabel").text(firstname + ' ' + lastname);
                        $("#MainContent_DateEnteredLabel").text(datetimeVar);
                        $("#MainContent_EnteredByLabel").text(enteredby);
                        $("#MainContent_CategoryLabel").text(descriptionVar);
                    }
                },
                error: function (response) {
                    alert("error");
                },
                cache: false
            });

        });



        function UpdateContactShowPopUp() {
            $("#ContactUpdatePopUp").show();
            $("#SemiTransparentBG").show();
        }
        function UpdateContactHidePopUp() {
            $("#ContactUpdatePopUp").hide();
            $("#SemiTransparentBG").hide();
        }


        $(document).on('click', "#UpdateContactcancel", function () {
            UpdateContactHidePopUp();
        });


        $(document).on('click', "#BackUrl", function () {
            window.top.location = 'Manage.aspx?PatientID=<%=PatientID%>';
        });

        //Update Contact Details

        $(document).on('click', "#UpdateContactBtn", function () {
            var editorObject = $find("MainContent_updateED");
            var ContactDesc = editorObject.get_content();
            if (ContactDesc == "") {
                alert('Please enter the content');
            }
            else {
                var postData = new Object();
                postData.content = ContactDesc;
                postData.ContactID = ID;
                $.ajax({
                    type: "POST",
                    url: "Contacts.aspx/UpdateContactDetails",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        UpdateContactHidePopUp();
                        GetAllContacts(1);
                    },
                    error: function (response) {
                        alert("error");
                    },
                    cache: false
                });
            }
        });




        $(document).on('click', "#MainContent_ButtonUpdate", function () {
            var bool = false;
            var editorObject = $find("<%=updateED.ClientID %>");
            var _content = editorObject.get_content();
            if (_content == '') {
                alert('Please enter the content'); return false;
            }
            else {
                bool = true;
            }
            return bool;
        });


        //=============================Update Contact block end====================================           



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <input type="hidden" value="" id="PatientIDHidden" runat="server" />

    <table border="0" cellpadding="6" cellspacing="0" class="border" style="overflow: auto ;width:1000px">
        <tr bgcolor="#D6B781" class="regText">
            <td>
                <b>Contact Records</b>

                <a class="AddContactlink" href="#">[Add]</a>



                <input type="radio" name="ContactRecords" value="Calendar" onclick="GetContactTypeValue(1)" id="rdCalendarEntries"> Calendar entries
                &nbsp;&nbsp;


                
                
                <input type="radio" name="ContactRecords" value="Autoship" onclick="GetContactTypeValue(2)" id="rdAutoshipEntries"> Autoship entries
                &nbsp;&nbsp;	
                			
                <input type="radio" name="ContactRecords" value="All" onclick="GetContactTypeValue(3)" id="rdBoth"> All Notes<br>
            
                
                &nbsp;&nbsp;				
            </td>
        </tr>
        <tr>
            <td>
                <b>Contact Type:</b> &nbsp;&nbsp;<asp:DropDownList ID="drpContacttype" runat="server" ClientIDMode="static" CssClass="FormField"></asp:DropDownList> &nbsp;&nbsp;
                <b>Date:</b> &nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass ="FormField" ></asp:TextBox>
                <%--<b>Search Text:</b> &nbsp;&nbsp;<asp:TextBox ID="txtSearchText"  runat="server" ClientIDMode="static" CssClass="FormField"></asp:TextBox> &nbsp;&nbsp;--%>
                <input type="button" id="btnSearch" onclick="GetAllContacts(1);"  value="Search" class="button" />
               

            </td>
        </tr>
        <tr>
            <td colspan="4" style="overflow: inherit;">


                <div id="ContactsDiv" style="display: none;" visible="false" bgcolor="#FFFFFF" class="regText"></div>

            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <table id="tdButton" style="display: none;">
        <tr>
            <td>
                <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button" />
                <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp <span id="pagingtext">of</span>
                <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>

            </td>
        </tr>


    </table>
    <div id="SemiTransparentBG" class="fadePanel " style="display: none;" visible="false"></div>
    
    <%--Add Contact block start--%>
    <div id="ContactAddPopUp" class="Popup " style="display: none;">

        <div style="padding-top: 30px;" class="InnerPopup">
            <input type="hidden" id="inpPatientID" runat="server" />
            <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr bgcolor="#D6B781" class="regText">
                    <td width="81%" bgcolor="#D6B781">
                        <b>Patient Name:</b>
                        <asp:Label ID="lblPatientName" runat="server" />
                    </td>
                    <td width="19%" bgcolor="#D6B781">&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr bgcolor="#D6B781">
                    <td colspan="2">
                        <p>
                            <b>Add Contact Record</b> (Medical Entry,Phone call, Email, etc)
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="regText">
                        <strong>Contact Type </strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="AptType" CssClass="FormField" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
            <br />

            <input type="button" id="AddContactbtn" class="button" value="Submit" />
            <input type="button" value="Cancel" id="CancleButton" class="button" />
            <obout:Editor ID="ed" runat="server" Height="600px" Width="600">
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
        </div>
    </div>
    <%--  Add Contact block end--%>
    <%--Update Contact block start--%>
    <div id="ContactUpdatePopUp" class="Popup " style="display: none;">
        <div style="padding-top: 30px;" class="InnerPopup">
            <input type="hidden" id="Hidden2" runat="server" />
            <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr bgcolor="#D6B781">
                    <td>
                        <strong>Contact Details </strong>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <input name="Button" type="button" class="button" id="BackUrl"
                            value="Back to Patient Profile">
                    </td>
                </tr>
                <tr>
                    <td width="194" nowrap>
                        <strong>Patient Name </strong>
                    </td>
                    <td>
                        <asp:Label ID="PatientNameLabel" runat="server" />
                    </td>
                    <td width="100">
                        <strong>Date Entered</strong>
                    </td>
                    <td>
                        <asp:Label ID="DateEnteredLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="194" nowrap>
                        <strong>Category</strong>
                    </td>
                    <td width="315">
                        <asp:Label ID="CategoryLabel" runat="server" />
                    </td>
                    <td width="100">
                        <strong>Entered By</strong>
                    </td>
                    <td width="125">
                        <asp:Label ID="EnteredByLabel" runat="server" />
                    </td>
                </tr>
            </table>
            <br />

            <input type="button" value="Submit" id="UpdateContactBtn" class="button" />
            <input type="button" class="button" id="UpdateContactcancel" value="Cancel" />
            <obout:Editor ID="updateED" runat="server" Height="600px" Width="600">
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
        </div>
    </div>
    <%-- Update Contact block end--%>
</asp:Content>
