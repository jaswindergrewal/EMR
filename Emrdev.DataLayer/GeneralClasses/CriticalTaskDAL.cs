using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class CriticalTaskDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
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

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<CriticalTaskViewModel> GetCriticalTaskListByPatientID(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetCriticalTask(PatientId).ToList();
            var objIList = new List<CriticalTaskViewModel>();
            Mapper.CreateMap<ssp_GetCriticalTask_Result, CriticalTaskViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<uploadtblViewModelCritical> GetUploadDocsByPatientID(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetUploadDocs(PatientId).ToList();
            var objIList = new List<uploadtblViewModelCritical>();
            Mapper.CreateMap<ssp_GetUploadDocs_Result, uploadtblViewModelCritical>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
