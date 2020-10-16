<%@ Page Title="lab_report_short" Language="C#" MasterPageFile="~/sub.master" ValidateRequest="false" EnableEventValidation="false" ViewStateEncryptionMode="Never" EnableViewStateMac="false" AutoEventWireup="true" CodeFile="lab_report_short.aspx.cs" Inherits="lab_report_short" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <!-- InstanceBeginEditable name="main" -->
<table width="100%" border="0" cellpadding="5" cellspacing="0" class="report" id="headerTable">
	<tr class="largeheading">
	  <td valign="top">&nbsp;</td>
	  <td>&nbsp;</td>
	  <td><div align="right"><strong><a href="lab_report_print.aspx?message_id=<%=message_id%>&patientid=<%=patientid%>"  target="_blank">Print</a> <a href="lab_launch_short.aspx?patientid=<%=patientid %>">Back</a> </strong></div></td>
  </tr>
	<tr class="largeheading">
	  <td valign="top"></td>
	  <td>&nbsp;</td>
	  <td>&nbsp;</td>
  </tr>
	<tr class="largeheading">
		<td width="36%" valign="top">&nbsp;</td>
  <td width="30%">
			<font class="smallheading">Patient Information</font>
			<br>
			<font class="largeheading"> <asp:Label ID="lblPatientName" runat="server"></asp:Label> </font>
		</td>
		<td width="34%" bordercolor="#000000" style="border-style: solid; border-width: 4px;">
			<font class="smallheading">Report Status</font>&nbsp;
			<font class="largeheading"><asp:Label ID="lblReport_status" runat="server"></asp:Label> </font>
		</td>
  </tr>
	<tr>
	  <td width="36%" valign="top">
			Specimen Information
			<br>
		    SPECIMEN:&nbsp;&nbsp;&nbsp;	   <asp:Label ID="lblSpecimenId" runat="server"></asp:Label>	    <br>
	    REQUISITION: <asp:Label ID="lblRequisitionId" runat="server"></asp:Label>
	        <!-- replace?  not sure of source -->
	  </td>
		<td width="30%" valign="top">
			<font class="smallheading">&nbsp;</font>
			<br>
			DOB: <asp:Label ID="lblPatientDob" runat="server"></asp:Label>
		  
		  Age: <asp:Label ID="lblPatientAge" runat="server"></asp:Label>
		  <br>
		  GENDER:<asp:Label ID="lblPatientSex" runat="server"></asp:Label> 
		  
	    &nbsp;<br>
		  SS: <asp:Label ID="lblPatientSsn" runat="server"></asp:Label> 
		  </td>
	  <td width="34%" valign="top">
			Ordering Physician
			<br><asp:Label ID="lblOrderingDr" runat="server"></asp:Label> 	      
			<br>
			&nbsp;
			<br>
			Client Information
			<br>
			<asp:Label ID="lblClient_id" runat="server"></asp:Label>
			<br>
           <asp:Literal ID="LitPIDComment" runat="server"></asp:Literal>
          <br />
      </td>
	</tr>
	<tr>
		<td width="36%">
			COLLECTED: <asp:Label ID="lblCollectedDt" runat="server"></asp:Label>
			<br>
			RECEIVED: &nbsp;<asp:Label ID="lblReceivedDt" runat="server"></asp:Label>
			<br>
			REPORTED: &nbsp;<asp:Label ID="lblReportedDt" runat="server"></asp:Label>
	  </td>
		<td width="30%">&nbsp;
			
	  </td>
		<td width="34%">&nbsp;
			
	  </td>
	</tr>
</table>

<hr align="left" width="100%" size="5" color="black" />

<table border="0" width="100%" class="labreport">
	<tr >
		<td width="39%" nowrap align="left"><b>Test Name</b></td>
		<td width="10%" nowrap align="left"><b>In Range</b></td>
		<td width="18%" nowrap align="left"><b>Out of Range</b></td>
		<td width="23%" nowrap align="left"><b>Reference Range</b></td>
		<td width="10%" nowrap align="left"><b>Lab</b></td>
	</tr>
    <tr>
        <td width="37%"><asp:Label ID= "lblTestName" runat="server"></asp:Label></td>
        <td width="10%"></td>
        <td width="20%"></td>
        <td width="23%"></td>
        <td width="10%"><asp:Label ID="lblLabId" runat="server"></asp:Label></td>
     </tr>
    <asp:Literal ID="litTestDetails" runat="server"></asp:Literal>
    </table>
   

<pre>
--------------------------------------------------------------------------------------------------------------
</pre>

Performing Laboratory Information:

<table border="0" width="100%">
<tr class="report">
    <td> <asp:Literal ID="litInner" runat="server"></asp:Literal></td>
    <%--<td width="20%"><asp:Label ID="lblCurrent_lab_code" runat="server"></asp:Label> </td>
    <td width="80%"><asp:Label ID="lblCurrent_lab_info" runat="server"></asp:Label> </td>--%>
    </tr>
</table>

<br>
</asp:Content>

