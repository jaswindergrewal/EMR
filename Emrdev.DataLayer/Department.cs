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
    
    public partial class Department
    {
        public Department()
        {
            this.DepartmentStaffs = new HashSet<DepartmentStaff>();
        }
    
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    
        public virtual ICollection<DepartmentStaff> DepartmentStaffs { get; set; }
    }
}
