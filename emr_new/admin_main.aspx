<%@ Page Title="EMR Admin" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_main.aspx.cs" Inherits="admin_main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="686" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td width="33%" bgcolor="#D6B781" class="PageTitle">Admin Home
            </td>
            <td width="33%" bgcolor="#D6B781">&nbsp;
            </td>
            <td width="33%" bgcolor="#D6B781">&nbsp;
            </td>
        </tr>
        <tr bgcolor="#D6B781">
            <td width="33%" bgcolor="#D6B781">
                <strong>Providers</strong>
            </td>
            <td width="33%" bgcolor="#D6B781">
                <strong>Marketing</strong>
            </td>
            <td width="33%" bgcolor="#D6B781">
                <strong>Staff Access</strong>
            </td>
        </tr>
        <tr valign="top">
            <td width="33%">
                <p>

                    <a href="admin_drug_list.aspx">Edit Drug List</a>
                </p>
                <p>
                    <a href="admin_supp_list.aspx">Edit SupplementList</a>
                </p>
                <p>
                    <a href="admin_UploadTags.aspx">Manage Tags</a>
                </p>
                <p>
                    <a href="admin_dictation.aspx">Manage Dictation Console</a>
                </p>
                <%-- <p>
                    <a href="PatientSurveyQuestion.aspx">Patient Survey</a>
                </p>--%>
                <p>
                    <%-- <a href="PatientSurveyQuestion.aspx">Patient Survey</a>--%>
                    <a href="AutoShipEmailTemplate.aspx">AutoShip Email Template</a>
                </p>
                <p>
                    <%-- <a href="PatientSurveyQuestion.aspx">Patient Survey</a>--%>
                    <a href="IVRTemplate.aspx">IVR Template</a>
                </p>
                <p><a href="AdminEmailTemplate.aspx">Email Tempalate</a></p>
                <p><a href="PatientFormXERO.aspx">Xero Patient</a></p>
                <p><a href="XEROCredential.aspx">Xero Credential</a></p>
                <p><a href="CallFirePage.aspx">Robo Call</a></p>
                <p><a href="Autoship/ShipmentStation.aspx">Shipstation</a></p>
                <p><a href="calendar/SchedulingAcuityAppointments.aspx">Acuity Scheduling</a></p>
                <p><a href="CalendarStatusTicket.aspx">Calendar Status</a></p>
                <p>

                    <a href="ICD10Code.aspx">ICD10Codes List</a>
                </p>
                <p>

                    <a href="FBImporter.aspx">Facebook Importer</a>
                </p>
                 <p>

                    <a href="ProblemSuppliment.aspx">Problem List</a>
                </p>
            </td>
            <td width="33%">
                <p>
                    <a href="admin_todayscontacts.aspx">Today's Patients</a>
                </p>
                <p>
                    <a href="admin_Sat_SurveyResults.aspx">Survey Results </a>
                </p>
                <p>
                    <a href="crm/CRM_DashBoard.aspx">CRM </a>
                </p>

                <p>
                    <a href="CRM/AddProspect.aspx">Event Registration </a>
                </p>
                <p>
                    <a href="Admin_CrmCampign.aspx">CRM Campaign </a>
                </p>
                <p>
                    <a href="CRM/admin_CRM_WebUnmachedData.aspx">Match homepage interface </a>
                </p>
                <p>
                    <a href="admin_reseller.aspx">Affiliate Admin</a><br />
                </p>
                <p><a href="admin_reseller_data.aspx">Affiliate Events and Status</a></p>
                <p><a href="AutoShipBundle.aspx">Bundle Products</a></p>
                <p><a href="Admin_ShippingValues.aspx">Shipping Fee & OrderLimit</a></p>
                <p><a href="ManagementFeeProgram.aspx">Management Program</a></p>
                <p><a href="admin_saleAccountCode.aspx">Sale Account </a></p>

            </td>
            <td width="33%">
                <p>
                    <a href="admin_stafflogin_add.aspx">Add New Staff Login</a>
                </p>
                <p>
                    <a href="admin_stafflogin_list.aspx">Edit Logins &amp; <strong>Passwords</strong></a>
                </p>
                <p>
                    <a href="crm/SeminarScheduling.aspx">Scheduling </a>
                </p>
                <p>
                    <a href="crm/SeminarList.aspx">Scheduling List</a>
                </p>

                <p><a href="MailChipCampaigns.aspx">MailChimp Campaign </a></p>
            </td>
        </tr>

    </table>
    <br>
    <table width="686" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td width="30%">
                <p>
                    <strong>Appointments</strong>
                </p>
            </td>
            <td width="36%" bgcolor="#D6B781">
                <strong>Appointments - New Follow Ups </strong>
            </td>
            <td width="34%" bgcolor="#D6B781">
                <p>
                    <b>Prescription</b>
                </p>
            </td>
        </tr>
        <tr valign="top">
            <td width="30%">
                <p>
                    <a href="admin_open_apt.aspx">Open Apts For Clinics</a>

                </p>
                <p>

                    <a href="patient_apt_gap.aspx">Patient Apt Gap</a>
                </p>
                <p>
                    <a href="apt_99_special.aspx">99 Special Apts</a>
                </p>
                <p>
                    <a href="Calendar/CalendarAdmin.aspx">Calendar Admin</a>
                </p>
                <p>
                    <a href="Admin_Reminders.aspx">Manage Reminders</a>
                </p>
            </td>
            <td width="36%">
                <p>
                    <a href="admin_pending_followups.aspx">Pending Follow Ups For Clinics</a>

                </p>
                <p>
                    <a href="admin_pending_blooddraws.aspx">Pending Lab Draws</a><br>
                    <p>
                        <a href="admin_match_labs.aspx">Match Appointments to Blood Draws</a><br>
                    </p>
                    <p>
                        <a href="admin_pending_aes_followups.aspx">Pending Aesthetics Follow Ups</a><br>
                    </p>
                    <p>
                        <a href="admin_pending_medrecs.aspx">Pending Medical Records Requests </a>
                    </p>
                    <p>
                        <a href="admin_pending_consults.aspx">Pending Consult Requests </a>
                    </p>
                    <p>
                        <a href="admin_pending_as.aspx">Pending Auto Ship Requests </a>
                    </p>
            </td>
            <td>
                <p>
                    <a href="admin_5Days_prescription.aspx">Last 5 Day's Prescriptions </a>
                </p>
                <p>
                    <a href="admin_pending_prescriptions.aspx">Pending Prescriptions</a>
                </p>
                <p>
                    <a href="admin_south_patients.aspx"><strong>South</strong> Patients</a>

                </p>
                <p>
                        <a href="ListSharePointPatients.aspx">Wait List </a>
                    </p>
            </td>
        </tr>
    </table>
    <br>
    <table width="686" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781">
            <td width="33%">
                <p>
                    <b>Quest Labs</b>
                </p>
            </td>
            <td width="33%">
                <strong>Protocols</strong>
            </td>
            <td width="201">
                <strong>Maintenance</strong>
            </td>
        </tr>
        <tr valign="top">
            <td width="33%">
                <p>
                    <a href="admin_lab_patientmatch.aspx">Patients Unassigned from Import</a>
                </p>
                <p>
                    <a href="admin_LabLog.aspx">Lab Import Log</a><font color="#FF0000"> </font>
                </p>
                <p>
                    <a href="admin_LabRequest.aspx">Lab Request Panels and Tests</a><font color="#FF0000"> </font>
                </p>
                <p>
                    <a href="admin_LabSchedule.aspx">Recent Draw Groups and Tests</a><font color="#FF0000"> </font>
                </p>
                <p>
                    <a href="admin_Reminders.aspx">Lab and Supplement Reminders</a><font color="#FF0000"> </font>
                </p>

            </td>
            <td width="33%">
                <p>
                    <a href="protocols_protocol_list.aspx">Protocols Administration</a>
                </p>
                <p>
                    <a href="protocols_symptom_add.aspx">Symptoms List</a>
                </p>
                <p>
                    <a href="protocols_diagnosis_add.aspx">Diagnosis List</a>
                </p>
                <p>
                    <a href="admin_icd9_list.aspx">Update Diagnoses / ICD9 Codes</a><br>
                    <a href="admin_icd9_add.aspx">Add New ICD9 Code</a>

                </p>
            </td>
            <td>
                <p>
                    <a href="External/Manage.aspx">Manage External Labs and Panels</a>
                </p>
                <p>
                    <p>
                        <a href="DepartmentStaff_Add.aspx">Ticketing Groups</a>
                    </p>
                    <p>
                        <a href="Admin_AutoTicket.aspx">Recurring Tickets</a>
                    </p>
                    <p>
                        <a href="Tickats_Manage.aspx">Manage Automatic Ticket Processes</a>
                    </p>
                    <p>
                        <%-- <a href="http://10.0.2.89/reportserver">Reports</a>--%>
                        <%-- <a href="http://50.23.221.50/Reports/">Reports</a>--%>
                        <a href="ReportList.aspx">Reports</a>
                    </p>
                    <p>
                        <a href="LabReports_Panels_Groups_Tests.aspx">Lab Report Data Entry</a>
                    </p>
                    <p>
                        <a href="admin_ImportTests.aspx">Import Tests to Lab Report</a>
                    </p>
                    <p>
                        <a href="FollowupTypeMaint.aspx">Maintain Followup Types</a>
                    </p>
                    <p>
                        <a href="Autoship/Admin_CancelReasons.aspx">Maintain Autoship Cancel Reasons</a>
                    </p>
                    <p>
                        <a href="admin_QBMmumatches.aspx">QuickBooks Matches </a>
                    </p>
                    <p>
                        <a href="admin_SharepointPatientTracking.aspx">Import Web Contacts </a>
                    </p>
                    <p>
                        <a href="Admin_EndMedical.aspx">Mark Patients End Medical</a>
                    </p>
            </td>
        </tr>
    </table>
</asp:Content>
