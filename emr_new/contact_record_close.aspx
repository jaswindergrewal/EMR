<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="contact_record_close.aspx.cs" Inherits="contact_record_close" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function tmt_winHistory(id, s) {
            var d = eval(id) == null || eval(id + ".closed");
            if (!d) { eval(id + ".history.go(" + s + ")"); }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
  <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
    <tr bgcolor="#D6B781">
     <td colspan="4"><strong>Contact Details </strong></td>
    </tr>
    <tr>
      <td width="194" nowrap><strong>Patient Name </strong></td>
      <td><asp:Label ID="lblFirestName" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblLastName" runat="server"></asp:Label></td>
      <td width="100"><strong>Date Entered</strong></td>
      <td><asp:Label ID="lblContactDateEntered" runat="server"></asp:Label></td>
    </tr>
    
    <tr>
      <td width="194" nowrap><strong>Category</strong></td>
      <td width="315"><asp:Label ID="lblAptTypeDesc" runat="server"></asp:Label> </td>
      <td width="100"><strong>Entered By</strong> </td>
      <td width="125"><asp:Label ID="lblEnteredBy" runat="server"></asp:Label></td>
    </tr>
  </table>

    
  
  
<%--<% If (FollowUpDetails.Fields.Item("ReqsFollow_YN").Value) <> 0 Then %>--%>
  <br>
   <asp:Panel ID="pnlReqsFollow_YN" runat="server" Visible="false">
  <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
    <tr bgcolor="#D6B781">
      <td colspan="4"><strong>Follow Up Details </strong></td>
    </tr>
    <tr>
      <td width="159"><strong>Requires Follow Up </strong></td>
      <td width="313"><asp:Label ID="lblReqsFollow_YN" runat="server"></asp:Label></td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td width="159"><strong>Date for Follow Up </strong></td>
      <td colspan="3"><asp:Label ID="lblFollowUp_Date" runat="server"></asp:Label></td>
    </tr>
    <tr>
      <td width="159"><strong>Category</strong></td>
      <td colspan="3"><asp:Label ID="lblCat_Desc" runat="server"></asp:Label></td>
    </tr>
  </table>
   </asp:Panel>
 <%-- <% End If ' end Not FollowUpDetails.EOF Or NOT FollowUpDetails.BOF %>--%>
<br>
  <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
    <tr>
      <td width="792" colspan="2" bgcolor="#D6B781"><strong>Contact Notes </strong></td>
    </tr>
    <tr>
      <td colspan="2"><asp:Label ID="lblMessageBody" runat="server"></asp:Label> <a href="./ContactRecUpdate.aspx?contactID=<%=ViewState["ContactID"]!=null&&ViewState["ContactID"].ToString()!=string.Empty?ViewState["ContactID"].ToString():"" %>&StaffID=195">[Add Dictation text/Update] </a></td>
    </tr>
  </table>
  <br>
  <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
    <tr>
      <td width="792" colspan="2" bgcolor="#D6B781"><strong>Follow Up  Notes </strong></td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="FollowUpNotes" cols="80" rows="10" class="FormField" id="txtAreaFollowUpBody" runat="server"></textarea>
   
      </td>
    </tr>
    <tr>
      <td colspan="2">Close Follow Up Record 
    <asp:CheckBox ID="chkFollowUp_Completed" runat="server" />

      </td>
    </tr>
  </table>
  <p>
    <%--<input name="Submit" type="submit" class="button" value="Submit Follow Up">--%>
      <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit Follow Up" OnClick="btnSubmit_Click" />
    <input name="Button" type="button" class="button" onClick="tmt_winHistory('self','-1')" value="Cancel">
</p>
  <p>

      <asp:Button ID="btnBack" runat="server" Text="Back to Patient Profile" CssClass="button"/>

</p>
  <%--<input type="hidden" name="MM_update" value="form2">--%>
  <%--<input type="hidden" name="MM_recordId" value="ContactID">--%>
</asp:Content>

