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
    
    public partial class Patients_CriticalTaskType
    {
        public Patients_CriticalTaskType()
        {
            this.Patients_CriticalTasks = new HashSet<Patients_CriticalTasks>();
        }
    
        public int TaskTypeID { get; set; }
        public string TaskName { get; set; }
        public int ExpireDays { get; set; }
    
        public virtual ICollection<Patients_CriticalTasks> Patients_CriticalTasks { get; set; }
    }
}
