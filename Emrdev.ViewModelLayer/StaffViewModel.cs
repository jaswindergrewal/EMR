using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class StaffViewModel
    {
        public int EmployeeID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string EmployeeName { get; set; }
        public string Email_Address { get; set; }
        public string Type { get; set; }
        public string DrugNumber { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string access_level { get; set; }
        public Nullable<bool> CanWritePrescript { get; set; }
        public Nullable<bool> ActualStaff { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public string AutoshipAccess { get; set; }
        public bool AllowRecurring { get; set; }
        public int DepartmentID { get; set; }
        public int RecordCount { get; set; }
        public bool IsHARep { get; set; }

    }
}
