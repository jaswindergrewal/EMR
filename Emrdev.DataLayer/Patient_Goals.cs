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
    using System.Collections.Generic;
    
    public partial class Patient_Goals
    {
        public Patient_Goals()
        {
            this.Patient_GoalItems = new HashSet<Patient_GoalItems>();
        }
    
        public int GoalID { get; set; }
        public int PatientID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string Other { get; set; }
        public string OtherTypeDiaplay { get; set; }
    
        public virtual ICollection<Patient_GoalItems> Patient_GoalItems { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
