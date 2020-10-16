using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PendingFollowupDAL: ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
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
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion 

        public List<ContactListViewModel> GetContactList(int FollowUpID)
        {
            var objResult = ObjectEntity1.ssp_GetContactList(FollowUpID).ToList();
            var objIList = new List<ContactListViewModel>();
            Mapper.CreateMap<ssp_GetContactList_Result, ContactListViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        
        public List<PendingConsultRequestViewModel> GetPendingFollowups()
        {
            var objResult = ObjectEntity1.apt_PendingAesFollowUps().ToList();
            var objIList = new List<PendingConsultRequestViewModel>();
            Mapper.CreateMap<apt_PendingAesFollowUps_Result, PendingConsultRequestViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void CloseFollowup(int FollowUpID)
        {
            ObjectEntity1.ssp_CloseContacttblFollowup(FollowUpID);
        }

        #region IRepositary Members


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
