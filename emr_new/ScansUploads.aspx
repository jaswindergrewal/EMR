<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="ScansUploads.aspx.cs" Inherits="ScansUploads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=lsttag]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td>
                <b>Patient Documents or Scans </b>
            </td>
            <td colspan="2">
                <asp:ListBox ID="lsttag" runat="server" SelectionMode="Multiple">
  
</asp:ListBox>
<asp:Button Text="Search" runat="server" id="txtSearch" OnClick="txtSearch_Click"/>
            </td>
            <td colspan="2">
                <div align="right">
                    [<a href="Upload_file.aspx?patientID=<%= PatientID %>">Upload New Document</a>]
                </div>
            </td>
        </tr>
        <tr class="regText">
            <td>
                <strong>Document Title</strong>
            </td>

            <td><strong>Created Date</strong></td>
            <td>
                <strong>Category</strong>
            </td>
            <td><strong>Edit</strong></td>
            <td><strong>Delete</strong></td>
        </tr>
        <asp:Repeater ID="rptDocuments" runat="server">
            <ItemTemplate>
                <tr class="regText">
                    <td>
                        <img src="<%# Eval("extentionPath")%>" height="25px" width="25px" align="middle" />&nbsp;&nbsp;
                        <%--<a href="uploads/<%# Eval("PatientID")%>/<%# Eval("Upload_Path")%>"
                           target="_blank">--%>

                        <a     href="#"                       onclick="DownLoadFile(<%# Eval("PatientID")%>,<%# Eval("UploadID")%>);">
                            <%# Eval("Upload_Title")%>
                        </a>


                    </td>
                    <td><%# Convert.ToDateTime(Eval("DateEntered")).ToShortDateString() %></td>
                    <td>

                        <%# Eval("Category")%>
                    </td>
                    <td>
                        <img src="images/edit.png" onclick="OpenURL(<%# Eval("UploadID")%>)"></img>

                    </td>
                    <td>
                        <img src="images/delete.gif" onclick="ConfirmDelete(<%# Eval("UploadID")%>,<%# Eval("PatientID")%>);" /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>


    <script type="text/javascript">
        function DownLoadFile(patientId, UploadId)
        {
            
            url = '<%=Page.ResolveUrl("~/ScansUploads.aspx/DownLoadFile")%>';
            var options = {
                type: "POST",
                url: url,
                data: "{patientId:'" + patientId + "',UploadId:'" + UploadId+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    urllink = '<%=Page.ResolveUrl("~/Uploads")%>';
                    //Window.open(urllink + '/' + result);
                    MM_goToURL('self', 'Uploads/' + result.d)
                    
                },
                error: function (obj) {
                    alert(obj.responseText);
                }
            };
            $.ajax(options);
        }

        //Added upload id in the delete function as the delete function not delete the correct entry.(12th sept 2013)
        function OpenURL(uploadId) {
            MM_goToURL('self', 'upload_edit.aspx?UploadID=' + uploadId);
        }

        function ConfirmDelete(uploadId, patientID) {

            if (confirm("Do you want to delete this Upload ?")) {

                var hiddenValue = uploadId;


                url = '<%=Page.ResolveUrl("~/ScansUploads.aspx/DeleteUpload")%>';
                var options = {
                    type: "POST",
                    url: url,
                    data: "{UploadID:'" + hiddenValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        MM_goToURL('self', 'ScansUploads.aspx?PatientID=' + patientID)

                    },
                    error: function (obj) {
                        alert(obj.responseText);
                    }
                };
                $.ajax(options);
                return true;
            }
            else {


                return false;
            }
        }


    </script>
</asp:Content>
