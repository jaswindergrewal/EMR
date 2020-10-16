using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using System.Data;
using System.Data.Entity.Validation;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AcuitySchedulingDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            /*try
            {*/
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
            /* }
             catch (DbEntityValidationException e)
             {
                 foreach (var eve in e.EntityValidationErrors)
                 {
                     Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.State);
                     foreach (var ve in eve.ValidationErrors)
                     {
                         Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                     }
                 }
                 throw;
             }*/
        }

               public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }


        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        
        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
        public List<SharePointPatientViewModel> ListSharePointPatients()
        {
            var objResult = ObjectEntity1.ssp_ListSharePointPatients().ToList();
            var objIList = new List<SharePointPatientViewModel>();
            Mapper.CreateMap<ssp_ListSharePointPatients_Result, SharePointPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public SharePointPatientViewModel GetSharePointPatientsById(int Id)
        {
            var objResult = ObjectEntity1.ssp_GetSharePointPatientsById(Id).FirstOrDefault();
            var objIList = new SharePointPatientViewModel();
            Mapper.CreateMap<ssp_GetSharePointPatientsById_Result, SharePointPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        public void SaveUpdateSharePointPatients(SharePointPatientViewModel PatientDetails)
        {
            ObjectEntity1.ssp_SaveUpdateSharePointPatients(PatientDetails.Id, PatientDetails.ClinicId, PatientDetails.ProviderId, PatientDetails.StartRange, PatientDetails.EndRange, PatientDetails.FirstName, PatientDetails.LastName, PatientDetails.ApptDuration, PatientDetails.Phone, PatientDetails.Notes);
        }
    }
}
