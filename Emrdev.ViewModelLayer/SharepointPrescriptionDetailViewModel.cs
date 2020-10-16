using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class SharepointPrescriptionDetailViewModel
    {
        public int ID { get; set; }
        public Nullable<int> PresciptionId { get; set; }
        public string PatientName { get; set; }
        public string Clinic { get; set; }
        public string Vials { get; set; }
        public string LastRefill { get; set; }
        public string MedStartDate { get; set; }

        public DateTime? LastRefillDateTime { get; set; }
        public DateTime? MedStartDateTime { get; set; }

        public string Physician { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> Diet { get; set; }
        public Nullable<bool> Medical { get; set; }
        public string ItemType { get; set; }
        public string Path { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }


    public class SharepointPrescriptionDetailAddEditViewModel
    {
        public int ID { get; set; }
        public Nullable<int> PresciptionId { get; set; }
        public string PatientName { get; set; }
        public string Clinic { get; set; }
        public string Vials { get; set; }
        public DateTime? LastRefill { get; set; }
        public DateTime? MedStartDate { get; set; }
        public string Physician { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> Diet { get; set; }
        public Nullable<bool> Medical { get; set; }
        public string ItemType { get; set; }
        public string Path { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }



}
