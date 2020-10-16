using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;
using System.Collections.Generic;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RenewalPackagesService" in both code and config file together.
    public class RenewalPackagesService : IRenewalPackagesService
    {
        RenewalPackagesBAL objService = new RenewalPackagesBAL();
        public List<RenewalPackagesViewModel> GetAllPackages(int RenewalID)
        {
            return objService.GetAllPackages(RenewalID);
        }

        public void InsertUpdateRenewalPackage(RenewalPackagesViewModel PackageViewModel)
        {
            objService.InsertUpdateRenewalPackage(PackageViewModel);
        }

        public bool CheckDuplicateType(int ID, string packageName)
        {
            return objService.CheckDuplicateType(ID, packageName);
        }


        public List<ManagementProgramViewModel> GetManagementPrograms()
        {
            return objService.GetManagementPrograms();
        }


        public void SaveManagementProgram(ManagementProgramViewModel managementProgramDetails)
        {
             objService.SaveManagementProgram(managementProgramDetails);
        }
    }
}
