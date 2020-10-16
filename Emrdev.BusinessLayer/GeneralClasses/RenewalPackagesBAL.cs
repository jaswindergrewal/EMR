using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;


namespace Emrdev.GeneralClasses
{
    public class RenewalPackagesBAL
    {
        RenewalPackagesDAL objBAL = new RenewalPackagesDAL();

        public List<RenewalPackagesViewModel> GetAllPackages(int RenewalID)
        {
            return objBAL.GetAllPackages(RenewalID);
           
        }


        public void InsertUpdateRenewalPackage(RenewalPackagesViewModel PackageViewModel)
        {
            //EMR2017
            //objBAL = new Emrdev.DataLayer.GeneralClasses.RenewalPackagesDAL();
            //admin_RenewalPackages objEntity = new admin_RenewalPackages();
            //if (PackageViewModel.RenewalID > 0)
            //{
            //    objEntity = objBAL.Get<admin_RenewalPackages>(o => o.RenewalID == PackageViewModel.RenewalID);
            //    if (objEntity != null)
            //    {
            //        objEntity.PackageName = PackageViewModel.PackageName;
            //        objEntity.Duration = PackageViewModel.Duration;
            //        objEntity.Amount = PackageViewModel.Amount;
            //        objEntity.IsActive = PackageViewModel.IsActive;
            //        objBAL.Edit(objEntity);
            //    }
                
               
            //}
            //else
            //{
            //    objEntity.PackageName = PackageViewModel.PackageName;
            //    objEntity.Duration = PackageViewModel.Duration;
            //    objEntity.Amount = PackageViewModel.Amount;
            //    objEntity.IsActive = PackageViewModel.IsActive;
            //    objBAL.Create(objEntity);
            //}
         
            
        }

        public bool CheckDuplicateType(int ID, string packageName)
        {
           
            RenewalPackagesDAL objDAL = new RenewalPackagesDAL();
            bool isExist = false;
            //Emr2017
            //if (ID == 0)
            //{
            //    var objfirst = objDAL.Get<Emrdev.DataLayer.admin_RenewalPackages>(o => o.PackageName.ToLower() == packageName.ToLower().Trim());
            //    if (objfirst != null)
            //        isExist = true;
            //}
            //else
            //{
            //    var objfirst = objDAL.Get<Emrdev.DataLayer.admin_RenewalPackages>(o => o.PackageName.ToLower() == packageName.ToLower().Trim() && o.RenewalID != ID);
            //    if (objfirst != null)
            //        isExist = true;
            //}
            return isExist;
        }

        public string renewalMonth(int RenewalID,int PatientID)
        {
            //RenewalPackagesDAL objDAL = new RenewalPackagesDAL();
            //if (RenewalID > 0)
            //{
            //   List<PatientPackagesViewModel> objPackage = objDAL.get<Emrdev.DataLayer.PatientPackage>(o => o.RenewalID== RenewalID && o.PatientID== PatientID).ToList();
            //    if (objPackage != null)
            //    { 
                   
            //    }
            //}
            //else
            //{
               
            //}
            return "";
        }

        public List<ManagementProgramViewModel> GetManagementPrograms()
        {
            return objBAL.GetManagementPrograms();

        }


        public void SaveManagementProgram(ManagementProgramViewModel managementProgramDetails)
        {
            objBAL = new Emrdev.DataLayer.GeneralClasses.RenewalPackagesDAL();
            AdminManagementProgramFee objEntity = new AdminManagementProgramFee();
            if (managementProgramDetails.Id > 0)
            {
                objEntity = objBAL.Get1<AdminManagementProgramFee>(o => o.Id == managementProgramDetails.Id);
                if (objEntity != null)
                {
                    objEntity.ProgramName = managementProgramDetails.ProgramName;
                    objEntity.DateEdited = DateTime.Now;
                    objEntity.IsActive = managementProgramDetails.IsActive;
                    objBAL.Edit1(objEntity);
                }


            }
            else
            {
                objEntity.ProgramName = managementProgramDetails.ProgramName;
                objEntity.DateEdited = DateTime.Now;
                objEntity.DateAdded = DateTime.Now;
                objEntity.IsActive = managementProgramDetails.IsActive;
                objBAL.Create1(objEntity);
            }


        }
    }
}
