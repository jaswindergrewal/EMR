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
    public class TicketManageDAL : ObjectEntity, IRepositary
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
        public List<ContactListViewModel> GetTicketOnlyDetails(int ID)
        {
            List<ContactListViewModel> _ContactViewModel = new List<ContactListViewModel>();
            var result = ObjectEntity1.Set<Contact_tbl>();
            _ContactViewModel = (from objResult in result where objResult.FollowUp_ID==ID
                                  select new ContactListViewModel()
                                  {
                                    ContactDateEntered = objResult.ContactDateEntered,
                                    MessageBody=objResult.MessageBody
                                  }).ToList();
            return _ContactViewModel;
        }
        public List<ContactListViewModel> GetContactsNoteDetails(int ID)
        {
            List<ContactListViewModel> _ContactViewModel = new List<ContactListViewModel>();
            var result = ObjectEntity1.Set<Contact_tbl>();
            _ContactViewModel = (from objResult in result
                                 where objResult.FollowUp_ID == ID
                                 select new ContactListViewModel()
                                 {
                                     ContactDateEntered = objResult.ContactDateEntered,
                                     MessageBody = objResult.MessageBody
                                 }).ToList();
            return _ContactViewModel;
        }

        public TicketPatientViewModel GetAllTicketManageList(int ActiveTicket)
        {
            var objResult = ObjectEntity1.ssp_PatientTicketByID(ActiveTicket).FirstOrDefault();
            var objIList = new TicketPatientViewModel();
            Mapper.CreateMap<ssp_PatientTicketByID_Result, TicketPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
