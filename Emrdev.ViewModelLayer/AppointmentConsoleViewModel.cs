using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{

   
    public class AppointmentsConsoleViewModel
    { }
    public class AppConsole_MedNotesViewModal
    {
        public int ContactId { get; set; }
        public System.DateTime ContactDateEntered { get; set; }
        public int? PatientID { get; set; }
        public string EmployeeName { get; set; }
        public string MessageBody { get; set; }
    }

    public class AppConsole_ConsultViewModal
    {
        public int ContactId { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? Range_Start { get; set; }
        public DateTime? Range_End { get; set; }
        public string EmployeeName { get; set; }
        public string FollowUp_Body { get; set; }
        public string FollowUp_Type_Desc { get; set; }
    }

    public class AppConsole_FollowUpViewModal
    {
        public int? PatientID { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? Range_Start { get; set; }
        public DateTime? Range_End { get; set; }
        public string EmployeeName { get; set; }
        public string FollowUp_Body { get; set; }
        public string FollowUp_Type_Desc { get; set; }
    }

    public class AppConsole_BloodDrawViewModal
    {
        public int? PatientID { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? value { get; set; }
        public string EmployeeName { get; set; }
        public string FollowUp_Body { get; set; }
        public string FollowUp_Type_Desc { get; set; }
        public int? FollowupID { get; set; }
    }

    public class AppConsole_PrescriptionViewModal
    {
        public string DrugName { get; set; }
        public string Drug_Dose { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Drug_NumbRefills { get; set; }
        public string EmployeeName { get; set; }

    }

    public class AppConsole_MedNoteDetailsViewModal
    {
        public int? PatientID { get; set; }
        public DateTime? value { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EnteredBy { get; set; }
        public string AptTypeDesc { get; set; }
        public int? ContactID { get; set; }
        public string MessageBody { get; set; }
    }

    public class AppConsole_CriticalTaskViewModal
    {
        public string TaskName { get; set; }
        public bool? Received { get; set; }
        public bool? Requested { get; set; }
        public bool? Reviewed { get; set; }
        public string ReceivedDate { get; set; }
        public string RequestedDate { get; set; }
        public string ReviewedDate { get; set; }
        public int? TaskID { get; set; }
        public string Upload_Title { get; set; }
        public string Upload_Path { get; set; }
    }

    public class AppConsole_LabWorkViewModal
    {
        public string GroupName { get; set; }
        public bool? InRange { get; set; }
        public string LastComplete { get; set; }
        public string Days { get; set; }

    }

    public class AppConsole_LabListViewModal
    {
        public int? MessageID { get; set; }
        public DateTime? ObservationDateTime { get; set; }
        public int? PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastChanged { get; set; }
        public string PlacerOrderNumber { get; set; }

    }

    public class AppConsole_DiagnosisListViewModal
    {
        public int? PatientID { get; set; }
        public int? ProbDiagID { get; set; }
        public string Diag_Title { get; set; }
        public string ICD9_Code { get; set; }
        public DateTime? DateEntered { get; set; }
        public bool? Active_YN { get; set; }
        public decimal? Severity_num { get; set; }
        public decimal? Priority_num { get; set; }
        public DateTime? inactive_date { get; set; }
        public bool? beingaddressed_yn { get; set; }
    }

    public class AppConsole_PatientSymptViewModal
    {
        public int? ProbSymptID { get; set; }
        public int? PatientID { get; set; } //
        public DateTime? DateEntered { get; set; }
        public string SymptomName { get; set; }
        public bool? Active_YN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Severity_num { get; set; }
        public decimal? Priority_num { get; set; }
        public DateTime? inactive_date { get; set; }
        public bool? beingaddressed_yn { get; set; }
        public string Dir { get; set; }

    }

    public class AppConsole_InvoiceViewModal
    {
        public string InvoiceLineItemRefListID { get; set; }
        public decimal? SalesPrice { get; set; }
        public DateTime? DueDate { get; set; }
        public string OpenBalance { get; set; }
        public string IsPaid { get; set; }

    }

    public class AppConsole_AptDateViewModal
    {
        public DateTime? ApptStart { get; set; }
        public int? apt_id { get; set; }
        public int? AppointmentTypeID { get; set; }
        public bool? OVU { get; set; }
    }

    public class AppConsole_OVUViewModal
    {
        public int? AptID { get; set; }
        public string dir { get; set; }
        public int? SymptomID { get; set; }
        public string SymptomName { get; set; }
    }

    public class AppConsole_OVUoldViewModal
    {
        public int? SymptomID { get; set; }
        public string SymptomName { get; set; }
    }

    public class AppConsole_LabDetailsViewModal
    {
        public DateTime? ObservationDateTime { get; set; }
        public int? ID { get; set; }
        public string ObservationIdentifier { get; set; }
        public string ObservationValue { get; set; }
        public string Units { get; set; }
        public string ReferencesRange { get; set; }
    }

    public class Upload_tblViewModel
    {
        public int UploadID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Upload_Path { get; set; }
        public string Upload_Title { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string Category { get; set; }
        public byte[] Pdf_Binary { get; set; }
        public byte[] otherFormats_Binary { get; set; }
    }

    public class PatientViewModel
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public Nullable<bool> Work_Detailed_info { get; set; }
        public Nullable<bool> Work_CB_only { get; set; }
        public Nullable<bool> Work_NoMessage { get; set; }
        public string WorkPhone { get; set; }
        public Nullable<bool> Cell_Detailed_info { get; set; }
        public Nullable<bool> Cell_CB_Only { get; set; }
        public Nullable<bool> Cell_NoMessage { get; set; }
        public string CellPhone { get; set; }
        public Nullable<bool> Home_detailed_info { get; set; }
        public Nullable<bool> Home_CB_only { get; set; }
        public Nullable<bool> Home_NoMessage { get; set; }
        public string HomePhone { get; set; }
        public string FaxPone { get; set; }
        public Nullable<bool> Fax_auth_detailed_info { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Email_auth_detailed_info { get; set; }
        public Nullable<bool> HIPPA_signed { get; set; }
        public Nullable<System.DateTime> HIPPA_signed_date { get; set; }
        public string Prefered_Pharm { get; set; }
        public string Pager { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sex { get; set; }
        public string Clinic { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyRelationship { get; set; }
        public string EmergencyState { get; set; }
        public string ContactPreference { get; set; }
        public Nullable<bool> Inactive { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> SpecialAttention { get; set; }
        public Nullable<int> ActivityRating { get; set; }
        public string Notes { get; set; }
        public string MedicalHistory { get; set; }
        public string image { get; set; }
        public string PCP { get; set; }
        public string LMC_CP { get; set; }
        public Nullable<int> ProvID { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> NameAlert { get; set; }
        public string ConciergeID { get; set; }
        public Nullable<bool> Aesthetic_YN { get; set; }
        public Nullable<bool> NoShowPol_Signed_YN { get; set; }
        public Nullable<bool> Cancel_NoShow_frm_signed { get; set; }
        public string Allergies { get; set; }
        public bool AllowApptReassign { get; set; }
        public bool Medical { get; set; }
        public bool Aesthetics { get; set; }
        public bool Autoship { get; set; }
        public bool Retail { get; set; }
        public bool Affiliate { get; set; }
        public Nullable<bool> SOC { get; set; }
        public bool DiabetesSOC { get; set; }
        public bool HeartSOC { get; set; }
        public string EmergencyContact { get; set; }
        public int Marketing_source { get; set; }
        public Nullable<int> Seminar_attended { get; set; }
        public Nullable<int> Seminar_status { get; set; }
        public string AutoshipNote { get; set; }
        public string AutoshipDiscounts { get; set; }
        public string AutoshipAlerts { get; set; }
        public bool MedicareOptOut_YN { get; set; }
        public Nullable<System.DateTime> MedicareOptOut_Date { get; set; }
        public bool EatingPlanReceived_YN { get; set; }
        public string Nickname { get; set; }
        public string RenewalMonth { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<System.DateTime> BalanceDueDate { get; set; }
        public bool AutoshipEmail { get; set; }
        public int AutoshipCancelReasonID { get; set; }
        public string AutoshipCancelOther { get; set; }
        public Nullable<System.DateTime> PaymentDue { get; set; }
        public Nullable<int> TermsInMonths { get; set; }
        public Nullable<System.DateTime> StartMedical { get; set; }
        public Nullable<System.DateTime> EndMedical { get; set; }
        public Nullable<System.DateTime> InvoiceDueDate { get; set; }
        public Nullable<bool> InvoicePaid { get; set; }
        public Nullable<decimal> InvoiceDue { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<bool> MedicareB { get; set; }
        public string RenewalException { get; set; }
        public Nullable<System.DateTime> RenewalExcExpire { get; set; }
        public Nullable<int> AffiliateID { get; set; }
        public Nullable<System.DateTime> AffiliateDate { get; set; }
        public bool IsAffiliate { get; set; }
        public bool LabsMailed { get; set; }
        public int RecordCount { get; set; }
        public string Age { get; set; }
        public string FullName { get; set; }
        public string PatientFullNameWithInActiveStatus { get; set; }
        public string EventName { get; set; }
        public string AffiliatePatient { get; set; }
        public string EmployeeName { get; set; }
        public string QBID { get; set; }
        public int PatientPackage { get; set; }
        public System.DateTime PackageDateentered { get; set; }
        public int PackageDuration { get; set; }
        public Nullable<int> ChinaPatientId { get; set; }
        public string BillingCountry { get; set; }
        public string ShippingCountry { get; set; }
        public Nullable<System.DateTime> RenewalDate { get; set; }
        public Nullable<System.Guid> XeropatientId { get; set; }
        public string AuthorisedPerson { get; set; }
        public int PatientManagementProgramId { get; set; }
        public string ProgramName { get; set; }
        public Nullable<bool> CallBeforeShip { get; set; }
        public string HotNotes { get; set; }
        public string MgtProgramName { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        //public virtual ICollection<Patient_GoalItems> Patient_GoalItems { get; set; }
        //public virtual ICollection<Patient_Goals> Patient_Goals { get; set; }
        //public virtual ICollection<ProfileException> ProfileExceptions { get; set; }
        //public virtual ICollection<ProfileItem> ProfileItems { get; set; }
    }

    public partial class SymptomViewModel
    {
        public int SymptomID { get; set; }
        public string SymptomName { get; set; }
        public Nullable<bool> viewable_yn { get; set; }
        public Nullable<bool> Dictation { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public int RecordCount { get; set; }
    }

    public partial class SymptomSupplementViewModel
    {
        public int SymptomSupplementID { get; set; }
        public int SymptomID { get; set; }
        public int SupplementID { get; set; }
    }

    public partial class DiagnosisSupplementViewModel
    {
        public int DiagnosisSupplementID { get; set; }
        public int Diagnosis_ID { get; set; }
        public int SupplementID { get; set; }
    }

    public partial class GroupRangeSupplementViewModel
    {
        public int GroupRangeSupplementID { get; set; }
        public int GroupRangeID { get; set; }
        public int SupplementID { get; set; }
    }

    public partial class GoalItemViewModel
    {
        public int GoalItemID { get; set; }
        public string Tag { get; set; }
        public string DisplayName { get; set; }
        public string TextBoxName { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> ProviderID { get; set; }
    }

    public partial class QB_InvoicesViewModel
    {
        public int QBInvoiceID { get; set; }
        public string TxnID { get; set; }
        public string CustomerRefListID { get; set; }
        public string CustomerRefFullName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Num { get; set; }
        public string PONumber { get; set; }
        public string Terms { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string OpenBalance { get; set; }
        public string IsPaid { get; set; }
        public string InvoiceLineDesc { get; set; }
        public string InvoiceLineItemRefFullName { get; set; }
        public string SalesRepRefListID { get; set; }
        public string SalesRepRefFullName { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> InvoiceLineQuantity { get; set; }
        public Nullable<decimal> InvoiceLineRate { get; set; }
        public Nullable<decimal> InvoiceLineAmount { get; set; }
        public Nullable<System.DateTime> InvoiceLineServiceDate { get; set; }
        public string InvoiceLineItemRefListID { get; set; }
    }

    public partial class ProviderViewModel
    {
        public int id { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string ProviderName { get; set; }
        public string MondayStart { get; set; }
        public string MondayEnd { get; set; }
        public string TuesdayStart { get; set; }
        public string TuesdayEnd { get; set; }
        public string WednesdayStart { get; set; }
        public string WednesdayEnd { get; set; }
        public string ThursdayStart { get; set; }
        public string ThursdayEnd { get; set; }
        public string FridayStart { get; set; }
        public string FridayEnd { get; set; }
        public bool Active { get; set; }
        public string Category { get; set; }
        public string DEA { get; set; }
        public string NPI { get; set; }
        public bool External { get; set; }
    }

    public partial class apt_recViewModel
    {
        public int apt_id { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public Nullable<System.DateTime> Arrived_time { get; set; }
        public Nullable<System.DateTime> Seen_nurse_time { get; set; }
        public Nullable<System.DateTime> Seen_dr_time { get; set; }
        public Nullable<System.DateTime> closed_time { get; set; }
        public Nullable<System.DateTime> prescriptprint_time { get; set; }
        public Nullable<System.DateTime> followUp_time { get; set; }
        public Nullable<bool> closed_yn { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public Nullable<System.DateTime> ApptEnd { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> AppointmentTypeID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<bool> AllDay { get; set; }
        public Nullable<bool> EmailOnChange { get; set; }
        public Nullable<int> Results { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string ActionNeeded { get; set; }
        public int SaleMade_yn { get; set; }
        public string Note { get; set; }
        public bool LabsCheckedIn { get; set; }

       
    }

    public partial class Patient_VitalsViewModel
    {
        public int Vital_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<decimal> Wgt { get; set; }
        public string BloodPres { get; set; }
        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string Respirations { get; set; }
        public string Waist_Circm { get; set; }
        public string Hip_Circm { get; set; }
        public string Perc_Body_fat { get; set; }
        public Nullable<decimal> Height { get; set; }
        public string grip_r_lbs { get; set; }
        public string grip_l_lbs { get; set; }
        public string bicep_r_cir { get; set; }
        public string bicep_l_cir { get; set; }
        public string thigh_r_cir { get; set; }
        public string thigh_l_cir { get; set; }
        public string knee_r_cir { get; set; }
        public string knee_l_cir { get; set; }
        public string neck_cir { get; set; }
        public string chest_cir { get; set; }
        public string midriff_cir { get; set; }
        public string ma_Notes { get; set; }
    }

    public partial class Patient_Details_ViewModel
    {
        public int PatientID { get; set; }
        public string allergies { get; set; }
        public Nullable<bool> Cancel_NoShow_frm_signed { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> Work_Detailed_info { get; set; }
        public Nullable<bool> Work_CB_only { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<bool> Cell_Detailed_info { get; set; }
        public Nullable<bool> namealert { get; set; }
        public Nullable<bool> Cell_CB_Only { get; set; }
        public Nullable<bool> Home_detailed_info { get; set; }
        public Nullable<bool> hippa_signed { get; set; }
        public Nullable<System.DateTime> hippa_signed_date { get; set; }
        public Nullable<bool> Home_CB_only { get; set; }
        public Nullable<bool> Fax_auth_detailed_info { get; set; }
        public Nullable<bool> Email_auth_detailed_info { get; set; }
        public Nullable<bool> namealert1 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public Nullable<int> AGE { get; set; }
        public string PCP { get; set; }
        public string LMC_CP { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string FaxPone { get; set; }
        public string Email { get; set; }
        public bool EatingPlanReceived_YN { get; set; }
        public string Pager { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sex { get; set; }
        public string Clinic { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmeregencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyRelationship { get; set; }
        public string EmergencyState { get; set; }
        public string ContactPreference { get; set; }
        public Nullable<bool> Inactive { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> SpecialAttention { get; set; }
        public Nullable<int> ActivityRating { get; set; }
        public string Notes { get; set; }
        public string MedicalHistory { get; set; }
        public bool Medical { get; set; }
        public bool Aesthetics { get; set; }
        public bool Autoship { get; set; }
        public bool Retail { get; set; }
        public string AutoshipNote { get; set; }
        public string AutoshipAlerts { get; set; }
        public Nullable<System.DateTime> MedicareOptOut_Date { get; set; }
        public bool MedicareOptOut_YN { get; set; }
        public string NickName { get; set; }
        public string RenewalMonth { get; set; }
        public Nullable<bool> MedicareB { get; set; }
        public int RecordCount { get; set; }
        public int ConciergeID { get; set; }
    }

    public partial class Contact_tblViewModel
    {
        public int? ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public Nullable<int> AptType { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<bool> ReqsFollow_YN { get; set; }
        public string FollowUpBody { get; set; }
        public string MessageBody { get; set; }
        public Nullable<int> FollowUp_Staff { get; set; }
        public Nullable<System.DateTime> FollowUp_Date { get; set; }
        public Nullable<int> FollowUp_Category { get; set; }
        public Nullable<System.DateTime> FollowUp_ActualDate { get; set; }
        public Nullable<bool> FollowUP_Completed { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<int> Apt_ID { get; set; }
        public Nullable<int> FollowUp_ID { get; set; }
        public int RecordCount { get; set; }
    }

    public partial class Contact_Type_tblViewModel
    {
        public int AptTypeID { get; set; }
        public string AptTypeDesc { get; set; }
        public Nullable<bool> Viewable_yn { get; set; }
    }

    public partial class apt_FollowUpsViewModel
    {
        public int FollowUp_ID { get; set; }
        public Nullable<int> Apt_ID { get; set; }
        public Nullable<int> AptAssigned { get; set; }
        public Nullable<int> FollowUp_Cat { get; set; }
        public string FollowUp_Body { get; set; }
        public string FollowUp_Subject { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> Entered_By { get; set; }
        public Nullable<int> PatientID { get; set; }
        public bool FollowUp_Completed_YN { get; set; }
        public bool FollowUp_Assigned_YN { get; set; }
        public Nullable<System.DateTime> Range_Start { get; set; }
        public Nullable<System.DateTime> Range_End { get; set; }
        public Nullable<int> Severity { get; set; }
        public Nullable<int> Assigned { get; set; }
        public Nullable<int> DepartmentAssign { get; set; }
        public bool Private { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool FirstCall { get; set; }
        public bool SecondCall { get; set; }
        public bool FinalCall { get; set; }
        public bool Letter { get; set; }
        public string FirstCallNote { get; set; }
        public string SeconCallNote { get; set; }
        public string FinalCallNote { get; set; }
        public string LetterNote { get; set; }
    }

    public partial class apt_FollowUp_typesViewModel
    {
        public int FollowUp_Type_ID { get; set; }
        public string FollowUp_Type_Desc { get; set; }
        public Nullable<bool> Viewable_YN { get; set; }
        public Nullable<bool> ConsultType_YN { get; set; }
        public Nullable<bool> FollowUpType_YN { get; set; }
        public bool TicketType_YN { get; set; }
        public bool Appointment { get; set; }
        public bool StaffTicketType_YN { get; set; }
        public string CustomPage { get; set; }
        public string CustomHeader { get; set; }
        public string CustomParams { get; set; }
        public bool ChangeColor { get; set; }
        public int DepartmentID { get; set; }
    }

    public partial class PStaffUsersViewModel
    {
        public int EmployeeID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string EmployeeName { get; set; }
        public string Email_Address { get; set; }
        public string Type { get; set; }
        public string DrugNumber { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string access_level { get; set; }
        public Nullable<bool> CanWritePrescript { get; set; }
        public Nullable<bool> ActualStaff { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public string AutoshipAccess { get; set; }
        public bool AllowRecurring { get; set; }
        public int DepartmentID { get; set; }
    }   

    public class ResellersViewModel
    {
        public string BusinessName { get; set; }
        public int ResellerID { get; set; }
    }

    public partial class AppointmentTypeViewModel
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public string Category { get; set; }
        public bool ConfirmationEmail { get; set; }
        public string ConfirmationText { get; set; }
        public string Subject { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
        public string Attachment { get; set; }
        public bool OVU { get; set; }
    }

    public class AppointmentTypeView
    {
        public int id { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public string Category { get; set; }
        public bool ConfirmationEmail { get; set; }
        public string ConfirmationText { get; set; }
        public string Attachment { get; set; }
        public string Subject { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
        public bool OVU { get; set; }
        public string WufooFormKey { get; set; }
        public string MailChimpCampaignId { get; set; }
        public string MailChimpCampaignName { get; set; }
    }

    public partial class ContactStaffPatientTypeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> PatientID { get; set; }
        public int ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public string EnteredBy { get; set; }
        public string MessageBody { get; set; }
        public string Followupbody { get; set; }
        public string AptTypeDesc { get; set; }
    }

    public partial class aptDoctorconsoleViewModel
    {
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<decimal> InvoiceDue { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<System.DateTime> InvoiceDueDate { get; set; }
        public Nullable<bool> InvoicePaid { get; set; }
        public Nullable<System.DateTime> RenewalExcExpire { get; set; }
        public string RenewalException { get; set; }
        public string RenewalMonth { get; set; }
        public Nullable<int> TermsInMonths { get; set; }
        public int BloodWorkCount { get; set; }
        public Nullable<System.DateTime> ApptEnd { get; set; }
        public int apt_id { get; set; }
        public string StatusName { get; set; }
    }

    public partial class TResult
    {
        public string PanelName{ get; set; }
        public string TestName{ get; set; }
        public string ResultValue{ get; set; }
        public int PanelID{ get; set; }
        public string Reference{ get; set; }
        public string unit{ get; set; }

    }

   
}


