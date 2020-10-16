using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PrescriptionViewModel
    {
    }

    public partial class PrescriptionDrugViewModel
    {
        public int? PatientID { get; set; }
        public string DrugName { get; set; }
        public string Drug_Dose { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Writer { get; set; }
        public string Drug_NumbRefills { get; set; }
        public DateTime? Drug_DatePrescibed { get; set; }
        public DateTime? Drug_EndDate { get; set; }
        public bool? RePre_yn { get; set; }
        public string Notes { get; set; }
        public int? DrugID { get; set; }
        public int PrescriptionID { get; set; }
        public bool? ThirdParty { get; set; }
        public bool? Supplement_yn { get; set; }

        

     
    }

    public partial class PrescriptionSupplierViewModel
    {
        public int? PatientID { get; set; }
        public string SuppName { get; set; }
        public string SuppDose { get; set; }
        public string SuppDispenses { get; set; }
        public string Writer { get; set; }
        public string SuppNumbRefills { get; set; }
        public DateTime? SuppDatePrescibed { get; set; }
        public DateTime? SuppEndDate { get; set; }
        public bool? RePre_yn { get; set; }
        public string Notes { get; set; }
        public int? ProductID { get; set; }
        public int PresscriptionSuppID { get; set; }

       
    }

    public partial class DrugViewModel 
    {
        public int? DrugID { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public System.Nullable<bool> Viewable_yn { get; set; }
        public string Gender { get; set; }
        public string DrugType { get; set; }
        public string DrugCategory { get; set; }
        public System.Nullable<bool> Supplement_yn { get; set; }
        public System.Nullable<bool> Reviewed { get; set; }
        public System.Nullable<System.DateTime> DateEntered { get; set; }
        public int? _ProductID { get; set; }
     
       
    }

    public partial class PrescripDrugStaffViewModel
    {
        public int? PatientID { get; set; }
        public string DrugName { get; set; }
        public string Drug_Dose { get; set; }
        public string Drug_Dispenses { get; set; }
        public string EmployeeName { get; set; }
        public string Drug_NumbRefills { get; set; }
        public System.Nullable<System.DateTime> Drug_DatePrescibed { get; set; }
        public System.Nullable<System.DateTime> Closed_Date { get; set; }
        public System.Nullable<bool> RePre_yn { get; set; }
        public string Notes { get; set; }
        public int? Drug_ID { get; set; }
        public System.Nullable<System.DateTime> Drug_EndDate { get; set; }
        public string Writer { get; set; }
    }

    public partial class AutoshipProductsViewModel
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? AutoshipPrice { get; set; }
        public string QBId { get; set; }
        public System.Nullable<bool> Reviewed { get; set; }
        public System.Nullable<bool> Viewable { get; set; }
        public System.Nullable<bool> Active { get; set; }
        public int? Count { get; set; }
        public int? GroupID { get; set; }
    }

    public partial class PresCripSuppAutoshipProductStaffViewModel
    {
        public int? PatientID { get; set; }
        public string ProductName { get; set; }
        public string SuppName { get; set; }
        public string SuppDose { get; set; }
        public string SuppDispenses { get; set; }
        public string EmployeeName { get; set; }
        public string SuppNumbRefills { get; set; }
        public string Notes { get; set; }
        public int? ProductID{ get; set; }
        public System.Nullable<DateTime> SuppDatePrescibed { get; set; }
        public System.Nullable<DateTime> SuppEndDate { get; set; }
        public System.Nullable<bool> RePre_yn { get; set; }
        public int DrugID { get; set; }
    }

    public partial class ModifiedPrescribedAutoshipViewModel
    {
        public int? PrescriptionSuppID { get; set; }
        public string ProductName { get; set; }
        public string SuppName { get; set; }
    }

    public partial class PendingPrescriptionViewModel
    {
        public System.Nullable<DateTime> DateEntered { get; set; }
        public int PrescriptionID { get; set; }
        public string clinic { get; set; }
        public int patientid { get; set; }
        public string DrugName { get; set; }
        public string EmployeeName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }        
    }

    public partial class PendingSupplementViewModel
    {
        public System.Nullable<DateTime> DateEntered { get; set; }
        public int PrescriptionID { get; set; }
        public string clinic { get; set; }
        public int patientid { get; set; }
        public string DrugName { get; set; }
        public string EmployeeName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }        
    }

    public class PendingConsultRequestViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Range_Start { get; set; }
        public DateTime? Range_End { get; set; }
        public string Clinic { get; set; }
        public string followup_type_desc { get; set; }
        public int? apt_id { get; set; }
        public int PatientID { get; set; }
        public int followup_id { get; set; }
        public DateTime? dateentered { get; set; }
    }

    public class PendingBloodDrawsViewModel
    {
        public string EmployeeName { get; set; }
        public string PatientName { get; set; }
        public string FirstCall { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? value { get; set; }
        public string FirstNotes { get; set; }
        public string SecondCall { get; set; }
        public string SecondNotes { get; set; }
        public bool? FinalCall { get; set; }
        public string FinalNotes { get; set; }
        public bool? LetterSent { get; set; }
        public string LetterNotes { get; set; }
        public int? ID { get; set; }
        public string Clinic { get; set; }
        public int? PatientID { get; set; }
    }

    public class PendingBloodDrawsByClinicViewModel
    {
        public string EmployeeName { get; set; }
        public string PatientName { get; set; }
        public string FirstCall { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? value { get; set; }
        public string FirstNotes { get; set; }
        public string SecondCall { get; set; }
        public string SecondNotes { get; set; }
        public bool? FinalCall { get; set; }
        public string FinalNotes { get; set; }
        public bool? LetterSent { get; set; }
        public string LetterNotes { get; set; }
        public int? ID { get; set; }
        public string Clinic { get; set; }
        public int? PatientID { get; set; }
    }

    public class prescriptionHistoryViewModel {

        public int PrescriptionID { get; set; }
        public string DrugName { get; set; }
        public Nullable<bool> Closed_YN { get; set; }
        public Nullable<int> AptID { get; set; }
        public Nullable<System.DateTime> Drug_DatePrescibed { get; set; }
        public string Drug_Dose { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Drug_NumbRefills { get; set; }
        public string EmployeeName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string SuppDose { get; set; }
    }

    public class PrescipApprove
    {
        public int? PrescriptionID { get; set; }
        public bool? ThirdParty_YN { get; set; }
        public bool? viewable_yn { get; set; }
        public int? Patientid { get; set; }
        public DateTime? approved_date { get; set; }
        public DateTime? DateEntered { get; set; }
        public string EmployeeName { get; set; }
        public string DrugName { get; set; }
        public string Drug_Dose { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Drug_NumbRefills { get; set; }
        public bool? Approved_yn { get; set; }
        public DateTime? Drug_DatePrescibed { get; set; }
        public DateTime? Drug_EndDate { get; set; }
        public string Notes { get; set; }
        public string AccessLevel { get; set; }
    }
}
