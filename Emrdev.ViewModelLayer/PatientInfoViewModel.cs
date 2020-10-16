using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PatientInfoViewModel
    {
        //Model used for PatientInfo Control
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Clinic { get; set; }
        public string Gender { get; set; }
        public Nullable<bool> NameAlert { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
        public Nullable<bool> InActive { get; set; }
        public string Allergies { get; set; }
        public string NickName { get; set; }
        public Nullable<decimal> InvoiceDue { get; set; }
        public Nullable<DateTime> ExpirationDate { get; set; }
        public Nullable<DateTime> InvoiceDueDate { get; set; }
        public Nullable<bool> InvoicePaid { get; set;}
        public string RenewalException { get; set; }
        public Nullable<int> TermsInMonths { get; set; }
    }
}
