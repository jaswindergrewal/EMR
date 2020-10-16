using System.Collections.Generic;
using System.ServiceModel;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    [ServiceContract]
    public interface IDepartmentStaffService
    {
        [OperationContract]
        int SaveDepartmentStaff(int StaffID, string DepartmentID);

        [OperationContract]
        List<StaffViewModel> GetStaffDetails(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText, int DepartmentID);
    }

    
}
