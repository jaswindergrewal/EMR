using Emrdev.ViewModelLayer;
using System.Collections.Generic;
using System.ServiceModel;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRenewalPackagesService" in both code and config file together.
    [ServiceContract]
    public interface IRenewalPackagesService
    {
        [OperationContract]
        List<RenewalPackagesViewModel> GetAllPackages(int RenewalID);

        [OperationContract]
        void InsertUpdateRenewalPackage(RenewalPackagesViewModel PackageViewModel);

        [OperationContract]
        bool CheckDuplicateType(int ID, string packageName);


        [OperationContract]
        List<ManagementProgramViewModel> GetManagementPrograms();

        [OperationContract]
        void SaveManagementProgram(ManagementProgramViewModel managementProgramDetails);
    }
}
