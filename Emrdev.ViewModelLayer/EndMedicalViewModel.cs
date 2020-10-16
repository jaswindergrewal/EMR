using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class EndMedicalViewModel
    {
        public int? PatientID { get; set; }
        public string FullName { get; set; }
        public string ShippingStreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Notes { get; set; }
       
    }
}
