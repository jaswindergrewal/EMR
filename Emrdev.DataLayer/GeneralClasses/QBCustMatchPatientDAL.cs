using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class QBCustMatchPatientDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

       #endregion

        public List<QBCustMatchPatientViewModel> GetQBCustomerList()
        {
            var objResult = ObjectEntity1.ssp_GetQuickBookList().ToList();
            var objIList = new List<QBCustMatchPatientViewModel>();
            Mapper.CreateMap<ssp_GetQuickBookList_Result, QBCustMatchPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList; 
            
        }

        public void DeleteMatch(string QBid)
        {
            ObjectEntity1.ssp_DeleteMatch(QBid);
        }

        public PatientViewModel GetPatientDetailById(int PatientId)
        {
            RenewalPackagesDAL objDAL = new RenewalPackagesDAL();
            return objDAL.GetPatientDetailById(PatientId);
            //Comented by jaswinder on 24 june 2014
            //var objResult = ObjectEntity1.ssp_GetPatientDetailById(PatientId).FirstOrDefault();
            //var objIList = new PatientViewModel();
            //Mapper.CreateMap<ssp_GetPatientDetailById_Result, PatientViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            //return objIList;
        }

        public List<PatientQuickBookViewModel> GetPatientQuickBookList()
        {
            var objResult = ObjectEntity1.ssp_GetPatientQuickBookList().ToList();
            var objIList = new List<PatientQuickBookViewModel>();
            Mapper.CreateMap<ssp_GetPatientQuickBookList_Result, PatientQuickBookViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        #region Qb_Match Page
        public List<QBCustMatchPatientViewModel> GetQBMatchListByPatientId(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetQBMatchByPatientId(PatientId).ToList();
            var objIList = new List<QBCustMatchPatientViewModel>();
            Mapper.CreateMap<ssp_GetQBMatchByPatientId_Result, QBCustMatchPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        #endregion
    }
}
