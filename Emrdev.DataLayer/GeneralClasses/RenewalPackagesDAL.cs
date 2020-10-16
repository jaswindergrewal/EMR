using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class RenewalPackagesDAL : ObjectEntity, IRepositary
    {
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public void Create1<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }

        public void Edit1<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntityPart1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntityPart1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public T Get1<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }


        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public List<RenewalPackagesViewModel> GetAllPackages(int RenewalID)
        {//emr2017
            //var objResult = ObjectEntityPart1.ssp_GetRenewalPackages(RenewalID).ToList();
            var objIList = new List<RenewalPackagesViewModel>();
            //Mapper.CreateMap<ssp_GetRenewalPackages_Result, RenewalPackagesViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
      


        public void InsertPatientPackages(int PatientID, int RenewalID, bool insertFlag)
        {//emr2017
           // ObjectEntityPart1.ssp_InsertPatientRenewalPackages(PatientID, RenewalID, insertFlag);
        }

        public void InsertPatientProgramManagement(int PatientId,int ProgramId)
        {
            ObjectEntityPart2.ssp_SavePatientManagementPrograms(PatientId, ProgramId);
        }

        public void UpdatePatientChina(int PatientID, int? ChinaPatientId, string BillingCountry, string ShippingCountry)
        {

            ObjectEntityPart2.ssp_UpdatePatientChinapatient(PatientID, ChinaPatientId, BillingCountry, ShippingCountry);
        }

        public PatientViewModel GetPatientDetailById(int PatientId)
        {
            var objResult = ObjectEntityPart2.ssp_GetPatientDetailById(PatientId).FirstOrDefault();
            var objIList = new PatientViewModel();
            Mapper.CreateMap<ssp_GetPatientDetailById_Result, PatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);

            var objMgtResult = ObjectEntityPart2.ssp_GetPatientManagementProgramById(PatientId).FirstOrDefault();
            var objMgtIList = new PatientViewModel();
            Mapper.CreateMap<ssp_GetPatientManagementProgramById_Result, PatientViewModel>();
            objMgtIList = Mapper.Map(objMgtResult, objMgtIList);
            if (objMgtIList != null)
            {
                objIList.PatientManagementProgramId = objMgtIList.PatientManagementProgramId;
                objIList.ProgramName = objMgtIList.ProgramName;
            }
            return objIList;
        }

        public List<ManagementProgramViewModel> GetManagementPrograms()
        {
            var objResult = ObjectEntityPart2.ssp_GetManagementPrograms().ToList();
            var objIList = new List<ManagementProgramViewModel>();
            Mapper.CreateMap<ssp_GetManagementPrograms_Result, ManagementProgramViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

    }
}
