<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="ShareFile.aspx.cs" Inherits="ShareFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script>
        function displayDiv() {
            debugger;
            var div = document.getElementById("divFile");
            var divEmail = document.getElementById("divEmail")
            if (div.style.display == "none") {
                div.style.display = "block";
                divEmail.style.display = "none";

            }
            {
                div.style.display = "none";
                divEmail.style.display = "block";}


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divFile" runat="server" clientidmode="Static" style="display: block">
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="2">
                    <p>
                        <b>ShareFile Uploader</b>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2">The file you select from your computer will automatically uploaded to ShareFile and
                    <br />
                    Notified the patient that new file exists for them to view.<br />
                    Note:Be sure to name it before you upload it.
                </td>

            </tr>

            <tr>
                <td><b>File Name</b></td>
                <td>

                    <asp:TextBox runat="server" CssClass="FormField" ID="txtFilename" Columns="50" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a file name."
                        SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFilename" />
                </td>
            </tr>
            <tr>
                <td><b>File Upload</b></td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FormField" ClientIDMode="Static" />
                    <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload1" 
                        SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please select a file."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnfilepath" runat="server" ClientIDMode="Static" />
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Next" OnClientClick="displayDiv();"  />
                </td>
            </tr>
        </table>
    </div>
    <div id="divEmail" runat="server" clientidmode="Static" style="display: none">
        <table width="600" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781">
                <td colspan="2">
                    <p>
                        <b>ShareFile Uploader</b>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2">The file you select from your computer will automatically uploaded to ShareFile and
                    <br />
                    Notified the patient that new file exists for them to view.<br />
                    Note:Be sure to name it before you upload it.
                </td>

            </tr>

            <tr>
                <td><b>Subject</b></td>
                <td>

                    <asp:TextBox runat="server" CssClass="FormField" ID="txtSubject" Columns="100" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You must enter a Subject."
                        SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSubject"  />
                </td>
            </tr>
            <tr>
                <td><b>Body</b></td>
                <td>
                    <asp:TextBox ID="txtBody" TextMode="MultiLine" MaxLength="800" CssClass="FormField" Columns="100" rows="25" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBody"
                        SetFocusOnError="true" ForeColor="Red" ErrorMessage="Please Enter Message." ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                    <asp:Button ID="Button1" runat="server" CssClass="button"  Text="Upload and Send" OnClick="btnSubmit_Click" OnClientClick="document.getElementById('hdnfilepath').value=document.getElementById('FileUpload1').value" />
                </td>
            </tr>
        </table>
    </div>
    <p>
        <b></b>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>

