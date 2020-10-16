using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AppointmentViewModel
    {
        public string TypeName {get;set; }
        public string ResultName { get; set; }
        public DateTime ApptStart { get; set; }
        public string ProviderName { get; set; }
        public int apt_id { get; set; }
        public DateTime date_entered { get; set; }
        public bool closed_yn { get; set; }
        public int patient_id { get; set; }
        public bool OVU { get; set; }
   
    
    }

    public class OpenAppointmentViewModel
    {
        public int apt_id { get; set; }
        public DateTime date_entered { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string clinic { get; set; }
        public int RecordCount { get; set; }
        public bool closed_yn { get; set; }
        public int PatientId { get; set; }
        public int Days { get; set; }
        public string DateEntered { get; set; }
    }

    public class PendingFollowupClinicViewModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Clinic { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Range_Start { get; set; }
        public DateTime Range_End { get; set; }
        public int apt_id { get; set; }
        public int patientid { get; set; }
        public int followup_id { get; set; }
        public DateTime dateentered { get; set; }
        public int RecordCount { get; set; }
        public string followup_type_desc { get; set; }
        public string Date_Entered { get; set; }
        public string RangeDate { get; set; }
    }

    public class PrescriptionFor5Days
    {
        public Nullable<int> PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ToDay { get; set; }
        public string OneDay { get; set; }
        public string TwoDay { get; set; }
        public string ThreeDay { get; set; }
        public string FourDay { get; set; }
        public int RecordCount { get; set; }
    }

    public class SpecialAptViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string shippingstreet { get; set; }
        public string shippingcity { get; set; }
        public string shippingstate { get; set; }
        public string shippingzip { get; set; }
        public string billingstreet { get; set; }
        public string billingcity { get; set; }
        public string billingstate { get; set; }
        public string billingzip { get; set; }
        public string workPhone { get; set; }
        public string cellphone { get; set; }
        public string homephone { get; set; }
        public string email { get; set; }
        public int patientid { get; set; }
        public string notes { get; set; }
        public string typename { get; set; }
        public string sourcetypedesc { get; set; }
        public int RecordCount { get; set; }
        public string DateEntered { get; set; }
        public string AptStart { get; set; }
       
    }

    public class OvuAppointment
    { 
        public int SymptomID { get; set; }
        public string SymptomName { get; set; }
        public int AptID { get; set; }
        public string dir { get; set; }
    }
  
}
