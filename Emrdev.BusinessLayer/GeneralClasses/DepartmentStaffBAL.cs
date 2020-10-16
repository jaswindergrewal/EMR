using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class DepartmentStaffBAL
    {
        DepartmentStaffDAL objDepartmentStaffDAL = new DepartmentStaffDAL();
          
        public int SaveDepartmentStaff(int StaffID, string DepartmentID)
        {
            int result=Convert.ToInt32( objDepartmentStaffDAL.SaveDepartmentStaff(StaffID, DepartmentID));
            return result;
        }

        public List<StaffViewModel> GetStaffDetails(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText, int DepartmentID)
        {
            CampaignTypeDAL objDepartment = new CampaignTypeDAL();
            return objDepartment.GetStaffDetails(page, rows, sord, sidx, IsSearch, SearchColumn, SearchText, DepartmentID);
        }
    }
}
