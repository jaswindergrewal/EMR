using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    public class DepartmentStaffService:IDepartmentStaffService
    {
      DepartmentStaffBAL objDeptStaffBAL = new DepartmentStaffBAL();
      public  int SaveDepartmentStaff(int StaffID, string DepartmentID)
      {
          return objDeptStaffBAL.SaveDepartmentStaff(StaffID, DepartmentID);
      }

      public List<StaffViewModel> GetStaffDetails(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText, int DepartmentID)
      {
          return objDeptStaffBAL.GetStaffDetails( page,  rows,  sord,  sidx,  IsSearch,  SearchColumn,  SearchText,  DepartmentID);
      }
    }
}
