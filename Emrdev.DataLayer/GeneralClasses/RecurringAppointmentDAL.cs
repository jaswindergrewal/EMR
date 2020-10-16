using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class RecurringAppointmentDAL : ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<RecurringAppointmentViewModel> PreviewRecurringAppointment(int ProviderID, int ApTID, DateTime ApptStart, DateTime ApptEND, string ApptStartH,
            string ApptEndH, string ApptStartM, string ApptEndM)
        {
            var objResult = ObjectEntity1.ssp_PreviewRecurringAppointment(ProviderID, ApTID, ApptStart, ApptEND, ApptStartH, ApptEndH, ApptStartM, ApptEndM).ToList();
            var objIList = new List<RecurringAppointmentViewModel>();
            Mapper.CreateMap<ssp_PreviewRecurringAppointment_Result, RecurringAppointmentViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void AddUpdateRecurringAppointment(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            ObjectEntity1.ssp_AddUpdateRecurringAppointment(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }

        
    }
}
