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
    
    public partial class Patient_GoalItemType
    {
        public Patient_GoalItemType()
        {
            this.Patient_GoalItems = new HashSet<Patient_GoalItems>();
        }
    
        public int GoalItemTypeID { get; set; }
        public string Tag { get; set; }
        public string DisplayName { get; set; }
        public string TextBoxName { get; set; }
    
        public virtual ICollection<Patient_GoalItems> Patient_GoalItems { get; set; }
    }
}
