//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    
    public partial class ssp_GetClosePrescriptionDetails_Result
    {
        public Nullable<int> PatientID { get; set; }
        public string DrugName { get; set; }
        public string Drug_Dose { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Writer { get; set; }
        public string Drug_NumbRefills { get; set; }
        public Nullable<System.DateTime> Drug_DatePrescibed { get; set; }
        public Nullable<System.DateTime> Drug_EndDate { get; set; }
        public Nullable<bool> RePre_yn { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DrugID { get; set; }
    }
}
