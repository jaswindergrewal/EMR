using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class QBCustMatchPatientViewModel
    {
        public string FullName { get; set; }
        public string ListID { get; set; }
        public int CountQB { get; set; }
    }

    public partial class QB_MatchViewModel
    {
        public int PatientID { get; set; }
        public string QBid { get; set; }
        public string Note { get; set; }
    }

    public partial class PatientQuickBookViewModel
    {
        public int PatientID { get; set; }
        public string QBCust { get; set; }
        public string FullName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string HomePhone { get; set; }
        public bool InActive { get; set; }
        public string Notes { get; set; }
        public System.Nullable<DateTime> LastContact{ get; set; }
    }

 
    public partial class QBMatchEmrAddressViewModel
    {
       
        public string EmrAddress { get; set; }
        
    }
}
