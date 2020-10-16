using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   public  class CallFirePatientListModel
    {

        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string ContactPreference { get; set; }
        public Nullable<bool> Work_CB_Only { get; set; }
        public Nullable<bool> Work_Nomessage { get; set; }
        public Nullable<bool> Cell_CB_Only { get; set; }
        public Nullable<bool> Cell_NoMessage { get; set; }
        public Nullable<bool> Home_CB_Only { get; set; }
        public Nullable<bool> Home_NoMessage { get; set; }
        public string ApptStart { get; set; }
        public string ApptEnd { get; set; }
   }
}
