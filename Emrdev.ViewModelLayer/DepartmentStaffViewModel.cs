using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class DepartmentStaffViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentStaffID { get; set; }
        public int DepartmentID { get; set; }
        public int StaffID{ get; set; }
    }

    public class AptFollowupsTypeViewModel
    {
        public int FollowUp_Type_ID { get; set; }
        public string FollowUp_Type_Desc { get; set; }
    }

    public class DepartmentViewModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }


   

}
