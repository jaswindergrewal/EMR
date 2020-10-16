﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PatientViewModelNew
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
    }
}
