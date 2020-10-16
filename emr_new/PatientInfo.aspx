<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="PatientInfo.aspx.cs" Inherits="PatientInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="PersInfo" style="position: absolute; width: 672px; z-index: 5; left: 9px; top: 212;">
        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
            <tr bgcolor="#D6B781" class="regText">
                <td colspan="3" bgcolor="#D6B781">
                    <b>Personal Information</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Last Updated</strong>:&nbsp;<%= pat.LastUpdated != null ? ((DateTime)pat.LastUpdated).ToShortDateString() : "" %>
                   <br /><b><%= pat.RenewalDate != null ? "Renwal Date: "+((DateTime)pat.RenewalDate).ToShortDateString() : "" %></b>
                </td>
                <td colspan="2" bgcolor="#D6B781">
                    <div align="right">
                        [<a href="intake_form_start.aspx?patientid=<%= Request.QueryString["PatientID"] %>">Start
							Intake</a>] [<a href="patient_update.aspx?PatientID=<%= pat.PatientID %>">Edit Profile</a>]
                    </div>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                      <b><%= "PatientId :"+ pat.PatientID.ToString() %></b>
                </td>
            </tr>
             <tr class="regText">
                <td nowrap>
                      <b><%= pat.ChinaPatientId != null ? "ChinaPatientId :"+ pat.ChinaPatientId.ToString():"" %></b>
                </td>
            </tr>
             <tr class="regText">
                <td nowrap>
                    <b><%= pat.MgtProgramName != null ? "Management Program :"+ pat.MgtProgramName.ToString():"Management Program :None" %></b>
                     
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <b>Billing Info</b>
                </td>
                <td width="175" nowrap>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <b>Shipping Info</b>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Address</b></font>
                </td>
                <td width="175" nowrap>
                    <%= pat.BillingStreet %>
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <font color="#666666">
						<%= pat.ShippingStreet %></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>City, State Zip </b></font>
                </td>
                <td width="175" nowrap>
                    <%= pat.BillingCity %>,&nbsp;<%= pat.BillingState %>&nbsp;<%= pat.BillingZip %>
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <font color="#666666">
						<%= pat.ShippingCity %>,
						<%= pat.ShippingState %>&nbsp;<%= pat.ShippingZip %></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Country </b></font>
                </td>
                <td width="175" nowrap>
                    <%= pat.BillingCountry %>
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <font color="#666666">
						<%= pat.ShippingCountry %></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666">&nbsp;</font>
                </td>
                <td width="175" nowrap>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">
                    <font color="#666666">&nbsp;</font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Home Phone </b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.HomePhone %>
					</font>
                    <%= pat.Home_detailed_info == true ? "<font color='#FF0000'>Detailed Info OK</font>" : ""%>
                    <%= pat.Home_CB_only == true ? "<font color='#FF0000'>Call Back ONLY</font>" : "" %>
                    <%= pat.Home_NoMessage == true ? "<font color='#FF0000'>Do NOT Leave message</font>" : "" %>
                </td>
                <td>&nbsp;
                </td>
                <td width="110" nowrap>
                    <font color="#666666"><b>Work Phone</b></font>
                </td>
                <td width="246">
                    <font color="#666666">
						<%= pat.WorkPhone %></font>
                    <%= pat.Work_Detailed_info == true ? "<font color='#FF0000'>Detailed Info OK</font>" : "" %>
                    <%= pat.Work_CB_only == true ? "<font color='#FF0000'>Call Back ONLY</font>" : "" %>
                    <%= pat.Work_NoMessage == true ? "<font color='#FF0000'>Do NOT leave message</font>" : "" %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Cell Phone</b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.CellPhone %></font>
                    <%= pat.Cell_Detailed_info == true ? "<font color='#FF0000'>Detailed Info OK</font>" : "" %>
                    <%= pat.Cell_CB_Only == true ? "<font color='#FF0000'>Call Back ONLY</font>" : "" %>
                    <%= pat.Cell_NoMessage == true ? "<font color='#FF0000'>Do NOT leave message</font>" : "" %>
                </td>
                <td>&nbsp;
                </td>
                <td nowrap>
                    <font color="#666666"><b>Email Address</b></font>
                </td>
                <td>
                    <font color="#666666">
						<%= pat.Email %></font>
                    <%= pat.Email_auth_detailed_info == true ? "<font color='#FF0000'>Detailed Info OK</font>" : "" %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap class="style4">
                    <font color="#666666"><b>Fax Phone</b></font>
                    <%= pat.Fax_auth_detailed_info == true ? "<font color='#FF0000'>Detailed Info OK</font>" : "" %>
                </td>
                <td width="175" nowrap class="style4">
                    <font color="#666666">
						<%= pat.FaxPone %></font>
                </td>
                <td class="style4"></td>
                <td class="style4">
                    <font color="#666666"><strong>Clinic</strong></font>
                </td>
                <td class="style4">
                    <font color="#666666">
						<%= pat.Clinic %></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Contact Preference</b></font>
                </td>
                <td>
                    <font color="#666666">
						<%= pat.ContactPreference %></font>
                </td>
                <td class="style4"></td>
                <td nowrap>Event
                </td>
                <td>
                    <font color="#666666">
						<%= pat.EventName%></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666">Affiliate</font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.AffiliatePatient %></font>
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666">&nbsp;</font>
                </td>
                <td width="175" nowrap>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Birthday</b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.Birthday != null ? ((DateTime)pat.Birthday).ToShortDateString() : ""%></font>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <strong><font color="#666674">Statement of Charges</font></strong>
                </td>
                <td>
                    <%= pat.SOC == true ? "Yes " : pat.SOC == null ? "None Selected" : "No" %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <strong><font color="#666674">Cancel/NS Form Signed</font></strong>
                </td>
                <td nowrap>
                    <%= pat.Cancel_NoShow_frm_signed == true ? "<font color='#FF0000'>Yes</font>" : "<font color='#FF0000'>No</font>" %>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <strong><font color="#666674">Patient Type</font></strong>
                </td>
                <td>
                    <font color="#666666">
						<%= pat.Medical == true ?"Medical ": "" %>
						<%= pat.Aesthetics == true ? "Aesthetics " : "" %>
						<%= pat.Autoship == true ? "Autoship ": "" %>
						<%= pat.Retail == true ? "Retail only " : "" %>
						<%= pat.Affiliate == true ? "Affiliate " : ""%>
					</font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <strong><font color="#666674">HIPAA Form Signed</font></strong>
                </td>
                <td nowrap>
                    <%= pat.HIPPA_signed == true ? "<font color='#FF0000'>Yes</font>" :  "<font color='#FF0000'>No</font>" %>
                    <%= pat.HIPPA_signed_date %>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <font color="#666666"><b>Medicare Opt Out</b></font>
                </td>
                <td>
                    <%= pat.MedicareOptOut_YN %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666">Medicare Part B</font>
                </td>
                <td width="175" nowrap>
                    <%= (pat.MedicareB == null || pat.MedicareB == false) ? "No" : "Yes"   %>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <font color="#666666"><b>Opt Out Date</b></font>
                </td>
                <td>
                    <%= pat.MedicareOptOut_Date != null ? ((DateTime)pat.MedicareOptOut_Date).ToShortDateString() : ""  %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <strong><font color="#666666">Patient Advocate</font></strong>
                </td>
                <td nowrap>
                    <font color="#666666">
                        
						<%= pat.EmployeeName %></font>
                </td>
                <td>&nbsp;
                </td>
                <td> <font color="#666666"><b>Labs Mailed</b></font>
                </td>
                <td> <font color="#666666">
                        
						<%= pat.LabsMailed %></font>
                </td>
            </tr>
             <tr class="regText">
                <td nowrap>
                    <span class="style2"><strong>Preffered Pharma</strong></span>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.Prefered_Pharm %></font>
                </td>
                <td>&nbsp;
                </td>
                <td>
                   
                </td>
                <td>
                 
                </td>
            </tr>

            <tr class="regText">
                <td nowrap>
                    <span class="style2"><strong>Primary Care Phys</strong></span>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.PCP %></font>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <font color="#666666"><b>Name Alert</b></font>
                </td>
                <td>
                    <%= pat.NameAlert %>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Emergency Contact </b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.EmergencyFirstName %>&nbsp;<%= pat.EmergencyLastName %></font>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <span class="style2"><strong>LMC Physician</strong></span>
                </td>
                <td>
                    <font color="#666666">
						<%= pat.LMC_CP %></font>
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Phone</b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.EmergencyPhone %></font>
                </td>
                <td>&nbsp;
                </td>
                <td nowrap>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr class="regText">
                <td nowrap>
                    <font color="#666666"><b>Relationship</b></font>
                </td>
                <td width="175" nowrap>
                    <font color="#666666">
						<%= pat.EmergencyRelationship %></font>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <font color="#666666"><b>Eating Planned Received</b></font>
                </td>
                <td>
                    <font color="#666666">
						<%= pat.EatingPlanReceived_YN %></font>
                </td>
            </tr>

        </table>
    </div>

</asp:Content>
